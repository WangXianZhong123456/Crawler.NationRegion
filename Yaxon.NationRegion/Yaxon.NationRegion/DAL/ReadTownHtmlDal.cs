using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.DAL.Province;
using Yaxon.NationRegion.Model;
using Yaxon.NationRegion.DAL.Town;
using System.Threading;

namespace Yaxon.NationRegion.DAL
{
    public class ReadTownHtmlDal
    {
        public string GetTownHtmlContent(int sleepTime, int index)
        {
            List<BasicModel> list = NationRegionOperaSql.GetTopBasicModelByLevel(3);
            if (list.Count <= 0)
            {
                Form1.readTownHtmlThreadCount[index] = 1;
            }
            //Thread.Sleep(sleepTime); /*我好累,休息一下!*/
            foreach (BasicModel model in list)
            {
                string htmlString = "";
                try
                {
                    //这个比较特殊点
                    string uri = Const.WebSiteUri + "//" + model.Node.Substring(0, 2) + "//" + model.Href + ".html";
                    htmlString = WebHandler.GetHtmlStr(uri, "Default");
                    if (htmlString == "")
                    {
                        if (!NationRegionOperaSql.IsExistsExpetionByUri(uri, 4))
                        {
                            NationRegionOperaSql.InsertExpetion(uri, 4);
                        }
                        continue;
                    }
                    List<BasicModel> townModels = AnalysisTownHtmlstr.GetTownModel(htmlString, model.Node);
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
                    foreach (BasicModel townModel in townModels)
                    {
                        insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                    townModel.ParentNode, townModel.Node, townModel.Code,
                                                    townModel.Name, townModel.Href, 0, townModel.Level);

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

                }
            }

            return "";
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public  string GetExceptionTownHtmlContent(string uri)
        {
            string htmlString = "";
            try
            {

                htmlString = WebHandler.GetHtmlStr(uri, "Default");
                if (htmlString == "")
                {
                    if (!NationRegionOperaSql.IsExistsExpetionByUri(uri, 4))
                    {
                        NationRegionOperaSql.InsertExpetion(uri, 4);
                    }
                    return "";
                }
                int startIndex = uri.LastIndexOf('/') + 1;
                string ParentNode = uri.Substring(startIndex, uri.Length - startIndex).Split('.')[0];
                List<BasicModel> townModels = AnalysisTownHtmlstr.GetTownModel(htmlString, ParentNode);
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
                foreach (BasicModel townModel in townModels)
                {
                    insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),",
                                                townModel.ParentNode, townModel.Node, townModel.Code,
                                                townModel.Name, townModel.Href, 0, townModel.Level);

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

            }


            return "";
        }
    }
}
