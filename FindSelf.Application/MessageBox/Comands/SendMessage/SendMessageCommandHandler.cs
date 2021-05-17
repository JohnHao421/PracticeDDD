using System.Threading;
using System.Threading.Tasks;
using FindSelf.Domain.Entities.UserAggregate;
using FindSelf.Domain.Repositories;
using MediatR;

namespace FindSelf.Application.MessageBox.Comands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly IMessageBoxRepository repository;

        public SendMessageCommandHandler(IMessageBoxRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new SiteMessage(request.Content, request.SenderId, request.ReciverId);
            var messagebox = await repository.GetOrCreateAsync(message.ReceiverId);
            messagebox.Send(message);
            await repository.UnitOfWork.CommitAsync();
            return true;
        }
    }
}
