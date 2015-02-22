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

		[HttpGet]
		public TaskViewModel[] Card(int level)
	    {
			using (var context = new InfostyleEntities())
			{
				var tasks = context.Tasks.Where(t => t.Type == TaskType.Card && t.Level == level)
					.OrderBy(_ => Guid.NewGuid())
					.Take(5).ToList();
				return tasks.Select(t => taskMapper.Parse(t)).ToArray();
			}
		}

		public TaskViewModel Get(TaskType type, int level)        {
            using (var context = new InfostyleEntities())
            {
                var task = context.Tasks.Where(t => t.Type == type && t.Level == level)
                        .OrderBy(_ => Guid.NewGuid())
                        .FirstOrDefault();
                if (task == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);

                return taskMapper.Parse(task);
            }
        }
    }
}