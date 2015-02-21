using System.IO;
using System.Net;
using Api.Models;
using Newtonsoft.Json.Linq;

namespace Api.Helpers
{
    public class VkApiHelper
    {
        public static UserInfoModel GetUserInfo(string userId)
        {
            var vkInfo = GetJson("https://api.vk.com/method/getProfiles?uids=" + userId + "&fields=photo_50");
            var humanInfo = ((JArray)vkInfo["response"])[0];
            return new UserInfoModel
            {
                Name = (string) humanInfo["first_name"],
                PictureSrc = (string) humanInfo["photo_50"]
            };
        }

        private static JObject GetJson(string url, int timeout = 5000)
        {
            var request = WebRequest.Create(url);
            request.Timeout = timeout;
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream == null)
                throw new WebException();
            return JObject.Parse(new StreamReader(stream).ReadToEnd());
        }
    }
}