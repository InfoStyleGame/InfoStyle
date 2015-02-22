using Api.Helpers;
using Api.Models;

namespace Api.Controllers
{
    public class UserInfoController : ControllerBase
    {
        public UserInfoModel Get()
        {
            var userId = VkAuthHelper.GetCurrentUserId(Request.Headers);
            if (null == userId)
                return new UserInfoModel { Name = "Stranger", PictureSrc = "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-xaf1/v/t1.0-1/c21.21.259.259/s50x50/1004617_1386999118185935_137709484_n.png?oh=a3913720c6b3b1cb860a828a6c045608&oe=554C1ACC&__gda__=1435102303_e5896d28895204f4abaca0a84c23acf3" };
            return VkApiHelper.GetUserInfo(userId);
        }
    }
}