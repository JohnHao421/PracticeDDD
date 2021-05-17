using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FindSelf.Application.MessageBox.Comands.SendMessage;
using FindSelf.Domain.Entities.UserAggregate;
using MediatR;

namespace FindSelf.Application.DomainEventHandler
{
    public class UserTransferSuccessEventHandler : INotificationHandler<UserTransferSuccessEvent>
    {
        private readonly IMediator mediator;

        public UserTransferSuccessEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(UserTransferSuccessEvent notification, CancellationToken cancellationToken)
        {
            var command = new SendMessageCommand
            {
                ReciverId = notification.Recevier.Id,
                SenderId = Guid.Empty,
                Content = $"系统通知: 转账成功！已收到{notification.Amount}元。由{notification.User.Nickname}发起。"
            };
            return mediator.Send(command);
        }
    }
}
