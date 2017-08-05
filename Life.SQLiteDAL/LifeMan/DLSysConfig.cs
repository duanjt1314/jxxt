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
    public class DLSysConfig:FSysConfig
    {
        #region Add
        /// <summary>
        /// 增加系统配置
        /// </summary>
        /// <param name="index">系统配置对象</param>
        /// <returns></returns>
        public int Add(SysConfig index)
        {
            string sql = string.Format("insert into Sys_Config(Id,Sys_Key,Name,Sys_Value,Remark,Group_No,Is_Visible,Order_Id,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@Sys_Key,@Name,@Sys_Value,@Remark,@Group_No,@Is_Visible,@Order_Id,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Sys_Key",index.SysKey),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Sys_Value",index.SysValue),
                new SQLiteParameter("@Remark",index.Remark),
                new SQLiteParameter("@Group_No",index.GroupNo),
                new SQLiteParameter("@Is_Visible",index.IsVisible),
                new SQLiteParameter("@Order_Id",index.OrderId),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }
        
        /// <summary>
        /// 批量新增系统配置
        /// </summary>
        /// <param name="list">系统配置对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<SysConfig> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            
            foreach (var index in list)
            {
                sql = string.Format("insert into Sys_Config(Id,Sys_Key,Name,Sys_Value,Remark,Group_No,Is_Visible,Order_Id,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@Sys_Key,@Name,@Sys_Value,@Remark,@Group_No,@Is_Visible,@Order_Id,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Sys_Key",index.SysKey),
                    new SQLiteParameter("@Name",index.Name),
                    new SQLiteParameter("@Sys_Value",index.SysValue),
                    new SQLiteParameter("@Remark",index.Remark),
                    new SQLiteParameter("@Group_No",index.GroupNo),
                    new SQLiteParameter("@Is_Visible",index.IsVisible),
                    new SQLiteParameter("@Order_Id",index.OrderId),
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
        /// 修改系统配置
        /// </summary>
        /// <param name="index">系统配置对象</param>
        /// <returns></returns>
        public int Update(SysConfig index)
        {
            string sql = "update Sys_Config set Id=@Id,Sys_Key=@Sys_Key,Name=@Name,Sys_Value=@Sys_Value,Remark=@Remark,Group_No=@Group_No,Is_Visible=@Is_Visible,Order_Id=@Order_Id,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@Sys_Key",index.SysKey),
                new SQLiteParameter("@Name",index.Name),
                new SQLiteParameter("@Sys_Value",index.SysValue),
                new SQLiteParameter("@Remark",index.Remark),
                new SQLiteParameter("@Group_No",index.GroupNo),
                new SQLiteParameter("@Is_Visible",index.IsVisible),
                new SQLiteParameter("@Order_Id",index.OrderId),
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
        public int Update(List<SysConfig> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();
            
            foreach (var index in list)
            {
                sql ="update Sys_Config set Id=@Id,Sys_Key=@Sys_Key,Name=@Name,Sys_Value=@Sys_Value,Remark=@Remark,Group_No=@Group_No,Is_Visible=@Is_Visible,Order_Id=@Order_Id,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@Sys_Key",index.SysKey),
                    new SQLiteParameter("@Name",index.Name),
                    new SQLiteParameter("@Sys_Value",index.SysValue),
                    new SQLiteParameter("@Remark",index.Remark),
                    new SQLiteParameter("@Group_No",index.GroupNo),
                    new SQLiteParameter("@Is_Visible",index.IsVisible),
                    new SQLiteParameter("@Order_Id",index.OrderId),
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
        /// 删除系统配置
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
                String sql = "delete from Sys_Config where Id in ("+id+")";
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
            String sql = "delete from Sys_Config";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }
        
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash,String sqlWhere)
        {
            string sql = "delete from Sys_Config where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["SysKey"] != null)
                {
                    sql += string.Format(" and Sys_Key='{0}'", hash["SysKey"]);
                }
                if (hash["Name"] != null)
                {
                    sql += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["SysValue"] != null)
                {
                    sql += string.Format(" and Sys_Value='{0}'", hash["SysValue"]);
                }
                if (hash["Remark"] != null)
                {
                    sql += string.Format(" and Remark='{0}'", hash["Remark"]);
                }
                if (hash["GroupNo"] != null)
                {
                    sql += string.Format(" and Group_No='{0}'", hash["GroupNo"]);
                }
                if (hash["IsVisible"] != null)
                {
                    sql += string.Format(" and Is_Visible='{0}'", hash["IsVisible"]);
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
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);            
        }
        #endregion
        
        #region Select
        /// <summary>
        /// 根据编号查询系统配置
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public SysConfig Select(string Id)
        {
            String sql = "select * from SysConfig where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                SysConfig index = new SysConfig();
                index.Id=dt.Rows[0]["Id"].GetString();
                index.SysKey=dt.Rows[0]["Sys_Key"].GetString();
                index.Name=dt.Rows[0]["Name"].GetString();
                index.SysValue=dt.Rows[0]["Sys_Value"].GetString();
                index.Remark=dt.Rows[0]["Remark"].GetString();
                index.GroupNo=dt.Rows[0]["Group_No"].GetString();
                index.IsVisible=dt.Rows[0]["Is_Visible"].GetBoolean();
                index.OrderId=dt.Rows[0]["Order_Id"].GetInt32();
                index.CreateBy=dt.Rows[0]["Create_By"].GetString();
                index.CreateTime=dt.Rows[0]["Create_Time"].GetDateTime();
                index.UpdateBy=dt.Rows[0]["UpDate_By"].GetString();
                index.UpdateTime=dt.Rows[0]["UpDate_Time"].GetDateTime();
                return index;
            }
            else
                return null;
        }
        
        /// <summary>
        /// 分页查询系统配置
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<SysConfig> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere)
        {
            List<SysConfig> list=new List<SysConfig>();
            sqlWhere="1=1 "+sqlWhere;
            
            #region 查询条件
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["SysKey"] != null)
                {
                    sqlWhere += string.Format(" and Sys_Key='{0}'", hash["SysKey"]);
                }
                if (hash["Name"] != null)
                {
                    sqlWhere += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["SysValue"] != null)
                {
                    sqlWhere += string.Format(" and Sys_Value='{0}'", hash["SysValue"]);
                }
                if (hash["Remark"] != null)
                {
                    sqlWhere += string.Format(" and Remark='{0}'", hash["Remark"]);
                }
                if (hash["GroupNo"] != null)
                {
                    sqlWhere += string.Format(" and Group_No='{0}'", hash["GroupNo"]);
                }
                if (hash["IsVisible"] != null)
                {
                    sqlWhere += string.Format(" and Is_Visible='{0}'", hash["IsVisible"]);
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
                    sqlWhere += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sqlWhere += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion
            
            DataTable dt = SqlLiteHelper.GetTable("Sys_Config",
                "Id,Sys_Key,Name,Sys_Value,Remark,Group_No,Is_Visible,Order_Id,Create_By,Create_Time,UpDate_By,UpDate_Time",
                pageSize, start, sqlWhere, "Id", "asc", out total);
                
            for(int i=0;i<dt.Rows.Count;i++)
            {
                SysConfig index=new SysConfig();
                index.Id=dt.Rows[i]["Id"].GetString();
                index.SysKey=dt.Rows[i]["Sys_Key"].GetString();
                index.Name=dt.Rows[i]["Name"].GetString();
                index.SysValue=dt.Rows[i]["Sys_Value"].GetString();
                index.Remark=dt.Rows[i]["Remark"].GetString();
                index.GroupNo=dt.Rows[i]["Group_No"].GetString();
                index.IsVisible=dt.Rows[i]["Is_Visible"].GetBoolean();
                index.OrderId=dt.Rows[i]["Order_Id"].GetInt32();
                index.CreateBy=dt.Rows[i]["Create_By"].GetString();
                index.CreateTime=dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy=dt.Rows[i]["UpDate_By"].GetString();
                index.UpdateTime=dt.Rows[i]["UpDate_Time"].GetDateTime();
                list.Add(index);
            }
            
            return list;
        }
                
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<SysConfig> Select(HashTableExp hash,String sqlWhere)
        {
            List<SysConfig> list=new List<SysConfig>();
            string sql = "select Id,Sys_Key,Name,Sys_Value,Remark,Group_No,Is_Visible,Order_Id,Create_By,Create_Time,UpDate_By,UpDate_Time from Sys_Config where 1=1 ";
            
            #region 查询条件            
            if(hash!=null){
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["SysKey"] != null)
                {
                    sql += string.Format(" and Sys_Key='{0}'", hash["SysKey"]);
                }
                if (hash["Name"] != null)
                {
                    sql += string.Format(" and Name='{0}'", hash["Name"]);
                }
                if (hash["SysValue"] != null)
                {
                    sql += string.Format(" and Sys_Value='{0}'", hash["SysValue"]);
                }
                if (hash["Remark"] != null)
                {
                    sql += string.Format(" and Remark='{0}'", hash["Remark"]);
                }
                if (hash["GroupNo"] != null)
                {
                    sql += string.Format(" and Group_No='{0}'", hash["GroupNo"]);
                }
                if (hash["IsVisible"] != null)
                {
                    sql += string.Format(" and Is_Visible='{0}'", hash["IsVisible"]);
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
                    sql += string.Format(" and UpDate_By='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and UpDate_Time='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion
            
            sql+=sqlWhere;
            
            DataTable dt=SqlLiteHelper.GetTable(sql,CommandType.Text);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                SysConfig index=new SysConfig();
                index.Id=dt.Rows[i]["Id"].GetString();
                index.SysKey=dt.Rows[i]["Sys_Key"].GetString();
                index.Name=dt.Rows[i]["Name"].GetString();
                index.SysValue=dt.Rows[i]["Sys_Value"].GetString();
                index.Remark=dt.Rows[i]["Remark"].GetString();
                index.GroupNo=dt.Rows[i]["Group_No"].GetString();
                index.IsVisible=dt.Rows[i]["Is_Visible"].GetBoolean();
                index.OrderId=dt.Rows[i]["Order_Id"].GetInt32();
                index.CreateBy=dt.Rows[i]["Create_By"].GetString();
                index.CreateTime=dt.Rows[i]["Create_Time"].GetDateTime();
                index.UpdateBy=dt.Rows[i]["UpDate_By"].GetString();
                index.UpdateTime=dt.Rows[i]["UpDate_Time"].GetDateTime();
                list.Add(index);
            }
            return list;
        }

        #endregion
                
    }
}


