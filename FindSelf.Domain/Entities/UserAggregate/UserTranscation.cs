using FindSelf.Domain.Entities.UserAggregate.ValueObjects;
using FindSelf.Domain.SeedWork;
using System;

namespace FindSelf.Domain.Entities.UserAggregate
{

    public class UserTranscation : Entity
    {
        public long Id { get; }
        public Guid PayerId { get; private set; }
        public Guid Uid { get; private set; }
        public Guid RequestId { get; private set; } 
        public decimal Amount { get; private set; }
        public TransferDircition Dircition { get; private set; }
        public DateTimeOffset CreateTime { get; protected set; }

        private UserTranscation(long id, Guid payerId, Guid uid, Guid requestId, decimal amount, TransferDircition dircition)
        {
            Id = id;
            PayerId = payerId;
            Uid = uid;
            RequestId = requestId;
            Amount = amount;
            Dircition = dircition;
        }

        public static UserTranscation Create(User payer, User receiver, Guid requestId, decimal amount)
        {
            var dircition = amount >= 0 ? TransferDircition.Increase : TransferDircition.Reduce;
            var payerId = payer?.Id ?? Guid.Empty;

            var trancation = new UserTranscation(default, payerId, receiver.Id, requestId, amount, dircition);
            trancation.CreateTime = DateTimeOffset.Now;
            return trancation;
        }

        public static UserTranscation CreateReverse(UserTranscation transcation)
        {
            var negativeDirction = (TransferDircition)((byte)transcation.Dircition ^ 1);
            var newTranscation = new UserTranscation(default, transcation.Uid, transcation.PayerId, transcation.RequestId, transcation.Amount, negativeDirction);
            newTranscation.CreateTime = transcation.CreateTime;
            return newTranscation;
        }
    }
}