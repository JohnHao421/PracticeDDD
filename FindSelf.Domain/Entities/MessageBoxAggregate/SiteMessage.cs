using FindSelf.Domain.SeedWork;
using System;

namespace FindSelf.Domain.Entities.UserAggregate
{
    public class SiteMessage : Entity
    {
        public int Id { get; }
        public string Content { get; }
        public DateTimeOffset SendTimesteamp { get; }
        public Guid SnederId { get; }
        public Guid ReceiverId { get; }

        protected SiteMessage()
        {

        }

        public SiteMessage(string content, Guid sneder, Guid receiver)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            SnederId = sneder;
            ReceiverId = receiver;
            SendTimesteamp = DateTimeOffset.Now;
        }
    }
}