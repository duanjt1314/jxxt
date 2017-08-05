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
    ///2014-03-01
    ///段江涛
    ///</summary>
    public class DLUserToRole : FUserToRole
    {
        #region Add
        /// <summary>
        /// 增加用户角色对应表
        /// </summary>
        /// <param name="index">用户角色对应表对象</param>
        /// <returns></returns>
        public int Add(UserToRole index)
        {
            string sql = string.Format("insert into User_To_Role(Id,User_Id,Role_Id) values(@Id,@User_Id,@Role_Id)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@User_Id",index.UserId),
                new SQLiteParameter("@Role_Id",index.RoleId)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增用户角色对应表
        /// </summary>
        /// <param name="list">用户角色对应表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<UserToRole> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into User_To_Role(Id,User_Id,Role_Id) values(@Id,@User_Id,@Role_Id)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@User_Id",index.UserId),
                    new SQLiteParameter("@Role_Id",index.RoleId)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改用户角色对应表
        /// </summary>
        /// <param name="index">用户角色对应表对象</param>
        /// <returns></returns>
        public int Update(UserToRole index)
        {
            string sql = "update User_To_Role set Id=@Id,User_Id=@User_Id,Role_Id=@Role_Id where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@User_Id",index.UserId),
                new SQLiteParameter("@Role_Id",index.RoleId)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除用户角色对应表
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
                String sql = "delete from User_To_Role where Id in (" + id + ")";
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
            String sql = "delete from User_To_Role";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from User_To_Role where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["UserId"] != null)
                {
                    sql += string.Format(" and User_Id='{0}'", hash["UserId"]);
                }
                if (hash["RoleId"] != null)
                {
                    sql += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);            
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询用户角色对应表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public UserToRole Select(string Id)
        {
            String sql = "select * from UserToRole where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                UserToRole index = new UserToRole();
                index.Id = dt.Rows[0]["Id"].GetString();
                index.UserId = dt.Rows[0]["User_Id"].GetString();
                index.RoleId = dt.Rows[0]["Role_Id"].GetString();
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询用户角色对应表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<UserToRole> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<UserToRole> list = new List<UserToRole>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["UserId"] != null)
                {
                    sqlWhere += string.Format(" and User_Id='{0}'", hash["UserId"]);
                }
                if (hash["RoleId"] != null)
                {
                    sqlWhere += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("User_To_Role",
                "Id,User_Id,Role_Id",
                pageSize, start, sqlWhere, "Id", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserToRole index = new UserToRole();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.UserId = dt.Rows[i]["User_Id"].GetString();
                index.RoleId = dt.Rows[i]["Role_Id"].GetString();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<UserToRole> Select(HashTableExp hash, String sqlWhere)
        {
            List<UserToRole> list = new List<UserToRole>();
            string sql = "select Id,User_Id,Role_Id from User_To_Role where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["UserId"] != null)
                {
                    sql += string.Format(" and User_Id='{0}'", hash["UserId"]);
                }
                if (hash["RoleId"] != null)
                {
                    sql += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserToRole index = new UserToRole();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.UserId = dt.Rows[i]["User_Id"].GetString();
                index.RoleId = dt.Rows[i]["Role_Id"].GetString();
                list.Add(index);
            }
            return list;
        }

        #endregion
        
        public int Save(string userId, string[] roleIds)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            //删除用户的所有角色
            hashTables.Add(new SqlHashTable("delete from User_To_Role where User_Id='"+userId+"'",null));
            //循环新增角色
            foreach (var roleId in roleIds)
            {
                sql = string.Format("insert into User_To_Role(Id,User_Id,Role_Id) values(@Id,@User_Id,@Role_Id)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",Guid.NewGuid()),
                    new SQLiteParameter("@User_Id",userId),
                    new SQLiteParameter("@Role_Id",roleId)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);

        }
    }
}


