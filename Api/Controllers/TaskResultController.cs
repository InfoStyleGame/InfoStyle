using System;
using System.Linq;
using EF;

namespace Api.Controllers
{
	public class TaskResultController : ControllerBase
    {
        public void Post(Guid taskId, int score)
        {
            using (var context = new InfostyleEntities())
            {
                var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (task == null)
                    throw new ArgumentException();

                context.Scores.Add(new Score {Id = Guid.NewGuid(), Score1 = score, Level = task.Level});
                context.SaveChanges();
            }
        }
    }
}