using System.Web.Http;
using Api.Attributes;

namespace Api.Controllers
{
    [AllowCrossSiteJson]
    [ExceptionHandling]
    public class ControllerBase : ApiController
    {
        
    }
}