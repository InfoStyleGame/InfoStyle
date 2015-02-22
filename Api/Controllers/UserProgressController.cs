using System.Linq;
using Api.Helpers;
using Api.Models;
using EF;

namespace Api.Controllers
{
    public class UserProgressController : ControllerBase
    {
        public UserProgress Get()
        {
            var userId = VkAuthHelper.GetCurrentUser(Request.Headers).Id;
            using (var context = new InfostyleEntities())
            {
                return new UserProgress {CurrentLevel = context.Scores.Where(s => s.UserId == userId).Max(s => s.Level)};
            }
        }
    }
}