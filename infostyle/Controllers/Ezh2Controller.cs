using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class Ezh2Controller : ControllerBase
    {
        public ActionResult Index()
        {
            VerifyIsLoggedIn();
            return View();
        }
    }
}