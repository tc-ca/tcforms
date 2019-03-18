using System;
using System.Configuration;
using Newtonsoft.Json;

namespace TCFORMS
{
    public static class Config
    {
        public const string MbunServerKey = "HTTP_SCSAML_NAME_ID_VALUE";
        public const string MbunSessionKey = "MBUN";
        public const string UserIdSessionKey = "USER_ID";

        public static readonly int ProgramId = Convert.ToInt32(ConfigurationManager.AppSettings["ProgramId"]);
        public static readonly int FormId = Convert.ToInt32(ConfigurationManager.AppSettings["FormId"]);
        public static readonly int AdminRoleId = Convert.ToInt32(ConfigurationManager.AppSettings["AdminRoleId"]);
        public static readonly int SystemUserId = Convert.ToInt32(ConfigurationManager.AppSettings["SystemUserId"]);
        public static readonly bool SimulateMbun = ConfigurationManager.AppSettings["SimulateMbun"] == "true";
        public static readonly string Mbun = ConfigurationManager.AppSettings["Mbun"];
        public static readonly string LanguageToggleUrl = ConfigurationManager.AppSettings["LanguageToggleUrl"];
        public static readonly string LogoutUrl = ConfigurationManager.AppSettings["LogoutUrl"];
        public static readonly string SenderEmail = ConfigurationManager.AppSettings["SenderEmail"];
        public static readonly string Environment = ConfigurationManager.AppSettings["Environment"];

        public static readonly JsonSerializerSettings JsonSettings  = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd" };
    }
}
