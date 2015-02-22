using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Api.Helpers;
using Api.Models;
using EF;
using EF.Enums;

namespace Api.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly TaskMapper taskMapper;

        public TaskController()
        {
            taskMapper = new TaskMapper();
        }

        [HttpPost]
        public TaskViewModel[] Card(int level)
        {
            const int count = 5;
            return GetTasks(TaskType.Card, level, count);
        }

        [HttpPost]
        public TaskViewModel Get(TaskType type, int level)
        {
            var tasks = GetTasks(type, level, 1);
            if (!tasks.Any())
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return tasks.Single();
        }

        private TaskViewModel[] GetTasks(TaskType taskType, int level, int count)
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;

            using (var context = new InfostyleEntities())
            {
                var tasks = context.Tasks.Where(t => t.Type == taskType && t.Level == level && !t.User_Tasks.Any())
                    .OrderBy(_ => Guid.NewGuid())
                    .Take(count).ToArray();

                foreach (var task in tasks)
                    context.User_Tasks.Add(new User_Tasks { Id = Guid.NewGuid(), UserId = userId, TaskId = task.Id });

                return tasks.Select(t => taskMapper.Parse(t)).ToArray();
            }
        }
    }
}