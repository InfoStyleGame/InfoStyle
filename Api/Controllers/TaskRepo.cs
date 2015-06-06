using System;
using System.Collections.Generic;
using System.Linq;
using EF;
using EF.Enums;
using NUnit.Framework;

namespace Api.Controllers
{
	public class TaskRepo
	{
		public List<Task> GetTasks(TaskType taskType, int level, int count, int userId)
		{
			using (var context = new InfostyleEntities())
			{
				var userSolvedTasks = context.Users.First(u => u.Id == userId).User_Tasks;
				var allTasks = context.Tasks.Where(t => t.Type == taskType && t.Level == level).ToList();
				allTasks.ShuffleInPlace();
				var allSolvedTasks = new HashSet<Guid>(userSolvedTasks.Select(ut => ut.TaskId));
				var notSolvedTasks = allTasks.Where(t => !allSolvedTasks.Contains(t.Id)).Take(count).ToList();
				var solvedTasks = allTasks.Where(t => allSolvedTasks.Contains(t.Id)).Take(count - notSolvedTasks.Count).ToList();
				foreach (var task in notSolvedTasks)
					context.User_Tasks.Add(new User_Tasks {Id = Guid.NewGuid(), UserId = userId, TaskId = task.Id});
				context.SaveChanges();
				var tasks = notSolvedTasks.Concat(solvedTasks);
				return tasks.ToList();
			}
		}
	}

	[TestFixture]
	public class TasksRepo_GetTasks_should
	{
		private User newUser;
		private TaskRepo controller = new TaskRepo();

		[SetUp]
		public void SetUp()
		{
			newUser = NewUser();
		}

		[TearDown]
		public void TearDown()
		{
			using (var context = new InfostyleEntities())
			{
				var tasks = context.User_Tasks.Where(t => t.UserId == newUser.Id);
				context.User_Tasks.RemoveRange(tasks);
				newUser = context.Users.Attach(newUser);
				context.Users.Remove(newUser);
				context.SaveChanges();
			}
		}

		[Test]
		public void return_count_tasks()
		{
			var first5 = controller.GetTasks(TaskType.Card, 0, 5, newUser.Id);
			var second5 = controller.GetTasks(TaskType.Card, 0, 5, newUser.Id);
			var third5 = controller.GetTasks(TaskType.Card, 0, 5, newUser.Id);
			Assert.AreEqual(5, first5.Count());
			Assert.AreEqual(5, first5.Distinct().Count());
			Assert.AreEqual(5, second5.Count());
			Assert.AreEqual(5, second5.Distinct().Count());
			Assert.AreEqual(5, third5.Count());
			Assert.AreEqual(5, third5.Distinct().Count());
			Assert.AreEqual(8, first5.Concat(second5).Concat(third5).Select(t => t.Id).Distinct().Count());
		}

		private static User NewUser()
		{
			using (var context = new InfostyleEntities())
			{
				var newUser = new User
				{
					Name = "UnitTest_GetTasks_should",
					VKLogin = "-1",
					LastDailyEditTime = DateTime.UtcNow,
					Rank = 1000
				};
				context.Users.Add(newUser);
				context.SaveChanges();
				return newUser;
			}
		}
	}
}