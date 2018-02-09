using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.DAL.Town;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;
using System.Threading;

namespace Yaxon.NationRegion.DAL
{
    public class ReadVillageHtmlDal
    {
        public static string GetVillageHtmlContent(int sleepTime, int index)
        {
            List<BasicModel> list = NationRegionOperaSql.GetTopBasicModelByLevel(4);
            if (list.Count <= 0)
            {
                Form1.readVillageHtmlThreadCount[index] = 1;
            }
            //Thread.Sleep(sleepTime);/*我好累,休息一下!*/
            foreach (BasicModel model in list)
            {
                string htmlString = "";
                try
                {
                    //这个比较特殊点
                    string uri = Const.WebSiteUri + "//" + model.Node.Substring(0, 2) + "//" +
                                 model.Node.Substring(2, 2) + "//" + model.Href + ".html";
                    htmlString = WebHandler.GetHtmlStr(uri, "Default");
                    if (htmlString == "")
                    {
                        if (!NationRegionOperaSql.IsExistsExpetionByUri(uri, 5))
                        {
                            NationRegionOperaSql.InsertExpetion(uri, 5);
                        }
                        continue;
                    }
                    List<VillageModel> villageModels = AnalysisVillageHtmlstr.GetVillageModel(htmlString, model.Node);
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
                    INSERT INTO #Temp_NationRegion([ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
                    foreach (VillageModel villageModel in villageModels)
                    {
                        insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'),",
                                                    villageModel.ParentNode, villageModel.Node, villageModel.Code,
                                                    villageModel.TypeCode, villageModel.Name, villageModel.Href, 0, villageModel.Level);
                    }
                    insertStr = insertStr.Substring(0, insertStr.Length - 1);
                    insertStr += @"

                    INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level])
                    SELECT [ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level] FROM #Temp_NationRegion a
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
            }

            return "";
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetExceptionVillageHtmlContent(string uri)
        {

            string htmlString = "";
            try
            {
                //这个比较特殊点

                htmlString = WebHandler.GetHtmlStr(uri, "Default");
                if (htmlString == "")
                {
                    if (!NationRegionOperaSql.IsExistsExpetionByUri(uri, 5))
                    {
                        NationRegionOperaSql.InsertExpetion(uri, 5);
                    }
                    return "";

                }
                int startIndex = uri.LastIndexOf('/') + 1;
                string ParentNode = uri.Substring(startIndex, uri.Length - startIndex).Split('.')[0];
                List<VillageModel> villageModels = AnalysisVillageHtmlstr.GetVillageModel(htmlString, ParentNode);
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
                    INSERT INTO #Temp_NationRegion([ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level]) VALUES ");
                foreach (VillageModel villageModel in villageModels)
                {
                    insertStr += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'),",
                                                villageModel.ParentNode, villageModel.Node, villageModel.Code,
                                                villageModel.TypeCode, villageModel.Name, villageModel.Href, 0, villageModel.Level);
                }
                insertStr = insertStr.Substring(0, insertStr.Length - 1);
                insertStr += @"

                    INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level])
                    SELECT [ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level] FROM #Temp_NationRegion a
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
