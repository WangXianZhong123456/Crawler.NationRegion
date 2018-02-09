using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;

namespace Yaxon.NationRegion.DAL.Town
{
    public class AnalysisTownHtmlstr
    {
        public static List<BasicModel> GetTownModel(string strWebContent, string parentNode)
        {

            List<BasicModel> basicModel = new List<BasicModel>();//定义1个列表用于保存结果

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strWebContent);
            string townXpath = "//tr[@class='towntr']//td//a";

            //int cityTotalCount = htmlDoc.DocumentNode.SelectNodes(townXpath + "//td").Count; /*获取网页有多少个城市*/
            int townTagATotalCount = htmlDoc.DocumentNode.SelectNodes(townXpath).Count;
            /*遍历*/
            string townCode = "";
            string townName = "";
            string townHref = "";
            for (int i = 0; i < townTagATotalCount; i++)
            {
                if (i % 2 == 0)
                {
                    townCode = htmlDoc.DocumentNode.SelectNodes(townXpath)[i].InnerText;

                }
                else
                {
                    townName = htmlDoc.DocumentNode.SelectNodes(townXpath)[i].InnerText;
                    townHref = htmlDoc.DocumentNode.SelectNodes(townXpath)[i].Attributes["href"].Value;
                }
                if (i % 2 != 0)
                {
                    basicModel.Add(new BasicModel
                    {
                        ParentNode = parentNode,
                        Node = townHref.Split('/')[1].Split('.')[0],
                        Code = townCode,
                        Href = townHref.Split('.')[0],
                        Name = townName,
                        Level = 4
                    });
                }

            }

            return basicModel;
        }
    }
}
