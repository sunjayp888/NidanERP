using System;
using System.Configuration;

namespace Nidan.Business
{
    public static class ConfigHelper
    {
        public static string DefaultConnection => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static int CacheTimeout => Convert.ToInt32(ConfigurationManager.AppSettings["CacheTimeout"]);
        public static string EmailDefaultFromAddress => ConfigurationManager.AppSettings["EmailDefaultFromAddress"].ToString();
        public static string OverrideEmailAddresses => ConfigurationManager.AppSettings["OverrideEmailAddresses"].ToString();
    }
}
