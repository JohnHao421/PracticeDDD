using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Redis
{
    public class RemoteSemaphore
    {
        public class SemaphoreTakeResult
        {
            public bool IsSuccess { get; internal set; }
            public Guid SemaphoreId { get; internal set; } 

            internal static SemaphoreTakeResult CreateNew()
            {
                return new SemaphoreTakeResult
                {
                    IsSuccess = false,
                    SemaphoreId = Guid.NewGuid()
                };
            }
        }


        public string TimeoutKey { get; }
        public string CounterKey { get; }
        public string OwnKey { get; }

        private readonly IDatabase db;
        private readonly int maxCount;
        private readonly TimeSpan holdTimeout;
        public RemoteSemaphore(IDatabase db, int maxCount, TimeSpan holdTimeout, string prefix = "")
        {
            if (maxCount <= 0) throw new ArgumentOutOfRangeException(nameof(maxCount));

            this.db = db;
            this.maxCount = maxCount;
            this.holdTimeout = holdTimeout;

            var id = Guid.NewGuid();

            prefix = string.IsNullOrWhiteSpace(prefix) ? "" : prefix + ":";
            this.TimeoutKey = prefix + "seamphore";
            this.CounterKey = prefix + "counter";
            this.OwnKey = prefix + "seamphore_own";
        }

        public async Task<SemaphoreTakeResult> TryGetAsync()
        {
            var result = SemaphoreTakeResult.CreateNew();
            await _TakeLockAsync(async () =>
            {
                result.IsSuccess = await _InnerGetAsync(result.SemaphoreId);
            });
            return result;
        }

        private async Task _TakeLockAsync(Action sucessTakeAction)
        {
            var lockKey = TimeoutKey + "_lock";
            var lockValue = Guid.NewGuid().ToString();

            var isTakeLock = await db.LockTakeAsync(lockKey, lockValue, TimeSpan.FromSeconds(5));
            try
            {
                if (isTakeLock)
                {
                   sucessTakeAction();
                }
            }
            finally
            {
                await db.LockReleaseAsync(lockKey, lockValue);
            }
        }

        private async Task<bool> _InnerGetAsync(Guid seamphoreId)
        {
            var semaphoreValue = seamphoreId.ToString();
            var count = await db.StringIncrementAsync(CounterKey);

            var pipeline = db.CreateBatch();
            var deadline = DateTimeOffset.Now.Subtract(holdTimeout);
            _ = pipeline.SortedSetRemoveRangeByScoreAsync(TimeoutKey, 0, deadline.ToUnixTimeMilliseconds());
            _ = pipeline.SortedSetCombineAndStoreAsync(SetOperation.Intersect, OwnKey, OwnKey, TimeoutKey, Aggregate.Min);

            _ = pipeline.SortedSetAddAsync(OwnKey, semaphoreValue, count);
            _ = pipeline.SortedSetAddAsync(TimeoutKey, semaphoreValue, DateTimeOffset.Now.ToUnixTimeMilliseconds());
            var rankTask = pipeline.SortedSetRankAsync(OwnKey, semaphoreValue, Order.Ascending);
            pipeline.Execute();

            return await rankTask < maxCount;
        }

        public async Task ReleaseAsync(Guid seamphoreId)
        {
            var seamphoreValue = seamphoreId.ToString();
            var pipeline = db.CreateBatch();
            var rmTask1 = pipeline.SortedSetRemoveAsync(TimeoutKey, seamphoreValue);
            var rmTask2 = pipeline.SortedSetRemoveAsync(TimeoutKey, seamphoreValue);
            pipeline.Execute();
            await Task.WhenAll(rmTask1, rmTask2);
        }

        public async Task<bool> RefreshAsync(Guid seamphoreId)
        {
            var r = false;
            await _TakeLockAsync(async () =>
            {
                r = await _InnerRefreshAsync(seamphoreId);
            });
            return r;
        }

        private Task<bool> _InnerRefreshAsync(Guid seamphoreId)
        {
            var id = seamphoreId.ToString();
            var newExpire = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var trancation = db.CreateTransaction();
            trancation.AddCondition(Condition.SortedSetContains(TimeoutKey, id));
            trancation.SortedSetRemoveAsync(TimeoutKey, id);
            trancation.SortedSetAddAsync(TimeoutKey, id, newExpire);
            return trancation.ExecuteAsync();
        }
    }
}
