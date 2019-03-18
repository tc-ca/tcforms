using System.Configuration;

namespace TCWebAPI
{
    public static class Config
    {
        public static readonly string TcFormsSecret = ConfigurationManager.AppSettings["TcFormsSecret"];
        public static readonly string TcForms = ConfigurationManager.AppSettings["TcForms"];
        public static readonly string IdSrvBaseUrl = ConfigurationManager.AppSettings["IdSrvBaseUrl"];
    }
}