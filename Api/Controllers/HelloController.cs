using System.Web.Http;
using Api.Helpers;
using log4net;

namespace Api.Controllers
{
    public class HelloController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HelloController));

        [HttpGet]
        public string Get()
        {
            var user = VkAuthHelper.GetCurrentUserId(Request.Headers) ?? "stranger";
            log.InfoFormat("Me was asked about hello, master! And he was {0}", user);
            return string.Format("Hello, {0}, I'm Infostyle Api", user);
        } 
    }
}