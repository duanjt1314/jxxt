using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using Life.Common;
using Life.DALCommon.Sqlite;
using Life.Factory.LifeMan;
using Life.Model.LifeMan;

namespace Life.SQLiteDAL.LifeMan
{
    ///<summary>
    ///2014-03-02
    ///段江涛
    ///</summary>
    public class DLArtCategory : FArtCategory
    {
        #region Add
        /// <summary>
        /// 增加文章类型表
        /// </summary>
        /// <param name="index">文章类型表对象</param>
        /// <returns></returns>
        public int Add(ArtCategory index)
        {
            string sql = string.Format("insert into Art_Category(Cat_Id,Cat_Name,Cat_Remark,Cat_Order,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Cat_Id,@Cat_Name,@Cat_Remark,@Cat_Order,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Cat_Id",index.CatId),
                new SQLiteParameter("@Cat_Name",index.CatName),
                new SQLiteParameter("@Cat_Remark",index.CatRemark),
                new SQLiteParameter("@Cat_Order",index.CatOrder),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增文章类型表
        /// </summary>
        /// <param name="list">文章类型表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<ArtCategory> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Art_Category(Cat_Id,Cat_Name,Cat_Remark,Cat_Order,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Cat_Id,@Cat_Name,@Cat_Remark,@Cat_Order,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Cat_Id",index.CatId),
                    new SQLiteParameter("@Cat_Name",index.CatName),
                    new SQLiteParameter("@Cat_Remark",index.CatRemark),
                    new SQLiteParameter("@Cat_Order",index.CatOrder),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改文章类型表
        /// </summary>
        /// <param name="index">文章类型表对象</param>
        /// <returns></returns>
        public int Update(ArtCategory index)
        {
            string sql = "update Art_Category set Cat_Id=@Cat_Id,Cat_Name=@Cat_Name,Cat_Remark=@Cat_Remark,Cat_Order=@Cat_Order,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Cat_Id=@Cat_Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Cat_Id",index.CatId),
                new SQLiteParameter("@Cat_Name",index.CatName),
                new SQLiteParameter("@Cat_Remark",index.CatRemark),
                new SQLiteParameter("@Cat_Order",index.CatOrder),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<ArtCategory> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Art_Category set Cat_Id=@Cat_Id,Cat_Name=@Cat_Name,Cat_Remark=@Cat_Remark,Cat_Order=@Cat_Order,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Cat_Id=@Cat_Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Cat_Id",index.CatId),
                    new SQLiteParameter("@Cat_Name",index.CatName),
                    new SQLiteParameter("@Cat_Remark",index.CatRemark),
                    new SQLiteParameter("@Cat_Order",index.CatOrder),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除文章类型表
        /// </summary>
        /// <param name="id">编号的集合，如：1,2,3...</param>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            try
            {
                String id = "";
                String[] temp = ids.Split(',');
                foreach (var item in temp)
                {
                    if (!String.IsNullOrEmpty(id))
                    {
                        id += ",";
                    }
                    id += String.Format("'{0}'", item);
                }
                String sql = "delete from Art_Category where Cat_Id in (" + id + ")";
                return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            String sql = "delete from Art_Category";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Art_Category where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["CatId"] != null)
                {
                    sql += string.Format(" and Cat_Id='{0}'", hash["CatId"]);
                }
                if (hash["CatName"] != null)
                {
                    sql += string.Format(" and Cat_Name='{0}'", hash["CatName"]);
                }
                if (hash["CatRemark"] != null)
                {
                    sql += string.Format(" and Cat_Remark='{0}'", hash["CatRemark"]);
                }
                if (hash["CatOrder"] != null)
                {
                    sql += string.Format(" and Cat_Order='{0}'", hash["CatOrder"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sql += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            sql += sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询文章类型表
        /// </summary>
        /// <param name="CatId">编号</param>
        /// <returns>数据集合</returns>
        public ArtCategory Select(string CatId)
        {
            String sql = "select * from ArtCategory where Cat_Id=@CatId";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@CatId",CatId),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                ArtCategory index = new ArtCategory();
                index.CatId = dt.Rows[0]["Cat_Id"].GetString();
                index.CatName = dt.Rows[0]["Cat_Name"].GetString();
                index.CatRemark = dt.Rows[0]["Cat_Remark"].GetString();
                index.CatOrder = dt.Rows[0]["Cat_Order"].GetInt32();
                index.CreateBy = dt.Rows[0]["Create_By"].GetString();
                index.CreateTime = dt.Rows[0]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[0]["UpDate_By"].GetString();
                index.UpdateTime = dt.Rows[0]["UpDate_Time"].GetDateTime();
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询文章类型表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VArtCategory> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<VArtCategory> list = new List<VArtCategory>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["CatId"] != null)
                {
                    sqlWhere += string.Format(" and Cat_Id='{0}'", hash["CatId"]);
                }
                if (hash["CatName"] != null)
                {
                    sqlWhere += string.Format(" and Cat_Name='{0}'", hash["CatName"]);
                }
                if (hash["CatRemark"] != null)
                {
                    sqlWhere += string.Format(" and Cat_Remark='{0}'", hash["CatRemark"]);
                }
                if (hash["CatOrder"] != null)
                {
                    sqlWhere += string.Format(" and Cat_Order='{0}'", hash["CatOrder"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sqlWhere += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sqlWhere += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sqlWhere += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sqlWhere += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("V_Art_Category",
                "Cat_Id,Cat_Name,Cat_Remark,Cat_Order,Create_By,Create_Time,UpDate_By,UpDate_Time,Create_Name,Update_Name",
                pageSize, start, sqlWhere, "Cat_Order", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VArtCategory index = new VArtCategory();
                index.CatId = dt.Rows[i]["Cat_Id"].GetString();
                index.CatName = dt.Rows[i]["Cat_Name"].GetString();
                index.CatRemark = dt.Rows[i]["Cat_Remark"].GetString();
                index.CatOrder = dt.Rows[i]["Cat_Order"].GetInt32();
                index.CreateBy = dt.Rows[i]["Create_By"].GetString();
                index.CreateTime = dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[i]["UpDate_By"].GetString();
                index.UpdateTime = dt.Rows[i]["UpDate_Time"].GetDateTime();
                index.CreateName = dt.Rows[i]["Create_Name"].GetString();
                index.UpdateName = dt.Rows[i]["Update_Name"].GetString();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<ArtCategory> Select(HashTableExp hash, String sqlWhere)
        {
            List<ArtCategory> list = new List<ArtCategory>();
            string sql = "select Cat_Id,Cat_Name,Cat_Remark,Cat_Order,Create_By,Create_Time,UpDate_By,UpDate_Time from Art_Category where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["CatId"] != null)
                {
                    sql += string.Format(" and Cat_Id='{0}'", hash["CatId"]);
                }
                if (hash["CatName"] != null)
                {
                    sql += string.Format(" and Cat_Name='{0}'", hash["CatName"]);
                }
                if (hash["CatRemark"] != null)
                {
                    sql += string.Format(" and Cat_Remark='{0}'", hash["CatRemark"]);
                }
                if (hash["CatOrder"] != null)
                {
                    sql += string.Format(" and Cat_Order='{0}'", hash["CatOrder"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sql += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            sql += sqlWhere + " order by Cat_Order asc";

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ArtCategory index = new ArtCategory();
                index.CatId = dt.Rows[i]["Cat_Id"].GetString();
                index.CatName = dt.Rows[i]["Cat_Name"].GetString();
                index.CatRemark = dt.Rows[i]["Cat_Remark"].GetString();
                index.CatOrder = dt.Rows[i]["Cat_Order"].GetInt32();
                index.CreateBy = dt.Rows[i]["Create_By"].GetString();
                index.CreateTime = dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[i]["UpDate_By"].GetString();
                index.UpdateTime = dt.Rows[i]["UpDate_Time"].GetDateTime();
                list.Add(index);
            }
            return list;
        }

        #endregion

    }
}


