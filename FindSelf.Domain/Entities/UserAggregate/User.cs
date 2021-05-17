using FindSelf.Domain.Entities.UserAggregate.Rules;
using FindSelf.Domain.Entities.UserAggregate.ValueObjects;
using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate
{
    public partial class User : Entity, IAggregateRoot
    {
        public static User Create() => new User(Guid.NewGuid());

        public Guid Id { get; }
        public string Nickname { get; private set; }
        public decimal Balance { get; private set; }

        public IReadOnlyList<UserTranscation> Transcations => _transcations;
        private List<UserTranscation> _transcations;

        private User(Guid id)
        {
            Id = id;
        }

        public void ChangeNickname(string nickname, ICheckUserNicknameUnique checkService)
        {
            CheckRule(new UserNicknameUniqueRule(nickname, checkService));
            Nickname = nickname;
        }

        public void Rechrage(decimal amount, Guid requestId)
        {
            CheckRule(new UserBalanceNonnegativeRule(amount));

            var transcation = UserTranscation.Create(null, this, requestId, amount);
            _Transfer(transcation);
        }

        public void TransferTo(User recevier, decimal amount, Guid requestId)
        {
            CheckRule(new UserBalanceNonnegativeRule(amount));

            var transcation = UserTranscation.Create(this, recevier, requestId, amount);
            recevier._Transfer(transcation);
            this._Transfer(UserTranscation.CreateReverse(transcation));

            AddDomainEvent(new UserTransferSuccessEvent(this, recevier, amount));
        }

        private void _Transfer(UserTranscation trancation)
        {
            if (trancation.Dircition == TransferDircition.Reduce)
            {
                CheckRule(new UserIsufficientBalanceRule(Balance, trancation.Amount));
                Balance -= trancation.Amount;
            }
            else
            {
                Balance += trancation.Amount;
            }
            _transcations ??= new List<UserTranscation>();
            _transcations.Add(trancation);
        }
    }
}
