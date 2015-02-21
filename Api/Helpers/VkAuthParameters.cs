using System;
using System.Linq;

namespace Api.Helpers
{
    public class VkAuthParameters
    {
        public const string VkAppId = "4793375";
        public const string SecretString = "lDJqz0Cr2Q3FZqZ6P0MN";

        public string Expire { get; set; } 
        public string UserId { get; set; }
        public string Secret { get; set; }
        public string SessionId { get; set; }
        public string Sig { get; set; }
        public string StringForMd5 { get; set; }

        public static VkAuthParameters Parse(string s)
        {
            var parameters = s.Split('&').Select(p => p.Split('=')).ToDictionary(p => p[0], p => p[1]);
            var result = new VkAuthParameters
                {
                    Expire = parameters["expire"],
                    Secret = parameters["secret"],
                    SessionId = parameters["sid"],
                    Sig = parameters["sig"],
                    UserId = parameters["mid"]
                };
            result.StringForMd5 = String.Format("expire={0}mid={1}secret={2}sid={3}{4}", result.Expire,
                                                result.UserId, result.Secret, result.SessionId, SecretString);
            return result;
        }
    }
}