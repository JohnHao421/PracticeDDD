using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class DistributedLock
    {
        internal Guid Id { get; }
        public string LockName { get; }
        public DateTime TimeoutAt { get; }

        internal DistributedLock(Guid id, string lockName, DateTime timeoutAt)
        {
            Id = id;
            LockName = lockName;
            TimeoutAt = timeoutAt;
        }
    }
}
