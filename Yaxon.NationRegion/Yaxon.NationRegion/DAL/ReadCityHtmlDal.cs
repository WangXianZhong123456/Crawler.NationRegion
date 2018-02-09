using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.DAL.Province;
using Yaxon.NationRegion.Model;
using Yaxon.NationRegion.DAL.City;
using System.Threading;

namespace Yaxon.NationRegion.DAL
{
    class ReadCityHtmlDal
    {
        public static string GetCityHtmlContent(int sleepTime)
        {
            List<BasicModel> list = NationRegionOperaSql.GetTopBasicModelByLevel(1);
            Thread.Sleep(sleepTime); /*我好累,休息一下!*/
            foreach (BasicModel model in list)
            {
                string htmlString = WebHandler.GetHtmlStr(Const.WebSiteUri + "//" + model.Href + ".html", "Default");
                if (htmlString == "")
                {
                    if (!NationRegionOperaSql.IsExistsExpetionByUri(Const.WebSiteUri + "//" + model.Href + ".html", 2))
                    {
                        NationRegionOperaSql.InsertExpetion(Const.WebSiteUri + "//" + model.Href + ".html", 2);
                    }
                    continue;
                }
                List<BasicModel> cityModels = AnalysisCityHtmlstr.GetCityModel(htmlString);
                string insertStr = string.Format("INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
                foreach (BasicModel cityModel in cityModels)
                {
                    insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                   cityModel.ParentNode, cityModel.Node, cityModel.Code,
                                                   cityModel.Name, cityModel.Href, 0, cityModel.Level);

                }
                NationRegionOperaSql.ExecuteSql(insertStr.Substring(0, insertStr.Length - 1));
            }


            return "";
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="sleepTime"></param>
        /// <returns></returns>
        public static string GetExceptionCityHtmlContent(string Uri)
        {
            string htmlString = WebHandler.GetHtmlStr(Uri, "Default");
            if (htmlString == "")
            {
                if (!NationRegionOperaSql.IsExistsExpetionByUri(Uri, 2))
                {
                    NationRegionOperaSql.InsertExpetion(Uri, 2);
                }
                return "";
            }
            List<BasicModel> cityModels = AnalysisCityHtmlstr.GetCityModel(htmlString);

            string insertStr = string.Format(@"
                    CREATE TABLE #Temp_NationRegion(
	                            [ParentNode] [varchar](50) NULL,
	                            [Node] [varchar](50) NULL,
	                            [Code] [varchar](50) NULL,
	                            [TypeCode] [varchar](50) NULL,
	                            [Name] [varchar](50) NULL,
	                            [Href] [varchar](200) NULL,
	                            [IsLowerUp] [int] NULL,
	                            [Level] [int] NULL)
                    INSERT INTO #Temp_NationRegion([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
            foreach (BasicModel cityModel in cityModels)
            {
                insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                               cityModel.ParentNode, cityModel.Node, cityModel.Code,
                                               cityModel.Name, cityModel.Href, 0, cityModel.Level);

            }
            insertStr = insertStr.Substring(0, insertStr.Length - 1);
            insertStr += @"

                    INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level])
                    SELECT [ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level] FROM #Temp_NationRegion a
                    WHERE NOT EXISTS(SELECT 1 FROM dbo.UCML_NationRegion b WHERE a.Code=b.Code)
                    DROP TABLE #Temp_NationRegion ";
            NationRegionOperaSql.ExecuteSql(insertStr.Substring(0, insertStr.Length - 1));


            return "";
        }
    }
}
