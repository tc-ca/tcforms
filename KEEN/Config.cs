using System;
using System.Configuration;

namespace KEEN
{
    public static class Config
    {
        public static readonly int SystemUserId = Convert.ToInt32(ConfigurationManager.AppSettings["SystemUserId"]);
    }
}