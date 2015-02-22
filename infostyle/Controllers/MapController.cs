using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class MapController : ControllerBase
    {
        public ActionResult Index(bool? notLoggedIn)
        {
            var checkLoggedIn = !notLoggedIn.HasValue || !notLoggedIn.Value;
            if (checkLoggedIn)
                VerifyIsLoggedIn();
            return View(checkLoggedIn);
        }
    }
}