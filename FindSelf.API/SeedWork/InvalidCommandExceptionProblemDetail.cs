using FindSelf.Application.Configuration.Vaildation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSelf.API.SeedWork
{
    public class InvalidCommandExceptionProblemDetail : ProblemDetails
    {
        public InvalidCommandExceptionProblemDetail(InvalidCommandException exception)
        {
            this.Detail = exception.Details;
            this.Title = exception.Message;
            this.Status = StatusCodes.Status400BadRequest;
            this.Type = "https://somedomain/validation-error";
        }
    }
}
