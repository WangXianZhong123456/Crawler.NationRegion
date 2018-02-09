using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL;

namespace Yaxon.NationRegion.BLL
{
    class ReadProvinceHtmlBLL
    {
        public static string GetProvinceHtmlContent()
        {
            return ReadProvinceHtmlDal.GetProvinceHtmlContent(); ;
        }
    }
}
