using System.Web.Mvc;
using infostyle.Attributes;

namespace infostyle.Controllers
{
    [AllowCrossSiteJson]
    public class ControllerBase : Controller
    {
    }
}