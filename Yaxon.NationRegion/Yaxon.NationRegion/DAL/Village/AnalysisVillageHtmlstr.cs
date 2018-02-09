using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;

namespace Yaxon.NationRegion.DAL.Town
{
    public class AnalysisVillageHtmlstr
    {
        public static List<VillageModel> GetVillageModel(string strWebContent, string parentNode)
        {

            List<VillageModel> basicModel = new List<VillageModel>();//定义1个列表用于保存结果

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strWebContent);
            string villageXpath = "//tr[@class='villagetr']";

            int villageTotalCount = htmlDoc.DocumentNode.SelectNodes(villageXpath).Count;
            /*遍历*/
            for (int i = 0; i < villageTotalCount; i++)
            {
                string villageCode = htmlDoc.DocumentNode.SelectNodes(villageXpath)[i].ChildNodes[0].InnerText;
                string villageTypeCode = htmlDoc.DocumentNode.SelectNodes(villageXpath)[i].ChildNodes[1].InnerText;
                string villageName = htmlDoc.DocumentNode.SelectNodes(villageXpath)[i].ChildNodes[2].InnerText;

                basicModel.Add(new VillageModel
                {
                    ParentNode = parentNode,
                    Node = villageCode,
                    Code = villageCode,
                    Href = "",
                    Name = villageName,
                    Level = 5,
                    TypeCode = villageTypeCode
                });

            }

            return basicModel;
        }
    }
}
