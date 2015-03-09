using System;
using System.Net;
using System.Web.Mvc;
using infostyle.Configuration;
using log4net;

namespace infostyle.Controllers
{
    //for testing
    //fake api for external domain api forwarding
    public class FakeApiController : ControllerBase
    {
        private readonly ILog logger = LogManager.GetLogger(typeof (FakeApiController).Name);

        protected override bool NeedToCheckLoginState
        {
            get { return false; }
        }

        public ActionResult Run(string apiControllerName, string apiActionName)
        {
            logger.InfoFormat("FakeApi called {0}/{1}", apiControllerName, apiActionName);

            var uri = Config.RealApiUrl + "/api";
            if (!string.IsNullOrEmpty(apiControllerName))
                uri = uri + "/" + apiControllerName;
            if (!string.IsNullOrEmpty(apiActionName))
                uri = uri + "/" + apiActionName;
            var query = Request.QueryString.ToString();
            if (!string.IsNullOrEmpty(query))
                uri = uri + "?" + query;

            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Timeout = 25000;//conf?
            if (Request.HttpMethod != "GET" && Request.HttpMethod != "POST")
                throw new Exception(string.Format("What is {0}?", Request.HttpMethod));
            request.Method = Request.HttpMethod == "GET" ? WebRequestMethods.Http.Get : WebRequestMethods.Http.Post;

            request.CookieContainer = new CookieContainer();
            foreach (string cookieKey in Request.Cookies)
            {
                var cookie = Request.Cookies[cookieKey];
                request.CookieContainer.Add(new Cookie(cookie.Name, cookie.Values.ToString(), cookie.Path, new Uri(Config.RealApiUrl).Host));
            }

            if (request.Method != WebRequestMethods.Http.Get)
                Request.InputStream.CopyTo(request.GetRequestStream());

            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream == null)
                throw new WebException();

            return new FileStreamResult(stream, "application/json");
        }
    }
}