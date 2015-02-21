using System;
using System.Web.Http;
using Api.Helpers;
using EF;

namespace Api.Controllers
{
    public class TaskResultController : ApiController
    {
        public void Post(Guid taskId, int score)
        {
            var userId = VkAuthHelper.GetCurrentUserId(Request.Headers) ?? "stranger";
            using (var context = new InfostyleEntities())
            {
                context.Scores.Add(new Score {Id = Guid.NewGuid(), UserId = userId, TaskId = taskId, Value = score});
                context.SaveChanges();
            }
        }
    }
}