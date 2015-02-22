using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class EzhController : ControllerBase
    {
        public ActionResult Index()
        {
            VerifyIsLoggedIn();
            return View();
        }
    }
}