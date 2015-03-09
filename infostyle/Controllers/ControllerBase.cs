using System.IO;
using System.Net;
using System.Web.Mvc;
using infostyle.Exceptions;
using log4net;
using Newtonsoft.Json.Linq;

namespace infostyle.Controllers
{
    public class ControllerBase : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ControllerBase));

        protected virtual bool NeedToCheckLoginState
        {
            get { return true; }
        }

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
            if (NeedToCheckLoginState)
                LoginState = IsLoggedIn();
        }

        //todo выпилить нафиг и перенести на клиента
        private bool IsLoggedIn()
        {
            return GetBool("http://" + Request.Url.Host + ":" + Request.Url.Port + "/api/userInfo/isLoggedIn", Request.Url.Host);
        }

        private bool GetBool(string url, string host, int timeout = 25000)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Timeout = timeout;
            
            request.CookieContainer = new CookieContainer();
            foreach (string cookieKey in Request.Cookies)
            {
                var cookie = Request.Cookies[cookieKey];
                request.CookieContainer.Add(new Cookie(cookie.Name, cookie.Values.ToString(), cookie.Path, host));
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