using System.Web.Http;

namespace Api.Controllers
{
    public class HelloController : ApiController
    {
        public string Get()
        {
            return "Hello, I'm Infostyle Api";
        } 
    }
}