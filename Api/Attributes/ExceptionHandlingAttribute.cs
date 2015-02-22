using System;
using System.Web.Http.Filters;
using log4net;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        private readonly ILog log = LogManager.GetLogger(typeof (ExceptionHandlingAttribute));

        public override void OnException(HttpActionExecutedContext context)
        {
            var errorId = Guid.NewGuid();

            var message = String.Format("Crash: Action '{0}' failed.", context.ActionContext.ActionDescriptor.ActionName);
            log.Error(message, context.Exception);
        }
    }
};