using System.Web.Http;
using log4net;

namespace Api.Controllers
{
    public class HelloController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HelloController));

        public string Get()
        {
            log.Info("Me was asked about hello, master!");
            return "Hello, I'm Infostyle Api";
        } 
    }
}