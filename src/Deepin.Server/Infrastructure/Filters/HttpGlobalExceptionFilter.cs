using Deepin.Server.Infrastructure.ActionResults;
using Deepin.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Deepin.Server.Infrastructure.Filters;

public class HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger, IWebHostEnvironment env) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
        if (context.Exception.GetType() == typeof(DomainException))
        {
            var problemDetails = new HttpValidationProblemDetails()
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Detail = "Please refer to the errors property for additional details."
            };
            if (context.Exception.InnerException is null)
            {
                problemDetails.Errors.Add("DomainValidations", [context.Exception.Message]);
            }
            else
            {
                switch (context.Exception.InnerException)
                {
                    case ValidationException validationException:
                        validationException.Errors.GroupBy(s => s.PropertyName)
                            .ToList()
                            .ForEach(g => problemDetails.Errors.Add(g.Key, g.Select(s => s.ErrorMessage).ToArray()));
                        break;
                    default:
                        break;
                }
            }
            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            var json = new JObject(new JProperty("Messages", ["An error occur.Try it again."]));
            if (env.IsDevelopment())
            {
                json.Add(new JProperty("Exception", context.Exception));
            }
            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        context.ExceptionHandled = true;
    }
}
