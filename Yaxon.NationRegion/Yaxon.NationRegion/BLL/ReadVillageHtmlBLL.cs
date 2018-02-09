using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL;

namespace Yaxon.NationRegion.BLL
{
    public class ReadVillageHtmlBLL
    {
        public static string GetVillageHtmlContent(int sleepTime, int index)
        {
            return ReadVillageHtmlDal.GetVillageHtmlContent(sleepTime, index);
        }
    }
}
