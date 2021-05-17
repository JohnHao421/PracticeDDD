using AutoMapper;
using FindSelf.Application.ApplicationServices.Commands;
using FindSelf.Application.Commands;
using FindSelf.Application.Configuration.Queries.Paging;
using FindSelf.Application.Queries.Users;
using FindSelf.Application.Users;
using FindSelf.Application.Users.CreateUser;
using FindSelf.Application.Users.GetUser;
using FindSelf.Application.Users.Queries.GetUsers;
using FindSelf.Application.Users.Recharge;
using FindSelf.Application.Users.Transfer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PaggingView<UserDTO>> GetAllAsync(int index, int pageSize)
        {
            return mediator.Send(new GetAllUsersQuery(pageSize, index));
        }

        [Route("{uid}")]
        public Task<UserDTO> GetAsync(Guid uid)
        {
            return mediator.Send(new GetUserQuery(uid));
        }

        [HttpPost]
        public Task<UserDTO> RegisterAsync(CreateUserCommand command)
        {
            return mediator.Send(command);
        }

        [Route("transfer")]
        [HttpPost]
        public async Task<IActionResult> TransferAsync(UserTransferCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var requestCommand = IdentifiedCommand<UserTransferCommand, bool>.Create(command, requestId);
            command.RequestId = requestCommand.Id;
            var result = await mediator.Send(requestCommand);
            return Ok(result);
        }

        [HttpPost]
        [Route("{uid}/recharge")]
        public async Task<IActionResult> Recharge(Guid uid, RechargeBalanceCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            command.Uid = uid;
            var requestCommand = IdentifiedCommand<RechargeBalanceCommand, bool>.Create(command, requestId);
            command.RequestId = requestCommand.Id;
            var result = await mediator.Send(requestCommand);
            return Ok(result);
        }
    }
}