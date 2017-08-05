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
    ///2014-02-28
    ///段江涛
    ///</summary>
    public class DLDiction : FDiction
    {
        #region Add
        /// <summary>
        /// 增加字典表
        /// </summary>
        /// <param name="index">字典表对象</param>
        /// <returns></returns>
        public int Add(Diction index)
        {
            string sql = string.Format("insert into Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values(@Id,@Name,@Note,@Parent_Id,@Order_Id,@Create_By,@Create_Time,@Update_By,@Update_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@Parent_Id",index.ParentId),
                new SQLiteParameter("@Order_Id",index.OrderId),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@Update_By",index.UpdateBy),
                new SQLiteParameter("@Update_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增字典表
        /// </summary>
        /// <param name="list">字典表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Diction> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values(@Id,@Name,@Note,@Parent_Id,@Order_Id,@Create_By,@Create_Time,@Update_By,@Update_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Name",index.Name),
                    new SQLiteParameter("@Note",index.Note),
                    new SQLiteParameter("@Parent_Id",index.ParentId),
                    new SQLiteParameter("@Order_Id",index.OrderId),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@Update_By",index.UpdateBy),
                    new SQLiteParameter("@Update_Time",index.UpdateTime)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改字典表
        /// </summary>
        /// <param name="index">字典表对象</param>
        /// <returns></returns>
        public int Update(Diction index)
        {
            string sql = "update Diction set Id=@Id,Name=@Name,Note=@Note,Parent_Id=@Parent_Id,Order_Id=@Order_Id,Create_By=@Create_By,Create_Time=@Create_Time,Update_By=@Update_By,Update_Time=@Update_Time where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@Parent_Id",index.ParentId),
                new SQLiteParameter("@Order_Id",index.OrderId),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@Update_By",index.UpdateBy),
                new SQLiteParameter("@Update_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除字典表
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
                String sql = "delete from Diction where Id in (" + id + ")";
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
            String sql = "delete from Diction";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询字典表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public Diction Select(Decimal Id)
        {
            String sql = "select * from Diction where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Diction index = new Diction();
                index.Id = dt.Rows[0]["Id"].GetDecimal();
                index.Name = dt.Rows[0]["Name"].GetString();
                index.Note = dt.Rows[0]["Note"].GetString();
                index.ParentId = dt.Rows[0]["Parent_Id"].GetDecimal();
                index.OrderId = dt.Rows[0]["Order_Id"].GetDecimal();
                index.CreateBy = dt.Rows[0]["Create_By"].GetString();
                index.CreateTime = dt.Rows[0]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[0]["Update_By"].GetString();
                index.UpdateTime = dt.Rows[0]["Update_Time"].GetDateTime();
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询字典表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Diction> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Diction> list = new List<Diction>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Name"] != null)
                {
                    sqlWhere += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["Note"] != null)
                {
                    sqlWhere += string.Format(" and Note='{0}'", hash["Note"]);
                }
                if (hash["ParentId"] != null)
                {
                    sqlWhere += string.Format(" and Parent_Id='{0}'", hash["ParentId"]);
                }
                if (hash["OrderId"] != null)
                {
                    sqlWhere += string.Format(" and Order_Id='{0}'", hash["OrderId"]);
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
                    sqlWhere += string.Format(" and Update_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sqlWhere += string.Format(" and Update_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Diction",
                "Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time",
                pageSize, start, sqlWhere, "Id", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Diction index = new Diction();
                index.Id = dt.Rows[i]["Id"].GetDecimal();
                index.Name = dt.Rows[i]["Name"].GetString();
                index.Note = dt.Rows[i]["Note"].GetString();
                index.ParentId = dt.Rows[i]["Parent_Id"].GetDecimal();
                index.OrderId = dt.Rows[i]["Order_Id"].GetDecimal();
                index.CreateBy = dt.Rows[i]["Create_By"].GetString();
                index.CreateTime = dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[i]["Update_By"].GetString();
                index.UpdateTime = dt.Rows[i]["Update_Time"].GetDateTime();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Diction> Select(HashTableExp hash, String sqlWhere)
        {
            List<Diction> list = new List<Diction>();
            string sql = "select Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time from Diction where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Name"] != null)
                {
                    sql += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["Note"] != null)
                {
                    sql += string.Format(" and Note='{0}'", hash["Note"]);
                }
                if (hash["ParentId"] != null)
                {
                    sql += string.Format(" and Parent_Id='{0}'", hash["ParentId"]);
                }
                if (hash["OrderId"] != null)
                {
                    sql += string.Format(" and Order_Id='{0}'", hash["OrderId"]);
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
                    sql += string.Format(" and Update_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and Update_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text,null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Diction index = new Diction();
                index.Id = dt.Rows[i]["Id"].GetDecimal();
                index.Name = dt.Rows[i]["Name"].GetString();
                index.Note = dt.Rows[i]["Note"].GetString();
                index.ParentId = dt.Rows[i]["Parent_Id"].GetDecimal();
                index.OrderId = dt.Rows[i]["Order_Id"].GetDecimal();
                index.CreateBy = dt.Rows[i]["Create_By"].GetString();
                index.CreateTime = dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[i]["Update_By"].GetString();
                list.Add(index);
            }
            return list;
        }

        #endregion

    }
}


