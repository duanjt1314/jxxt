using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using Life.Common;
using Life.DALCommon.Repository;
using Life.DALCommon.Sqlite;
using Life.Factory.LifeMan;
using Life.Model.LifeMan;

namespace Life.SQLiteDAL.LifeMan
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class DLUsers : FUsers
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(Users index)
        {
            string sql = string.Format("insert into Users(Id,Login_Id,Login_Pwd,Name,Phone,Mail,Address,Age,Notes) values(@Id,@Login_Id,@Login_Pwd,@Name,@Phone,@Mail,@Address,@Age,@Notes)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Login_Id",index.LoginId),
                new SQLiteParameter("@Login_Pwd",index.LoginPwd),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Phone",index.Phone),
                new SQLiteParameter("@Mail",index.Mail),
                new SQLiteParameter("@Address",index.Address),
                new SQLiteParameter("@Age",index.Age),
                new SQLiteParameter("@Notes",index.Notes)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<Users> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Users(Id,Login_Id,Login_Pwd,Name,Phone,Mail,Address,Age,Notes) values(@Id,@Login_Id,@Login_Pwd,@Name,@Phone,@Mail,@Address,@Age,@Notes)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Login_Id",index.LoginId),
                    new SQLiteParameter("@Login_Pwd",index.LoginPwd),
                    new SQLiteParameter("@Name",index.Name),
                    new SQLiteParameter("@Phone",index.Phone),
                    new SQLiteParameter("@Mail",index.Mail),
                    new SQLiteParameter("@Address",index.Address),
                    new SQLiteParameter("@Age",index.Age),
                    new SQLiteParameter("@Notes",index.Notes)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }

        #region Update

        /// <summary>
        /// 修改用户表
        /// </summary>
        /// <param name="index">用户表对象</param>
        /// <returns></returns>
        public int Update(Users index)
        {
            string sql = "update Users set Id=@Id,Login_Id=@Login_Id,Login_Pwd=@Login_Pwd,Name=@Name,Phone=@Phone,Mail=@Mail,Address=@Address,Age=@Age,Notes=@Notes where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Login_Id",index.LoginId),
                new SQLiteParameter("@Login_Pwd",index.LoginPwd),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Phone",index.Phone),
                new SQLiteParameter("@Mail",index.Mail),
                new SQLiteParameter("@Address",index.Address),
                new SQLiteParameter("@Age",index.Age),
                new SQLiteParameter("@Notes",index.Notes)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        #endregion

        /// <summary>
        /// 删除
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
                String sql = "delete from Users where Id in (" + id + ")";
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
            String sql = "delete from Users";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据集合</returns>
        public List<Users> Select()
        {
            List<Users> list = new List<Users>();
            String sql = "select Id,Login_Id,Login_Pwd,Name,Phone,Mail,Address,Age,Notes from Users";
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Users index = new Users();
                index.Id = dt.Rows[i]["Id"].ToString();
                index.LoginId = dt.Rows[i]["Login_Id"].ToString();
                index.LoginPwd = dt.Rows[i]["Login_Pwd"].ToString();
                index.Name = dt.Rows[i]["Name"].ToString();
                index.Phone = dt.Rows[i]["Phone"].ToString();
                index.Mail = dt.Rows[i]["Mail"].ToString();
                index.Address = dt.Rows[i]["Address"].ToString();
                index.Age = dt.Rows[i]["Age"].GetInt32();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                list.Add(index);
            }
            return list;
        }

        /// <summary>
        /// 分页查询用户表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Users> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Users> list = new List<Users>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["LoginId"] != null)
                {
                    sqlWhere += string.Format(" and Login_Id='{0}'", hash["LoginId"]);
                }
                if (hash["LoginPwd"] != null)
                {
                    sqlWhere += string.Format(" and Login_Pwd='{0}'", hash["LoginPwd"]);
                }
                if (hash["Name"] != null)
                {
                    sqlWhere += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["Phone"] != null)
                {
                    sqlWhere += string.Format(" and Phone='{0}'", hash["Phone"]);
                }
                if (hash["Mail"] != null)
                {
                    sqlWhere += string.Format(" and Mail='{0}'", hash["Mail"]);
                }
                if (hash["Address"] != null)
                {
                    sqlWhere += string.Format(" and Address='{0}'", hash["Address"]);
                }
                if (hash["Age"] != null)
                {
                    sqlWhere += string.Format(" and Age='{0}'", hash["Age"]);
                }
                if (hash["Notes"] != null)
                {
                    sqlWhere += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Users",
                "Id,Login_Id,Login_Pwd,Name,Phone,Mail,Address,Age,Notes",
                pageSize, start, sqlWhere, "Id", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Users index = new Users();
                index.Id = dt.Rows[i]["Id"].ToString();
                index.LoginId = dt.Rows[i]["Login_Id"].ToString();
                index.LoginPwd = dt.Rows[i]["Login_Pwd"].ToString();
                index.Name = dt.Rows[i]["Name"].ToString();
                index.Phone = dt.Rows[i]["Phone"].ToString();
                index.Mail = dt.Rows[i]["Mail"].ToString();
                index.Address = dt.Rows[i]["Address"].ToString();
                index.Age = dt.Rows[i]["Age"].GetInt32();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public Users Select(string id)
        {
            String sql = "select * from Users where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Users index = new Users();
                index.Id = dt.Rows[0]["Id"].ToString();
                index.LoginId = dt.Rows[0]["Login_Id"].ToString();
                index.LoginPwd = dt.Rows[0]["Login_Pwd"].ToString();
                index.Name = dt.Rows[0]["Name"].ToString();
                index.Phone = dt.Rows[0]["Phone"].ToString();
                index.Mail = dt.Rows[0]["Mail"].ToString();
                index.Address = dt.Rows[0]["Address"].ToString();
                index.Age = dt.Rows[0]["Age"].GetInt32();
                index.Notes = dt.Rows[0]["Notes"].ToString();
                return index;
            }
            else
                return null;
        }

        public Users Login(string loginId, string loginPwd)
        {
            String sql = "select * from Users where Login_Id=@loginId and login_Pwd=@loginPwd";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@loginId",loginId),
                new SQLiteParameter("@loginPwd",loginPwd),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Users index = new Users();
                index.Id = dt.Rows[0]["Id"].ToString();
                index.LoginId = dt.Rows[0]["Login_Id"].ToString();
                index.LoginPwd = dt.Rows[0]["Login_Pwd"].ToString();
                index.Name = dt.Rows[0]["Name"].ToString();
                index.Phone = dt.Rows[0]["Phone"].ToString();
                index.Mail = dt.Rows[0]["Mail"].ToString();
                index.Address = dt.Rows[0]["Address"].ToString();
                index.Age = dt.Rows[0]["Age"].GetInt32();
                index.Notes = dt.Rows[0]["Notes"].ToString();
                return index;
            }
            else
                return null;

        }


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Users> Select(Life.Common.HashTableExp hash, String sqlWhere)
        {
            List<Users> list = new List<Users>();
            string sql = "select Id,Login_Id,Login_Pwd,Name,Phone,Mail,Address,Age,Notes from Users where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["LoginId"] != null)
                {
                    sql += string.Format(" and Login_Id='{0}'", hash["LoginId"]);
                }
                if (hash["LoginPwd"] != null)
                {
                    sql += string.Format(" and Login_Pwd='{0}'", hash["LoginPwd"]);
                }
                if (hash["Name"] != null)
                {
                    sql += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["Phone"] != null)
                {
                    sql += string.Format(" and Phone='{0}'", hash["Phone"]);
                }
                if (hash["Mail"] != null)
                {
                    sql += string.Format(" and Mail='{0}'", hash["Mail"]);
                }
                if (hash["Address"] != null)
                {
                    sql += string.Format(" and Address='{0}'", hash["Address"]);
                }
                if (hash["Age"] != null)
                {
                    sql += string.Format(" and Age='{0}'", hash["Age"]);
                }
                if (hash["Notes"] != null)
                {
                    sql += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Users index = new Users();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.LoginId = dt.Rows[i]["Login_Id"].GetString();
                index.LoginPwd = dt.Rows[i]["Login_Pwd"].GetString();
                index.Name = dt.Rows[i]["Name"].GetString();
                index.Phone = dt.Rows[i]["Phone"].GetString();
                index.Mail = dt.Rows[i]["Mail"].GetString();
                index.Address = dt.Rows[i]["Address"].GetString();
                index.Age = dt.Rows[i]["Age"].GetDecimal();
                index.Notes = dt.Rows[i]["Notes"].GetString();
                list.Add(index);
            }
            return list;
        }

    }
}
