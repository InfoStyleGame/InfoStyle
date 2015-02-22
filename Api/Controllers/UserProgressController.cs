using Api.Models;

namespace Api.Controllers
{
    public class UserProgressController : ControllerBase
    {
        public UserProgress Get()
        {
            return new UserProgress { CurrentLevel = 1 };
        }
    }
}