using System.Linq;
using System.Net;
using System.Web.Http;
using Api.Helpers;
using Api.Models;
using EF.Enums;

namespace Api.Controllers
{
	public class TaskController : ControllerBase
	{
		private readonly TaskMapper taskMapper;
		private readonly TaskRepo taskRepo = new TaskRepo();

		public TaskController()
		{
			taskMapper = new TaskMapper();
		}

		[HttpPost]
		public TaskViewModel[] Card(int level)
		{
			var user = VkAuthHelper.GetCurrentUser(Request.Headers);
			const int count = 5;
			return taskRepo.GetTasks(TaskType.Card, level, count, user.Id)
				.Select(taskMapper.Parse).ToArray();
		}

		[HttpPost]
		public TaskViewModel Get(TaskType type, int level)
		{
			var user = VkAuthHelper.GetCurrentUser(Request.Headers);
			var tasks = taskRepo.GetTasks(type, level, 1, user.Id);
			if (!tasks.Any())
				throw new HttpResponseException(HttpStatusCode.NotFound);
			var single = tasks.Single();
			return taskMapper.Parse(single);
		}
	}
}