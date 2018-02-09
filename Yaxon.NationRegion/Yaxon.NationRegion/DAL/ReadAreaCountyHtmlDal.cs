using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.DAL.Province;
using Yaxon.NationRegion.Model;
using Yaxon.NationRegion.DAL.AreaCounty;
using System.Threading;

namespace Yaxon.NationRegion.DAL
{
    class ReadAreaCountyHtmlDal
    {
        public static string GetAreaCountyHtmlContent(int sleepTime)
        {
            List<BasicModel> list = NationRegionOperaSql.GetTopBasicModelByLevel(2);
            Thread.Sleep(sleepTime); /*我好累,休息一下!*/
            foreach (BasicModel model in list)
            {
                string htmlString = "";
                try
                {
                    htmlString = WebHandler.GetHtmlStr(Const.WebSiteUri + "//" + model.Href + ".html", "Default");
                    if (htmlString == "")
                    {
                        if (!NationRegionOperaSql.IsExistsExpetionByUri(Const.WebSiteUri + "//" + model.Href + ".html", 3))
                        {
                            NationRegionOperaSql.InsertExpetion(Const.WebSiteUri + "//" + model.Href + ".html", 3);
                        }
                        continue;
                    }
                    List<BasicModel> areaModels = AnalysisAreaCountyHtmlstr.GetAreaCountyModel(htmlString, model.Node);
                    string insertStr = string.Format("INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
                    foreach (BasicModel areaModel in areaModels)
                    {
                        insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                       areaModel.ParentNode, areaModel.Node, areaModel.Code,
                                                       areaModel.Name, areaModel.Href, 0, areaModel.Level);

                    }
                    NationRegionOperaSql.ExecuteSql(insertStr.Substring(0, insertStr.Length - 1));
                }
                catch (Exception ex)
                {
                    LogUtil.WriteInfo(ex.Message);
                    LogUtil.WriteInfo(ex.StackTrace);
                    LogUtil.WriteInfo(htmlString);
                }
            }

            return "";
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="sleepTime"></param>
        /// <returns></returns>
        public static string GetExceptionAreaCountyHtmlContent(string Uri)
        {
            string htmlString = "";
            try
            {
                htmlString = WebHandler.GetHtmlStr(Uri, "Default");
                if (htmlString == "")
                {
                    if (!NationRegionOperaSql.IsExistsExpetionByUri(Uri, 3))
                    {
                        NationRegionOperaSql.InsertExpetion(Uri, 3);
                    }
                    return "";
                }
                int startIndex = Uri.LastIndexOf('/') + 1;  
                string ParentNode = Uri.Substring(startIndex, Uri.Length - startIndex).Split('.')[0];
                List<BasicModel> areaModels = AnalysisAreaCountyHtmlstr.GetAreaCountyModel(htmlString, ParentNode);
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
                foreach (BasicModel areaModel in areaModels)
                {
                    insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                   areaModel.ParentNode, areaModel.Node, areaModel.Code,
                                                   areaModel.Name, areaModel.Href, 0, areaModel.Level);

                }
                insertStr = insertStr.Substring(0, insertStr.Length - 1);
                insertStr += @"

                    INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level])
                    SELECT [ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level] FROM #Temp_NationRegion a
                    WHERE NOT EXISTS(SELECT 1 FROM dbo.UCML_NationRegion b WHERE a.Code=b.Code)
                    DROP TABLE #Temp_NationRegion ";
                NationRegionOperaSql.ExecuteSql(insertStr.Substring(0, insertStr.Length - 1));
            }
            catch (Exception ex)
            {
                LogUtil.WriteInfo(ex.Message);
                LogUtil.WriteInfo(ex.StackTrace);
                LogUtil.WriteInfo(htmlString);
            }

            return "";
        }
    }
}
