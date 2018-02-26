using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.ObtainBaiDuAddress.Model;
using Yaxon.ObtainBaiDuAddress.Common;
using System.Data;

namespace Yaxon.ObtainBaiDuAddress.DAL
{
    class ShopDal
    {

        /// <summary>
        /// 获取未执行的包括异常的数据
        /// </summary>
        /// <returns></returns>
        public static List<ShopModel> GetTopShopModel()
        {
            string strSql = string.Format(@"SELECT TOP 500 * FROM dbo.ShopInfo_gps 
                                            WHERE ISNULL(IsUpload,0)!=1 AND longitude NOT IN ('.000000','')");
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            List<ShopModel> list = new List<ShopModel>();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    list.Add(new ShopModel
                    {
                        ShopID = Convert.ToInt32(dr["ShopID"].ToString()),
                        Longitude = Convert.ToDouble(dr["Longitude"].ToString()),
                        Latitude = Convert.ToDouble(dr["Latitude"].ToString())
                    });
                }
                catch
                {
                    continue;
                }

            }
            return list;
        }

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(ShopModel model, int state)
        {
            string strSql = string.Format(@"UPDATE [dbo].[ShopInfo_gps] 
                                            SET [Province] = '{0}'
                                                ,[CityID] = '{1}'
                                                ,[County] = '{2}'
                                                ,[Town]='{3}'
                                                ,[Address] = '{4}'
                                                ,[IsUpload] = '{5}'
                                                ,[bd_Longitude] = '{6}'
                                                ,[bd_Latitude] = '{7}'
                                                ,[Uri] = '{8}'
                                            WHERE [ShopID] = '{9}'",
                                          model.Province, model.CityID, model.County, model.Town, model.Address, state, model.bd_Longitude,
                                          model.bd_Latitude, model.Uri, model.ShopID);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateLongLat(ShopModel model, int state)
        {
            string strSql = string.Format(@"UPDATE [dbo].[ShopInfo_gps] 
                                            SET [bd_Longitude] = '{0}'
                                                ,[bd_Latitude] = '{1}'
                                                ,[IsUpload] ='{2}'
                                                ,[Uri] = '{3}'
                                            WHERE [ShopID] = '{4}'",
                                          model.bd_Longitude, model.bd_Latitude, state, model.Uri, model.ShopID);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateState(int shopID, string exceptionUri, int state)
        {
            string strSql = string.Format(@"UPDATE [dbo].[ShopInfo_gps] 
                                            SET [IsUpload] = '{0}'
                                                ,[Uri]='{1}'
                                            WHERE [ShopID] = '{2}'",
                                            state, exceptionUri, shopID);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


        /*public static bool IsExsistException(int ShopID)
        {
            string strSql = string.Format(@"SELECT * FROM dbo.ShopInfo_gps_Exception WHERE ShopID = {0} AND ISNULL(IsUpload,0)=0", ShopID);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            bool result = false;
            if (dt.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public static int InsertException(ShopModel model)
        {
            string strSql = string.Format(@"INSERT INTO [dbo].[ShopInfo_gps_Exception]
                                            ([ShopID],[Longitude],[Latitude],[IsUpload],[CreateTime],[ModifyTime])
                                            VALUES('{0}','{1}','{2}','{3}','{4}',GETDATE(),GETDATE()) ",
                                        model.ShopID, model.Longitude, model.Latitude, 0);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        */

    }
}
