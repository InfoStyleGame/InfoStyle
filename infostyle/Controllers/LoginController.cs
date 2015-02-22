using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}