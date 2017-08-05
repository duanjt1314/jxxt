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
    ///2014-03-09
    ///段江涛
    ///</summary>
    public class DLTempData:FTempData
    {
        #region Add
        /// <summary>
        /// 增加临时信息存储表
        /// </summary>
        /// <param name="index">临时信息存储表对象</param>
        /// <returns></returns>
        public int Add(TempData index)
        {
            string sql = string.Format("insert into Temp_Data(Id,Email,Expires,Create_Time) values(@Id,@Email,@Expires,@Create_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Email",index.Email),
                new SQLiteParameter("@Expires",index.Expires),
                new SQLiteParameter("@Create_Time",index.CreateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }
        
        /// <summary>
        /// 批量新增临时信息存储表
        /// </summary>
        /// <param name="list">临时信息存储表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<TempData> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            
            foreach (var index in list)
            {
                sql = string.Format("insert into Temp_Data(Id,Email,Expires,Create_Time) values(@Id,@Email,@Expires,@Create_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Email",index.Email),
                    new SQLiteParameter("@Expires",index.Expires),
                    new SQLiteParameter("@Create_Time",index.CreateTime)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion
        
        #region Update
        
        /// <summary>
        /// 修改临时信息存储表
        /// </summary>
        /// <param name="index">临时信息存储表对象</param>
        /// <returns></returns>
        public int Update(TempData index)
        {
            string sql = "update Temp_Data set Id=@Id,Email=@Email,Expires=@Expires,Create_Time=@Create_Time where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Email",index.Email),
                new SQLiteParameter("@Expires",index.Expires),
                new SQLiteParameter("@Create_Time",index.CreateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }
        
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<TempData> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            
            foreach (var index in list)
            {
                sql ="update Temp_Data set Id=@Id,Email=@Email,Expires=@Expires,Create_Time=@Create_Time where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Email",index.Email),
                    new SQLiteParameter("@Expires",index.Expires),
                    new SQLiteParameter("@Create_Time",index.CreateTime)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion
        
        #region Delete
        
        /// <summary>
        /// 删除临时信息存储表
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
                    id +=String.Format("'{0}'",item);
                }
                String sql = "delete from Temp_Data where Id in ("+id+")";
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
            String sql = "delete from Temp_Data";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }
        
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash,String sqlWhere)
        {
            string sql = "delete from Temp_Data where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Email"] != null)
                {
                    sql += string.Format(" and Email='{0}'", hash["Email"]);
                }
                if (hash["Expires"] != null)
                {
                    sql += string.Format(" and Expires='{0}'", hash["Expires"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);            
        }
        #endregion
        
        #region Select
        /// <summary>
        /// 根据编号查询临时信息存储表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public TempData Select(string Id)
        {
            String sql = "select * from TempData where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                TempData index = new TempData();
                index.Id=dt.Rows[0]["Id"].GetString();
                index.Email=dt.Rows[0]["Email"].GetString();
                index.Expires=dt.Rows[0]["Expires"].GetDateTime();
                index.CreateTime=dt.Rows[0]["Create_Time"].GetDateTime();
                return index;
            }
            else
                return null;
        }
        
        /// <summary>
        /// 分页查询临时信息存储表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<TempData> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere)
        {
            List<TempData> list=new List<TempData>();
            sqlWhere="1=1 "+sqlWhere;
            
            #region 查询条件
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Email"] != null)
                {
                    sqlWhere += string.Format(" and Email='{0}'", hash["Email"]);
                }
                if (hash["Expires"] != null)
                {
                    sqlWhere += string.Format(" and Expires='{0}'", hash["Expires"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sqlWhere += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
            }
            #endregion
            
            DataTable dt = SqlLiteHelper.GetTable("Temp_Data",
                "Id,Email,Expires,Create_Time",
                pageSize, start, sqlWhere, "Id", "asc", out total);
                
            for(int i=0;i<dt.Rows.Count;i++)
            {
                TempData index=new TempData();
                index.Id=dt.Rows[i]["Id"].GetString();
                index.Email=dt.Rows[i]["Email"].GetString();
                index.Expires=dt.Rows[i]["Expires"].GetDateTime();
                index.CreateTime=dt.Rows[i]["Create_Time"].GetDateTime();
                list.Add(index);
            }
            
            return list;
        }
                
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<TempData> Select(HashTableExp hash,String sqlWhere)
        {
            List<TempData> list=new List<TempData>();
            string sql = "select Id,Email,Expires,Create_Time from Temp_Data where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Email"] != null)
                {
                    sql += string.Format(" and Email='{0}'", hash["Email"]);
                }
                if (hash["Expires"] != null)
                {
                    sql += string.Format(" and Expires='{0}'", hash["Expires"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and Create_Time='{0}'", hash["CreateTime"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            
            DataTable dt=SqlLiteHelper.GetTable(sql,CommandType.Text);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                TempData index=new TempData();
                index.Id=dt.Rows[i]["Id"].GetString();
                index.Email=dt.Rows[i]["Email"].GetString();
                index.Expires=dt.Rows[i]["Expires"].GetDateTime();
                index.CreateTime=dt.Rows[i]["Create_Time"].GetDateTime();
                list.Add(index);
            }
            return list;
        }

        #endregion
                
    }
}


