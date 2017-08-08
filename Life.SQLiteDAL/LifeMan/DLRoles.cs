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
    public class DLRoles:FRoles
    {
        #region Add
        /// <summary>
        /// 增加角色表
        /// </summary>
        /// <param name="index">角色表对象</param>
        /// <returns></returns>
        public int Add(Roles index)
        {
            string sql = string.Format("insert into Roles(Role_Id,Role_Name,Notes) values(@Role_Id,@Role_Name,@Notes)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Role_Id",index.RoleId),
                new SQLiteParameter("@Role_Name",index.RoleName),
                new SQLiteParameter("@Notes",index.Notes)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增角色表
        /// </summary>
        /// <param name="list">角色表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Roles> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Roles(Role_Id,Role_Name,Notes) values(@Role_Id,@Role_Name,@Notes)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Role_Id",index.RoleId),
                    new SQLiteParameter("@Role_Name",index.RoleName),
                    new SQLiteParameter("@Notes",index.Notes)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改角色表
        /// </summary>
        /// <param name="index">角色表对象</param>
        /// <returns></returns>
        public int Update(Roles index)
        {
            string sql = "update Roles set Role_Id=@Role_Id,Role_Name=@Role_Name,Notes=@Notes where Role_Id=@Role_Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Role_Id",index.RoleId),
                new SQLiteParameter("@Role_Name",index.RoleName),
                new SQLiteParameter("@Notes",index.Notes)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除角色表
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
                    id += String.Format("'{0}'",item);
                }
                String sql = "delete from Roles where Role_Id in (" + id + ")";
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
            String sql = "delete from Roles";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询角色表
        /// </summary>
        /// <param name="RoleId">编号</param>
        /// <returns>数据集合</returns>
        public Roles Select(string RoleId)
        {
            String sql = "select * from Roles where Role_Id=@RoleId";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@RoleId",RoleId),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Roles index = new Roles();
                index.RoleId = dt.Rows[0]["Role_Id"].ToString();
                index.RoleName = dt.Rows[0]["Role_Name"].ToString();
                index.Notes = dt.Rows[0]["Notes"].ToString();
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询角色表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Roles> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Roles> list = new List<Roles>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash["RoleId"] != null)
            {
                sqlWhere += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
            }
            if (hash["RoleName"] != null)
            {
                sqlWhere += string.Format(" and Role_Name='{0}'", hash["RoleName"]);
            }
            if (hash["Notes"] != null)
            {
                sqlWhere += string.Format(" and Notes='{0}'", hash["Notes"]);
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Roles",
                "Role_Id,Role_Name,Notes",
                pageSize, start, sqlWhere, "Role_Id", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Roles index = new Roles();
                index.RoleId = dt.Rows[i]["Role_Id"].ToString();
                index.RoleName = dt.Rows[i]["Role_Name"].ToString();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Roles> Select(HashTableExp hash, String sqlWhere)
        {
            List<Roles> list = new List<Roles>();
            string sql = "select Role_Id,Role_Name,Notes from Roles where 1=1 ";
            #region 查询条件
            if (hash["RoleId"] != null)
            {
                sql += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
            }
            if (hash["RoleName"] != null)
            {
                sql += string.Format(" and Role_Name='{0}'", hash["RoleName"]);
            }
            if (hash["Notes"] != null)
            {
                sql += string.Format(" and Notes='{0}'", hash["Notes"]);
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Roles index = new Roles();
                index.RoleId = dt.Rows[i]["Role_Id"].ToString();
                index.RoleName = dt.Rows[i]["Role_Name"].ToString();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                list.Add(index);
            }
            return list;
        }

        #endregion

    }
}


