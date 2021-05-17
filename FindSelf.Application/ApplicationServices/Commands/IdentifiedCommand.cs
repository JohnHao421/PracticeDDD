using FindSelf.Application.Configuration.Commands;
using FindSelf.Application.Configuration.Vaildation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.ApplicationServices.Commands
{
    public class IdentifiedCommand<T, R> : ICommand<R>
        where T : ICommand<R>
    {
        public T Command { get; }
        public Guid Id { get; }

        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }

        public static IdentifiedCommand<T, R> Create(T command, string requestId)
        {
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                return new IdentifiedCommand<T, R>(command, guid);
            }
            else
            {
                throw new InvalidCommandException("无效命令", "命令幂等Id不是有效的GUID");
            }
        }
    }
}
