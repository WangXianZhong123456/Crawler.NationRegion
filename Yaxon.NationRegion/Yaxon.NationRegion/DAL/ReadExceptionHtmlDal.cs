using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Yaxon.NationRegion.DAL
{
    public class ReadExceptionHtmlDal
    {
        /// <summary>
        /// 处理全部异常数据
        /// </summary>
        public static void GetAllExpetionData()
        {
            DataTable dt = NationRegionOperaSql.GetAllExpetionData();
            foreach (DataRow dr in dt.Rows)
            {
                GetExceptionHtmlContent(dr["Uri"].ToString(), int.Parse(dr["Level"].ToString()));
            }
        }

        /// <summary>
        /// 单独处理
        /// </summary>
        /// <param name="Uri"></param>
        /// <param name="Level"></param>
        public static void GetExceptionHtmlContent(string Uri, int Level)
        {
            if (Level == 1)
            {
                //
            }
            else if (Level == 2)
            {
                ReadCityHtmlDal.GetExceptionCityHtmlContent(Uri);
            }
            else if (Level == 3)
            {
                ReadAreaCountyHtmlDal.GetAreaCountyHtmlContent(0);
                ReadAreaCountyHtmlDal.GetExceptionAreaCountyHtmlContent(Uri);
            }
            else if (Level == 4)
            {
                ReadTownHtmlDal dal = new ReadTownHtmlDal();
                dal.GetTownHtmlContent(0, 0);
                dal.GetExceptionTownHtmlContent(Uri);
            }
            else if (Level == 5)
            {
                ReadVillageHtmlDal.GetVillageHtmlContent(0, 0);
                ReadVillageHtmlDal.GetExceptionVillageHtmlContent(Uri);
            }
        }
    }
}
