using FindSelf.Domain.SeedWork;
using System;

namespace FindSelf.Domain.Entities.UserAggregate
{
    public class UserTransferSuccessEvent : IDomainEvent
    {
        public User User { get; }
        public User Recevier { get; }
        public decimal Amount { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;

        public UserTransferSuccessEvent(User user, User recevier, decimal amount)
        {
            User = user;
            Recevier = recevier;
            Amount = amount;
        }
    }
}