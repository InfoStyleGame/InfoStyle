using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Api.Helpers
{
    public class VkAuthHelper
    {
        public static string GetCurrentUserId(HttpRequestHeaders requestParams)
        {
            const string cookieKey = "vk_app_" + VkAuthParameters.VkAppId;
            var cookie = requestParams.GetCookies(cookieKey).FirstOrDefault();
            if (cookie == null)
                return null;

            try
            {
                var parameters = VkAuthParameters.Parse(cookie[cookieKey].Value);
                VerifyParameters(parameters);
                return parameters.UserId;
            }
            catch (Exception ex)
            {
                return null;
            }
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