using System.Web.Http;
using Api.Helpers;
using log4net;

namespace Api.Controllers
{
    public class TaskResultController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TaskResultController));

        [HttpPost]
        public void Post(int score)
        {
            var user = VkAuthHelper.GetCurrentUserId(Request.Headers) ?? "stranger";
            log.InfoFormat("{0} scored {1}", user, score);
        }
    }
}