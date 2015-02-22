using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class AppController : ControllerBase
    {
        public ActionResult Index(int? level)
        {
            VerifyIsLoggedIn();
            return View(new AppModel{Level = level});
        }
    }

    public class AppModel
    {
        public int? Level { get; set; }
    }
}