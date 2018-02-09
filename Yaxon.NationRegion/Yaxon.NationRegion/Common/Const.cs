using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Yaxon.NationRegion.Common
{
    public class Const
    {

        public static readonly string WebSiteUri = ConfigurationManager.AppSettings["WebSiteUri"].ToString();

    }
}
