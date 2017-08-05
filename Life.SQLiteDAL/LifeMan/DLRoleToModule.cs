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
    public class DLRoleToModule : FRoleToModule
    {
        #region Add
        /// <summary>
        /// 增加角色模块对应表
        /// </summary>
        /// <param name="index">角色模块对应表对象</param>
        /// <returns></returns>
        public int Add(RoleToModule index)
        {
            string sql = string.Format("insert into Role_To_Module(Id,Role_Id,Module_Id) values(@Id,@Role_Id,@Module_Id)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Role_Id",index.RoleId),
                new SQLiteParameter("@Module_Id",index.ModuleId)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增角色模块对应表
        /// </summary>
        /// <param name="list">角色模块对应表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<RoleToModule> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Role_To_Module(Id,Role_Id,Module_Id) values(@Id,@Role_Id,@Module_Id)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Role_Id",index.RoleId),
                    new SQLiteParameter("@Module_Id",index.ModuleId)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改角色模块对应表
        /// </summary>
        /// <param name="index">角色模块对应表对象</param>
        /// <returns></returns>
        public int Update(RoleToModule index)
        {
            string sql = "update Role_To_Module set Id=@Id,Role_Id=@Role_Id,Module_Id=@Module_Id where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Role_Id",index.RoleId),
                new SQLiteParameter("@Module_Id",index.ModuleId)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除角色模块对应表
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
                String sql = "delete from Role_To_Module where Id in (" + id + ")";
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
            String sql = "delete from Role_To_Module";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Role_To_Module where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["RoleId"] != null)
                {
                    sql += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
                if (hash["ModuleId"] != null)
                {
                    sql += string.Format(" and Module_Id='{0}'", hash["ModuleId"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);            
        }
        #endregion

        #region Select
        /// <summary>
        /// 根据编号查询角色模块对应表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public RoleToModule Select(string Id)
        {
            String sql = "select * from RoleToModule where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                RoleToModule index = new RoleToModule();
                index.Id = dt.Rows[0]["Id"].GetString();
                index.RoleId = dt.Rows[0]["Role_Id"].GetString();
                index.ModuleId = dt.Rows[0]["Module_Id"].GetString();
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询角色模块对应表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<RoleToModule> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<RoleToModule> list = new List<RoleToModule>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["RoleId"] != null)
                {
                    sqlWhere += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
                if (hash["ModuleId"] != null)
                {
                    sqlWhere += string.Format(" and Module_Id='{0}'", hash["ModuleId"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Role_To_Module",
                "Id,Role_Id,Module_Id",
                pageSize, start, sqlWhere, "Id", "asc", out total);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RoleToModule index = new RoleToModule();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.RoleId = dt.Rows[i]["Role_Id"].GetString();
                index.ModuleId = dt.Rows[i]["Module_Id"].GetString();
                list.Add(index);
            }

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<RoleToModule> Select(HashTableExp hash, String sqlWhere)
        {
            List<RoleToModule> list = new List<RoleToModule>();
            string sql = "select Id,Role_Id,Module_Id from Role_To_Module where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["RoleId"] != null)
                {
                    sql += string.Format(" and Role_Id='{0}'", hash["RoleId"]);
                }
                if (hash["ModuleId"] != null)
                {
                    sql += string.Format(" and Module_Id='{0}'", hash["ModuleId"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RoleToModule index = new RoleToModule();
                index.Id = dt.Rows[i]["Id"].GetString();
                index.RoleId = dt.Rows[i]["Role_Id"].GetString();
                index.ModuleId = dt.Rows[i]["Module_Id"].GetString();
                list.Add(index);
            }
            return list;
        }

        #endregion
        
        public int Save(string roleId, string[] moduleIds)
        {
            String sql = "delete from Role_To_Module where Role_Id='"+roleId+"'";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            //删除角色所对应的模块信息
            hashTables.Add(new SqlHashTable(sql, null));
            //循环增加
            foreach (var moduleId in moduleIds)
            {
                sql = string.Format("insert into Role_To_Module(Id,Role_Id,Module_Id) values(@Id,@Role_Id,@Module_Id)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",Guid.NewGuid()),
                    new SQLiteParameter("@Role_Id",roleId),
                    new SQLiteParameter("@Module_Id",moduleId)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
    }
}


