using System;
using System.Linq;
using Api.Helpers;
using EF;

namespace Api.Controllers
{
	public class TaskResultController : ControllerBase
    {
        public void Post(int level, int score)
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            using (var context = new InfostyleEntities())
            {
                var prevScore = context.Scores.FirstOrDefault(s => s.UserId == userId && s.Level == level);
                if (prevScore != null)
                {
                    if (prevScore.Score1 <= score)
                        prevScore.Score1 = score;
                }
                else
                {
                    context.Scores.Add(new Score {Id = Guid.NewGuid(), Score1 = score, Level = level, UserId = userId});
                }
                context.SaveChanges();
            }
        }
    }
}