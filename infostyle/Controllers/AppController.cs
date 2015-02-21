using System.Web.Mvc;

namespace infostyle.Controllers
{
    public class AppController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}