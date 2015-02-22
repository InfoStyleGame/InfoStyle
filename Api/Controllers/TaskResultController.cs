using System;
using Api.Helpers;
using EF;

namespace Api.Controllers
{
	public class TaskResultController : ControllerBase
    {
        public void Post(Guid taskId, int score)
        {
            var userId = VkAuthHelper.GetCurrentUserId(Request.Headers) ?? "stranger";
            using (var context = new InfostyleEntities())
            {
				//TODO: убрать 42
				context.Scores.Add(new Score { Id = Guid.NewGuid(), UserId = 42, TaskId = taskId, Value = score });
                context.SaveChanges();
            }
        }
    }
}