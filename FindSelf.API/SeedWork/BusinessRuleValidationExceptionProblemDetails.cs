using FindSelf.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSelf.API.SeedWork
{
    public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Detail = exception.Details;
            this.Status = StatusCodes.Status409Conflict;
            this.Title = "执行失败";
            this.Type = "https://somedomain/business-rule-validation-error";
        }
    }
}
