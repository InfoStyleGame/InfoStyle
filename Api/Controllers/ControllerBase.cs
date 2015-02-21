using System.Web.Http;
using Api.Attributes;

namespace Api.Controllers
{
    [AllowCrossSiteJson]
    public class ControllerBase : ApiController
    {
        
    }
}