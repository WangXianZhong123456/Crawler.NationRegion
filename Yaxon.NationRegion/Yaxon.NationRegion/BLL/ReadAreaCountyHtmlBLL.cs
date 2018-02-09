using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL;

namespace Yaxon.NationRegion.BLL
{
    public class ReadAreaCountyHtmlBLL
    {
        public static string GetAreaCountyHtmlContent(int sleepTime)
        {
            return ReadAreaCountyHtmlDal.GetAreaCountyHtmlContent(sleepTime);
        }
    }
}
