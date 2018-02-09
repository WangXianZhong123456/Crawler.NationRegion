using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;

namespace Yaxon.NationRegion.DAL.Province
{
    public class AnalysisProvHtmlstr
    {

        public static List<BasicModel> GetProvinceModel(string strWebContent)
        {

            List<BasicModel> basicModel = new List<BasicModel>();//定义1个列表用于保存结果

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strWebContent);
            string proXpath = "//tr[@class='provincetr']//td//a";
            int proTotalCount = htmlDoc.DocumentNode.SelectNodes(proXpath).Count; /*获取网页有多少个省份*/
            /*遍历*/
            for (int i = 0; i < proTotalCount; i++)
            {
                string proName = htmlDoc.DocumentNode.SelectNodes(proXpath)[i].InnerText;
                string proAttributesHref = htmlDoc.DocumentNode.SelectNodes(proXpath)[i].Attributes["href"].Value;

                basicModel.Add(new BasicModel
                {
                    ParentNode = "-1",
                    Node = proAttributesHref.Split('.')[0],
                    Code = proAttributesHref.Split('.')[0],
                    Href = proAttributesHref.Split('.')[0],
                    Name = proName,
                    Level = 1

                });
            }

            return basicModel;
        }
    }
}
