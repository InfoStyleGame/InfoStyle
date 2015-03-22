using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Api.Exceptions;
using log4net;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        private readonly ILog log = LogManager.GetLogger(typeof (ExceptionHandlingAttribute));

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is UnauthorizedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            var message = String.Format("Crash: Action '{0}' failed.", context.ActionContext.ActionDescriptor.ActionName);
            log.Error(message, context.Exception);
        }
    }
};