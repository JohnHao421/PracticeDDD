using System;
using Xunit;
using StackExchange.Redis;
using Utils;
using System.Threading.Tasks;
using Utils.Redis;
using System.Collections.Generic;
using System.Linq;

namespace UtilsTests
{
    public class RedisUntilsTests : IDisposable
    {
        private static IConnectionMultiplexer multiplexer;
        private readonly IDatabase db;
        private RemoteSemaphore semaphore;

        static RedisUntilsTests()
        {
            var configuraOption = new ConfigurationOptions()
            {
                EndPoints = {
                    "host.docker.internal:6380"
                },
                Ssl = false,
            };
            multiplexer = ConnectionMultiplexer.Connect(configuraOption);
        }


        public RedisUntilsTests()
        {
            db = multiplexer.GetDatabase();
            semaphore = new RemoteSemaphore(db, 2, TimeSpan.FromSeconds(30));
        }


        [Fact]
        public async Task SemaphoreCanEnterWhenLessMaxCount()
        {
            var resList = new List<RemoteSemaphore.SemaphoreTakeResult>();

            for (int i = 0; i < 4; i++)
            {
                var res = await semaphore.TryGetAsync();
                resList.Add(res);
            }

            resList.Take(2).ToList().ForEach(v => Assert.True(v.IsSuccess));
            resList.TakeLast(2).ToList().ForEach(v => Assert.False(v.IsSuccess));

            foreach (var res in resList)
                await semaphore.ReleaseAsync(res.SemaphoreId);
        }

        [Fact]
        public async Task SeamphoreCanBeRelease()
        {
            var res = await semaphore.TryGetAsync();
            var len = await db.SortedSetLengthAsync(semaphore.TimeoutKey);
            Assert.Equal(1, len);

            await semaphore.ReleaseAsync(res.SemaphoreId);
            len = await db.SortedSetLengthAsync(semaphore.TimeoutKey);
            Assert.Equal(0, len);
        }

        [Fact]
        public async Task SeamphoreCanTimeout()
        {
            semaphore = new RemoteSemaphore(db, 1, TimeSpan.FromSeconds(1));

            var s1 = await semaphore.TryGetAsync();
            await Task.Delay(1500);
            var s2 = await semaphore.TryGetAsync();
            var len = await db.SortedSetLengthAsync(semaphore.TimeoutKey);

            Assert.True(s1.IsSuccess);
            Assert.True(s2.IsSuccess);
            Assert.Equal(1, len);
        }

        [Fact]
        public async Task CanRefreshWhenNotTimeout()
        {
            semaphore = new RemoteSemaphore(db, 2, TimeSpan.FromSeconds(2));
            var r1 = await semaphore.TryGetAsync();
            await Task.Delay(500);
            
            var isRefresh = await semaphore.RefreshAsync(r1.SemaphoreId);
            await Task.Delay(2200);
            var isRefresh2 = await semaphore.RefreshAsync(r1.SemaphoreId);

            Assert.True(isRefresh);
            Assert.True(isRefresh2);
        }

        public void Dispose()
        {
            var keys = new[] { semaphore.TimeoutKey, semaphore.OwnKey, semaphore.CounterKey };
            foreach (var k in keys)
                db.KeyDelete(k);
        }
    }
}
