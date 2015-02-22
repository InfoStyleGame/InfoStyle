using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using infostyle.Exceptions;
using log4net;
using Newtonsoft.Json.Linq;

namespace infostyle.Controllers
{
    public class ControllerBase : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ControllerBase));

        private bool LoginState;

        protected void VerifyIsLoggedIn()
        {
            if (!LoginState)
                throw new LoginPleaseException();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception is LoginPleaseException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Index", "Map", new {notLoggedIn = true});
                return;
            }

            log.Error("Crash", filterContext.Exception);

            throw filterContext.Exception;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            LoginState = IsLoggedIn();
        }

        private const string apiHost = "norris.kontur";

        private bool IsLoggedIn()
        {
            //говно костыль
            if (!System.Web.HttpContext.Current.Request.Url.ToString().ToLower().Contains(apiHost.ToLower()))
                return true;
            return GetBool("http://" + apiHost + ":4444/api/userInfo/isLoggedIn");
        }

        private bool GetBool(string url, int timeout = 5000)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Timeout = timeout;
            
            request.CookieContainer = new CookieContainer();
            foreach (string cookieKey in Request.Cookies)
            {
                var cookie = Request.Cookies[cookieKey];
                request.CookieContainer.Add(new Cookie(cookie.Name, cookie.Values.ToString(), cookie.Path, apiHost));
            }

            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream == null)
                throw new WebException();
            var value = JToken.Parse(new StreamReader(stream).ReadToEnd());
            return (bool) value;
        }
    }
}