using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.Model;

namespace Yaxon.NationRegion.DAL.City
{
    public class AnalysisCityHtmlstr
    {
        public static List<BasicModel> GetCityModel(string strWebContent)
        {

            List<BasicModel> basicModel = new List<BasicModel>();//定义1个列表用于保存结果

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strWebContent);
            string cityXpath = "//tr[@class='citytr']//td//a";

            int cityTotalCount = htmlDoc.DocumentNode.SelectNodes(cityXpath).Count; /*获取网页有多少个城市*/

            /*遍历*/
            string cityCode = "";
            string cityName = "";
            string cityHref = "";
            for (int i = 0; i < cityTotalCount; i++)
            {
                if (i % 2 == 0)
                {
                    cityCode = htmlDoc.DocumentNode.SelectNodes(cityXpath)[i].InnerText;
                }
                else
                {
                    cityName = htmlDoc.DocumentNode.SelectNodes(cityXpath)[i].InnerText;
                    cityHref = htmlDoc.DocumentNode.SelectNodes(cityXpath)[i].Attributes["href"].Value;
                }
                if (i % 2 != 0)
                {
                    basicModel.Add(new BasicModel
                    {
                        ParentNode = cityHref.Split('/')[0],
                        Node = cityHref.Split('/')[1].Split('.')[0],
                        Code = cityCode,
                        Href = cityHref.Split('.')[0],
                        Name = cityName,
                        Level = 2
                    });
                }

            }

            return basicModel;
        }
    }
}
