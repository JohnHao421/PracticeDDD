using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Utils
{
    public class RedisLocker
    {
        private readonly IDatabase db;

        public RedisLocker(IDatabase db)
        {
            this.db = db;
        }

        public async Task<DistributedLock> GetLockAsync(string lockName, TimeSpan lockTimeout, TimeSpan? requireTimeout = null)
        {
            var requireAt = DateTime.Now + (requireTimeout ?? TimeSpan.Zero);
            while (requireTimeout == null || DateTime.Now < requireAt)
            {
                var value = Guid.NewGuid();
                var res = await db.StringSetAsync(lockName, value.ToString(), lockTimeout, When.NotExists);
                if (res == true)
                    return new DistributedLock(value, lockName, DateTime.Now + lockTimeout);
                await Task.Delay(5);
            }
            return null;
        }

        public async Task<bool> ReleaseLockAsync(DistributedLock theLock)
        {
            var transcation = db.CreateTransaction();
            transcation.AddCondition(Condition.StringEqual(theLock.LockName, theLock.Id.ToString()));
            var resTask = transcation.KeyDeleteAsync(theLock.LockName);
            var r = await transcation.ExecuteAsync();

            return await resTask;
        }
    }
}
