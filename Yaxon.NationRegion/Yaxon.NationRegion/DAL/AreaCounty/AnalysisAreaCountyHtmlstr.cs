using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;


namespace Yaxon.NationRegion.DAL.AreaCounty
{
    public class AnalysisAreaCountyHtmlstr
    {
        public static List<BasicModel> GetAreaCountyModel(string strWebContent, string parentNode)
        {

            List<BasicModel> basicModel = new List<BasicModel>();//定义1个列表用于保存结果

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strWebContent);
            string countyXpath = "//tr[@class='countytr']//td//a";

            int countyTagATotalCount = htmlDoc.DocumentNode.SelectNodes(countyXpath).Count;
            /*遍历*/
            string countyCode = "";
            string countyName = "";
            string countyHref = "";
            for (int i = 0; i < countyTagATotalCount; i++)
            {
                if (i % 2 == 0)
                {
                    countyCode = htmlDoc.DocumentNode.SelectNodes(countyXpath)[i].InnerText;

                }
                else
                {
                    countyName = htmlDoc.DocumentNode.SelectNodes(countyXpath)[i].InnerText;
                    countyHref = htmlDoc.DocumentNode.SelectNodes(countyXpath)[i].Attributes["href"].Value;
                }
                if (i % 2 != 0)
                {
                    basicModel.Add(new BasicModel
                    {
                        ParentNode = parentNode,
                        Node = countyHref.Split('/')[1].Split('.')[0],
                        Code = countyCode,
                        Href = countyHref.Split('.')[0],
                        Name = countyName,
                        Level = 3
                    });
                }

            }

            return basicModel;
        }
    }
}
