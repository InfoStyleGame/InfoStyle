using System;
using System.Linq;
using Api.Helpers;
using EF;

namespace Api.Managers
{
    public static class UserManager
    {
        public static User GetOrCreateUser(string vkLogin)
        {
            using (var db = new InfostyleEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.VKLogin == vkLogin);
                if (user != null)
                    return user;
                var newUser = new User
                {
                    Name = VkApiHelper.GetUserInfo(vkLogin).Name,
                    VKLogin = vkLogin,
                    LastDailyEditTime = DateTime.Now,
                    Rank = 1000
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser;
            }
        }
    }
}