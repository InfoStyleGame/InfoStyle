using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Api.Managers;
using EF;
using log4net;

namespace Api.Helpers
{
    public class VkAuthHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VkAuthHelper));

        public static User GetCurrentUser(HttpRequestHeaders requestParams)
        {
            var vkLogin = GetCurrentUserVkLogin(requestParams);
            if (vkLogin == null)
                return null;
            return UserManager.GetOrCreateUser(vkLogin);
        }

        private static string GetCurrentUserVkLogin(HttpRequestHeaders requestParams)
        {
            //todo выпилить на боевой
            var debugUser = GetUserIdDebugEdition(requestParams);
            if (debugUser != null)
                return debugUser;

            const string cookieKey = "vk_app_" + VkAuthParameters.VkAppId;
            var cookie = requestParams.GetCookies(cookieKey).FirstOrDefault();
            if (cookie == null)
                return null;

            try
            {
                var parameters = VkAuthParameters.Parse(cookie[cookieKey].Values.ToString());
                VerifyParameters(parameters);
                return parameters.UserId;
            }
            catch (Exception ex)
            {
                log.Warn("Strange user", ex);
                return null;
            }
        }

        private static string GetUserIdDebugEdition(HttpRequestHeaders requestParams)
        {
            const string cookieKey = "userId";
            var cookie = requestParams.GetCookies(cookieKey).FirstOrDefault();
            if (cookie == null)
                return null;
            return cookie[cookieKey].Values.ToString();
        }

        private static void VerifyParameters(VkAuthParameters parameters)
        {
            if (parameters.Sig != GetMd5Hash(parameters.StringForMd5))
                throw new Exception(string.Format("Bad Md5. Sig: {0} , StringFormMD5: {1}.", parameters.Sig, parameters.StringForMd5));
        }

        private static string GetMd5Hash(string s)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
                sBuilder.Append(t.ToString("x2"));
            return sBuilder.ToString();
        }
    }
}