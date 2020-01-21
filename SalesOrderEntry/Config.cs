using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public static class Config
    {

        public static string DnsIdentity { get { return ConfigurationManager.AppSettings["hostName"]; } }

        public static string epicorUserID { get { return ConfigurationManager.AppSettings["epicorUserID"].ToString(); } }

        public static string epiorUserPassword { get { return ConfigurationManager.AppSettings["epiorUserPassword"]; } }

        public static string hostName { get { return ConfigurationManager.AppSettings["hostName"]; } }

        public static string environment { get { return ConfigurationManager.AppSettings["environment"]; } }

        public static string Company { get { return ConfigurationManager.AppSettings["Company"]; } }

        public static string Plant { get { return ConfigurationManager.AppSettings["Plant"]; } }

        public static string ConfigFile { get { return ConfigurationManager.AppSettings["ConfigFile"]; } }

    }
}
