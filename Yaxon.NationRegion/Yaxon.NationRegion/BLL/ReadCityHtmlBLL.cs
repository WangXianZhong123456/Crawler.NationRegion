using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL;

namespace Yaxon.NationRegion.BLL
{
    public class ReadCityHtmlBLL
    {
        public static string GetCityHtmlContent(int sleepTime)
        {
            return ReadCityHtmlDal.GetCityHtmlContent(sleepTime); ;
        }
    }
}
