using System;

namespace com.strava.v3.api.Common
{
    public static class Global
    {
        private static string authfile = System.IO.Directory.GetCurrentDirectory() + "\\auth";
        public static string Code { get; set; }

        public static string Token { get; set; }

        public static string ExpiresIn { get; set; }

        public static DateTime ExpiresAt { get; set; }

        public static string RefreshToken { get; set; }

        public static void SaveValues()
        {
            using (IniFile iniFile = new IniFile(authfile))
            {
                iniFile.IniWriteValue("tokens", "auth", Token);
                iniFile.IniWriteValue("expiry", "in", ExpiresIn);
                iniFile.IniWriteValue("expiry", "at", ExpiresAt.ToString());
                iniFile.IniWriteValue("tokens", "refresh", RefreshToken);
            }
        }

        public static void LoadValues()
        {
            if (System.IO.File.Exists(authfile))
            {
                using (IniFile iniFile = new IniFile(authfile))
                {
                    Token = iniFile.IniReadValue("tokens", "auth");
                    ExpiresAt = DateTime.Parse(iniFile.IniReadValue("expiry", "at"));
                }
            }
            else { throw new System.IO.FileNotFoundException($"The requested file {authfile} does not exist"); }
        }
    }
}
