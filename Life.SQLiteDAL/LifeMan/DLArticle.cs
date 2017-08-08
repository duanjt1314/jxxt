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
    ///2014-03-04
    ///段江涛
    ///</summary>
    public class DLArticle : FArticle
    {
        #region Add
        /// <summary>
        /// 增加文章表
        /// </summary>
        /// <param name="index">文章表对象</param>
        /// <returns></returns>
        public int Add(Article index)
        {
            string sql = string.Format("insert into Article(Id,Title,Content,Cate_Id,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@Title,@Content,@Cate_Id,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Title",index.Title),
                new SQLiteParameter("@Content",index.Content),
                new SQLiteParameter("@Cate_Id",index.CateId),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增文章表
        /// </summary>
        /// <param name="list">文章表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Article> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Article(Id,Title,Content,Cate_Id,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@Title,@Content,@Cate_Id,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Title",index.Title),
                    new SQLiteParameter("@Content",index.Content),
                    new SQLiteParameter("@Cate_Id",index.CateId),
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
        /// 修改文章表
        /// </summary>
        /// <param name="index">文章表对象</param>
        /// <returns></returns>
        public int Update(Article index)
        {
            string sql = "update Article set Id=@Id,Title=@Title,Content=@Content,Cate_Id=@Cate_Id,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Title",index.Title),
                new SQLiteParameter("@Content",index.Content),
                new SQLiteParameter("@Cate_Id",index.CateId),
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
        public int Update(List<Article> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Article set Id=@Id,Title=@Title,Content=@Content,Cate_Id=@Cate_Id,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Title",index.Title),
                    new SQLiteParameter("@Content",index.Content),
                    new SQLiteParameter("@Cate_Id",index.CateId),
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
        /// 删除文章表
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
                String sql = "delete from Article where Id in (" + id + ")";
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
            String sql = "delete from Article";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Article where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Title"] != null)
                {
                    sql += string.Format(" and Title='{0}'", hash["Title"]);
                }
                if (hash["Content"] != null)
                {
                    sql += string.Format(" and Content='{0}'", hash["Content"]);
                }
                if (hash["CateId"] != null)
                {
                    sql += string.Format(" and Cate_Id='{0}'", hash["CateId"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sql += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpDateBy"] != null)
                {
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpDateBy"]);
                }
                if (hash["UpDateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpDateTime"]);
                }
            }
            #endregion

            sql += sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询文章表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public Article Select(string Id)
        {
            String sql = "select * from Article where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Article index = new Article();
                index.Id = dt.Rows[0]["Id"].GetString();
                index.Title = dt.Rows[0]["Title"].GetString();
                index.Content = dt.Rows[0]["Content"].GetString();
                index.CateId = dt.Rows[0]["Cate_Id"].GetString();
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
        /// 分页查询文章表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Article> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Article> list = new List<Article>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Title"] != null)
                {
                    sqlWhere += string.Format(" and Title='{0}'", hash["Title"]);
                }
                if (hash["Content"] != null)
                {
                    sqlWhere += string.Format(" and Content='{0}'", hash["Content"]);
                }
                if (hash["CateId"] != null)
                {
                    sqlWhere += string.Format(" and Cate_Id='{0}'", hash["CateId"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sqlWhere += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sqlWhere += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpDateBy"] != null)
                {
                    sqlWhere += string.Format(" and UpDate_By='{0}'", hash["UpDateBy"]);
                }
                if (hash["UpDateTime"] != null)
                {
                    sqlWhere += string.Format(" and UpDate_Time='{0}'", hash["UpDateTime"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Article",
                "Id,Title,Content,Cate_Id,Create_By,Create_Time,UpDate_By,UpDate_Time",
                pageSize, start, sqlWhere, "Create_Time", "desc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Article index = new Article();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.Title = dt.Rows[i]["Title"].GetString();
                index.Content = dt.Rows[i]["Content"].GetString();
                index.CateId = dt.Rows[i]["Cate_Id"].GetString();
                index.CreateBy = dt.Rows[i]["Create_By"].GetString();
                index.CreateTime = dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[i]["UpDate_By"].GetString();
                index.UpdateTime = dt.Rows[i]["UpDate_Time"].GetDateTime();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Article> Select(HashTableExp hash, String sqlWhere)
        {
            List<Article> list = new List<Article>();
            string sql = "select Id,Title,Content,Cate_Id,Create_By,Create_Time,UpDate_By,UpDate_Time from Article where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Title"] != null)
                {
                    sql += string.Format(" and Title='{0}'", hash["Title"]);
                }
                if (hash["Content"] != null)
                {
                    sql += string.Format(" and Content='{0}'", hash["Content"]);
                }
                if (hash["CateId"] != null)
                {
                    sql += string.Format(" and Cate_Id='{0}'", hash["CateId"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sql += string.Format(" and Create_By='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpDateBy"] != null)
                {
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpDateBy"]);
                }
                if (hash["UpDateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpDateTime"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Article index = new Article();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.Title = dt.Rows[i]["Title"].GetString();
                index.Content = dt.Rows[i]["Content"].GetString();
                index.CateId = dt.Rows[i]["Cate_Id"].GetString();
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


