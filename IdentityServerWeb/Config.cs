using System.Configuration;

namespace IdentityServerWeb
{
    public static class Config
    {
        public static readonly string TcFormsSecret = ConfigurationManager.AppSettings["TcFormsSecret"];
        public static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"];
    }
}