using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Yaxon.ObtainBaiDuAddress.Common
{
    public class Const
    {

        public static readonly string BaiduAK = ConfigurationManager.AppSettings["baiduAK"].ToString();

    }
}
