using System.Configuration;

namespace infostyle.Configuration
{
    public static class Config
    {
        public static bool NeedToStartFakeApi
        {
            get { return !string.IsNullOrEmpty(RealApiUrl); }
        }

        public static string RealApiUrl
        {
            get { return ConfigurationManager.AppSettings["apiUrl"]; }
        }
    }
}