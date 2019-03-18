using System.Configuration;

namespace KEEN.IDSrv
{
    public static class Config
    {
        public static readonly string TcFormsSecret = ConfigurationManager.AppSettings["TcFormsSecret"];
        public static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"];
        public static readonly string IdSrvBaseUrl = ConfigurationManager.AppSettings["IdSrvBaseUrl"];
    }
}