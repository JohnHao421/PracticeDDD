using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FindSelf.Application.MessageBox.Comands.SendMessage
{
    public class SendMessageCommand : IRequest<bool>
    {
        public Guid ReciverId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
    }
}
