using System.Web.Mvc;

namespace infostyle.Attributes
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "http://norris.kontur");
            base.OnActionExecuting(filterContext);
        }
    }
}