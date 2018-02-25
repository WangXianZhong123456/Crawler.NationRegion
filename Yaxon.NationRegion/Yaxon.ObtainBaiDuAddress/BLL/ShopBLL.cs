using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.ObtainBaiDuAddress.Model;
using Yaxon.ObtainBaiDuAddress.DAL;
using Yaxon.ObtainBaiDuAddress.Common;
using System.Xml;
using System.Threading;

namespace Yaxon.ObtainBaiDuAddress.BLL
{
    public class ShopBLL
    {
        public static int GetTopShopModel()
        {
            string exceptionUri = "";
            int exceptionShopID = 0;
            List<ShopModel> list = ShopDal.GetTopShopModel();
            if (list.Count == 0)
            {
                return 1;
            }
            //Thread.Sleep(5000);
            try
            {
                foreach (ShopModel model in list)
                {
                    BaiDuCoordinate bd_Coor = BaiduCoordinateTrans.GCJ2Baidu(model.Longitude, model.Latitude);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("http://api.map.baidu.com/geocoder/v2/?");
                    sb.Append("latest_admin=0&");
                    sb.Append("extensions_town=true&");
                    sb.Append("ak=" + Const.BaiduAK + "&");
                    sb.Append("callback=renderReverse&");
                    //sb.Append("location=22.939708,113.951647&");
                    sb.Append("location=" + bd_Coor.Latitude + "," + bd_Coor.Longitude + "&");
                    sb.Append("output=xml&");
                    sb.Append("pois=0");
                    exceptionUri = sb.ToString();
                    exceptionShopID = model.ShopID;
                    model.bd_Latitude = bd_Coor.Latitude;
                    model.bd_Longitude = bd_Coor.Longitude;
                    model.Uri = sb.ToString();
                    string result = WebHandler.GetHtmlStr(sb.ToString(), "UTF8");
                    if (result == "")
                    {
                        ShopDal.UpdateLongLat(model, 2);/*异常*/
                        continue;
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(result);
                    string statusPath = "/GeocoderSearchResponse/status";
                    string rootPath = "/GeocoderSearchResponse/result/addressComponent";
                    XmlElement root = doc.DocumentElement;
                    if (root.SelectNodes(statusPath)[0].InnerText == "0") // 成功
                    {
                        XmlNodeList listNodes = root.SelectNodes(rootPath);
                        string country = root.SelectNodes(rootPath + "/country")[0].InnerText;
                        string province = root.SelectNodes(rootPath + "/province")[0].InnerText;
                        string city = root.SelectNodes(rootPath + "/city")[0].InnerText;
                        string district = root.SelectNodes(rootPath + "/district")[0].InnerText;
                        string town = root.SelectNodes(rootPath + "/town")[0].InnerText;
                        string street = root.SelectNodes(rootPath + "/street")[0].InnerText;
                        model.Province = province;
                        model.CityID = city;
                        model.County = district;
                        model.Town = town;
                        model.Address = street;
                        ShopDal.Update(model, 1);

                    }
                    else
                    {
                        ShopDal.UpdateLongLat(model, 2);/*异常*/
                    }
                }

            }
            catch (Exception ex)
            {
                ShopDal.UpdateState(exceptionShopID, exceptionUri, 3);/*程序异常*/
                LogUtil.WriteInfo(ex.Message);
                LogUtil.WriteInfo("exceptionHtml=" + exceptionUri);
            }
            return 0;
        }
    }
}
