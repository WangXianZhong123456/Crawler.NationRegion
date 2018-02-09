using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaxon.NationRegion.Model;
using Yaxon.NationRegion.Common;
using System.Data;

namespace Yaxon.NationRegion.DAL
{
    public class NationRegionOperaSql
    {
        /// <summary>
        /// 插入BasicModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(BasicModel model)
        {
            string strSql = string.Format(@"INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[Name],[Href],[IsLowerUp],[Level])
                                            VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", model.ParentNode, model.Node, model.Code, model.Name, model.Href, 0, model.Level);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 插入VillageModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(VillageModel model)
        {
            string strSql = string.Format(@"INSERT INTO [dbo].[UCML_NationRegion]([ParentNode],[Node],[Code],[TypeCode],[Name],[Href],[IsLowerUp],[Level])
                                            VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", model.ParentNode, model.Node, model.Code,
                                                                                                    model.TypeCode, model.Name, model.Href, 0, model.Level);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 根据级别获取实体
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<BasicModel> GetBasicModelByLevel(int level)
        {
            string strSql = string.Format("SELECT * FROM dbo.UCML_NationRegion WHERE LEVEL = {0}", level);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            List<BasicModel> list = new List<BasicModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BasicModel
                {
                    ParentNode = dr["ParentNode"].ToString(),
                    Node = dr["Node"].ToString(),
                    Code = dr["Code"].ToString(),
                    Href = dr["Href"].ToString(),
                    Name = dr["Name"].ToString(),
                    Level = int.Parse(dr["Level"].ToString()),
                });
            }
            return list;

        }

        /// <summary>
        /// 根据级别获取实体->限制500
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<BasicModel> GetTopBasicModelByLevel(int level)
        {
            string strSql = string.Format(@"
                                            SELECT TOP 500 * INTO #TB_IsNotLowerUp FROM dbo.UCML_NationRegion WHERE IsLowerUp=0 AND LEVEL = {0};
                                            UPDATE  UCML_NationRegion
                                            SET IsLowerUp = 1 
                                            WHERE ID IN (SELECT ID FROM #TB_IsNotLowerUp );
                                            SELECT *FROM #TB_IsNotLowerUp;
                                            DROP TABLE #TB_IsNotLowerUp;", level);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            List<BasicModel> list = new List<BasicModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new BasicModel
                {
                    ParentNode = dr["ParentNode"].ToString(),
                    Node = dr["Node"].ToString(),
                    Code = dr["Code"].ToString(),
                    Href = dr["Href"].ToString(),
                    Name = dr["Name"].ToString(),
                    Level = int.Parse(dr["Level"].ToString()),
                });
            }
            return list;

        }

        /// <summary>
        /// 根据类别判断是否有记录存在(慎用)
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static bool IsExistsByLevel(int level)
        {
            string strSql = string.Format(@"SELECT * FROM dbo.UCML_NationRegion WHERE LEVEL = {0};", level);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            bool results = false;
            if (dt.Rows.Count > 0)
            {
                results = true;
            }
            return results;
        }
        /// <summary>
        /// sql语句执行数据库
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteSql(string strSql)
        {

            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 插入InsertExpetion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertExpetion(string Uri, int Level)
        {
            string strSql = string.Format(@"INSERT INTO UCML_NationRegionExpetion([Uri],[Level],[IsUpdate],[CreateTime],[ModifyTime]) 
                                            VALUES('{0}','{1}',0,GETDATE(),GETDATE())", Uri, Level);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改InsertExpetion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateExpetion(string ID, string Uri, int IsUpdate)
        {
            string strSql = string.Format(@"UPDATE [dbo].[UCML_NationRegionExpetion]
                                           SET [Uri] = '{0}'
                                              ,[IsUpdate] = '{1}'
                                              ,[ModifyTime] = GETDATE()
                                        WHERE ID='{2}'", Uri, IsUpdate, ID);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 插入InsertExpetion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool IsExistsExpetionByUri(string Uri, int Level)
        {
            string strSql = string.Format(@"SELECT * FROM dbo.UCML_NationRegionExpetion WHERE Uri='{0}' AND LEVEL = '{1}' AND IsUpdate=0 ;", Uri, Level);
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            bool results = false;
            if (dt.Rows.Count > 0)
            {
                results = true;
            }
            return results;
        }

        /// <summary>
        /// 获取全部未处理的异常数据以及要马上处理
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static DataTable GetAllExpetionData()
        {
            string strSql = string.Format(@"
											SELECT * INTO #TB_NationRegionExpetion FROM dbo.UCML_NationRegionExpetion WHERE IsUpdate=0 
											UPDATE  UCML_NationRegionExpetion
											SET IsUpdate = 1,ModifyTime=GETDATE()
											WHERE ID IN (SELECT ID FROM #TB_NationRegionExpetion );
											SELECT * FROM #TB_NationRegionExpetion ORDER BY LEVEL;
											DROP TABLE #TB_NationRegionExpetion;");
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];


            return dt;

        }

    }
}
