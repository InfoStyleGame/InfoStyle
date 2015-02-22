using System.Linq;
using Api.Helpers;
using Api.Models;
using EF;

namespace Api.Controllers
{
    public class UserProgressController : ControllerBase
    {
        private const int defaultLevel = 1;

        public UserProgress Get()
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            using (var context = new InfostyleEntities())
            {
                var scores = context.Scores.Where(s => s.UserId == userId).ToArray();
                var currentLevel = scores.Any() ? scores.Max(s => s.Level) : defaultLevel;
                return new UserProgress {CurrentLevel = currentLevel};
            }
        }
    }
}