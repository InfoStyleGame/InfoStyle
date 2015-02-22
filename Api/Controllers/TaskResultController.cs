using System;
using Api.Helpers;
using EF;

namespace Api.Controllers
{
	public class TaskResultController : ControllerBase
    {
        public void Post(Guid taskId, int score)
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            using (var context = new InfostyleEntities())
            {
                context.User_Tasks.Add(new User_Tasks {Id = Guid.NewGuid(), UserId = userId, TaskId = taskId});
                context.SaveChanges();
            }
        }
    }
}