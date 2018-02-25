using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.ObtainBaiDuAddress.Model;

namespace Yaxon.ObtainBaiDuAddress.Common
{
    public class BaiduCoordinateTrans
    {
        static double pi = 3.14159265358979324;
        static double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        /**
        * 转换百度坐标
        * @param {*} gg_lon 
        * @param {*} gg_lat 
        */
        public static BaiDuCoordinate GCJ2Baidu(double gg_lon, double gg_lat)
        {
            double bd_lon, bd_lat;
            double x = gg_lon, y = gg_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            bd_lon = z * Math.Cos(theta) + 0.0065;
            bd_lat = z * Math.Sin(theta) + 0.006;
            BaiDuCoordinate obj = new BaiDuCoordinate { Latitude = bd_lat, Longitude = bd_lon };
            return obj;
        }

        /**
        * 转换手机坐标
        * @param {*} gg_lon 
        * @param {*} gg_lat 
        */
        public static BaiDuCoordinate Baidu2GCJ(double bd_lon, double bd_lat)
        {
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            BaiDuCoordinate obj = new BaiDuCoordinate { Latitude = bd_lat, Longitude = bd_lon };
            return obj;
        }
    }
}
