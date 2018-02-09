using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.DAL.Province;
using Yaxon.NationRegion.Model;

namespace Yaxon.NationRegion.DAL
{
    class ReadProvinceHtmlDal
    {
        public static string GetProvinceHtmlContent()
        {

            if (NationRegionOperaSql.IsExistsByLevel(1))
                return "0";
            string htmlString = WebHandler.GetHtmlStr(Const.WebSiteUri, "Default");
            if (htmlString == "")
            {
                if (!NationRegionOperaSql.IsExistsExpetionByUri(Const.WebSiteUri, 1))
                {
                    NationRegionOperaSql.InsertExpetion(Const.WebSiteUri, 1);
                }
                return "";
            }
            List<BasicModel> provinceModel = AnalysisProvHtmlstr.GetProvinceModel(htmlString);
            string insertStr = string.Format("INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
            foreach (BasicModel model in provinceModel)
            {
                insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                  model.ParentNode, model.Node, model.Code,
                                                  model.Name, model.Href, 0, model.Level);
            }
            NationRegionOperaSql.ExecuteSql(insertStr.Substring(0, insertStr.Length - 1));
            return "";
        }

    }
}
