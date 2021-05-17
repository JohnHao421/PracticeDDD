using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Domain.Entities.UserAggregate
{
    public class MessageBox : Entity , IAggregateRoot
    {
        public int Id { get; }
        public Guid Uid { get; }

        public IEnumerable<SiteMessage> Messages => _messages;
        private List<SiteMessage> _messages;

        private MessageBox() { }
        private MessageBox(Guid userId)
        {
            Uid = userId;
        }

        public void Send(SiteMessage message)
        {
            _messages ??= new List<SiteMessage>();
            _messages.Add(message);
        }

        public static MessageBox Create(Guid userId)
        {
            return new MessageBox(userId);
        }
    }
}
