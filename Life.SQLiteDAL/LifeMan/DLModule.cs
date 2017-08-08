using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Life.Factory.LifeMan;
using Life.DALCommon.Sqlite;
using System.Data;
using Life.Model.LifeMan;
using Life.Common;
using System.Data.SQLite;

namespace Life.SQLiteDAL.LifeMan
{
    public class DLModule : FModule
    {
        public int Add(Module index)
        {
            string sql = string.Format("insert into Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values(@Module_ID,@Module_Name,@Module_URL,@Icon_Url,@Parent_Id,@Order_Id,@Notes,@STATUS)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Module_ID",index.ModuleId),
                new SQLiteParameter("@Module_Name",index.ModuleName),
                new SQLiteParameter("@Module_URL",index.ModuleUrl),
                new SQLiteParameter("@Icon_Url",index.IconUrl),
                new SQLiteParameter("@Parent_Id",index.ParentId),
                new SQLiteParameter("@Order_Id",index.OrderId),
                new SQLiteParameter("@Notes",index.Notes),
                new SQLiteParameter("@STATUS",index.Status)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增模块表
        /// </summary>
        /// <param name="list">模块表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Module> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values(@Module_ID,@Module_Name,@Module_URL,@Icon_Url,@Parent_Id,@Order_Id,@Notes,@STATUS)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Module_ID",index.ModuleId),
                    new SQLiteParameter("@Module_Name",index.ModuleName),
                    new SQLiteParameter("@Module_URL",index.ModuleUrl),
                    new SQLiteParameter("@Icon_Url",index.IconUrl),
                    new SQLiteParameter("@Parent_Id",index.ParentId),
                    new SQLiteParameter("@Order_Id",index.OrderId),
                    new SQLiteParameter("@Notes",index.Notes),
                    new SQLiteParameter("@STATUS",index.Status)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }

        /// <summary>
        /// 修改模块表
        /// </summary>
        /// <param name="index">模块表对象</param>
        /// <returns></returns>
        public int Update(Module index)
        {
            string sql = "update Module set Module_ID=:Module_ID,Module_Name=:Module_Name,Module_URL=:Module_URL,Icon_Url=:Icon_Url,Parent_Id=:Parent_Id,Order_Id=:Order_Id,Notes=:Notes,STATUS=:STATUS where Module_ID=:Module_ID";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter(":Module_ID",index.ModuleId),
                new SQLiteParameter(":Module_Name",index.ModuleName),
                new SQLiteParameter(":Module_URL",index.ModuleUrl),
                new SQLiteParameter(":Icon_Url",index.IconUrl),
                new SQLiteParameter(":Parent_Id",index.ParentId),
                new SQLiteParameter(":Order_Id",index.OrderId),
                new SQLiteParameter(":Notes",index.Notes),
                new SQLiteParameter(":STATUS",index.Status)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 删除模块表
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
                String sql = "delete from Module where Module_ID in (" + id + ")";
                return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete()
        {
            String sql = "delete from Module";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        public List<Module> Select(int pageSize, int start, Common.HashTableExp hash, out int total, String sqlWhere)
        {
            List<Module> list = new List<Module>();
            sqlWhere = "1=1" + sqlWhere;

            #region 查询条件
            if (hash["ModuleID"] != null)
            {
                sqlWhere += string.Format(" and Module_ID='{0}'", hash["ModuleID"]);
            }
            if (hash["ModuleName"] != null)
            {
                sqlWhere += string.Format(" and Module_Name='{0}'", hash["ModuleName"]);
            }
            if (hash["ModuleURL"] != null)
            {
                sqlWhere += string.Format(" and Module_URL='{0}'", hash["ModuleURL"]);
            }
            if (hash["IconUrl"] != null)
            {
                sqlWhere += string.Format(" and Icon_Url='{0}'", hash["IconUrl"]);
            }
            if (hash["ParentId"] != null)
            {
                sqlWhere += string.Format(" and Parent_Id='{0}'", hash["ParentId"]);
            }
            if (hash["OrderId"] != null)
            {
                sqlWhere += string.Format(" and Order_Id='{0}'", hash["OrderId"]);
            }
            if (hash["Notes"] != null)
            {
                sqlWhere += string.Format(" and Notes='{0}'", hash["Notes"]);
            }
            if (hash["Status"] != null)
            {
                sqlWhere += string.Format(" and STATUS='{0}'", hash["Status"]);
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Module",
                "Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS",
                pageSize, start, sqlWhere, "Order_Id", "asc", out total);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Module index = new Module();
                index.ModuleId = dt.Rows[i]["Module_ID"].ToString();
                index.ModuleName = dt.Rows[i]["Module_Name"].ToString();
                index.ModuleUrl = dt.Rows[i]["Module_URL"].ToString();
                index.IconUrl = dt.Rows[i]["Icon_Url"].ToString();
                index.ParentId = dt.Rows[i]["Parent_Id"].ToString();
                index.OrderId = dt.Rows[i]["Order_Id"].GetInt32();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                index.Status = dt.Rows[i]["STATUS"].GetDecimal();
                list.Add(index);
            }

            return list;
        }

        public Module Select(string ModuleID)
        {
            String sql = "select * from Module where Module_ID=@ModuleID";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@ModuleID",ModuleID),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Module index = new Module();
                index.ModuleId = dt.Rows[0]["Module_ID"].ToString();
                index.ModuleName = dt.Rows[0]["Module_Name"].ToString();
                index.ModuleUrl = dt.Rows[0]["Module_URL"].ToString();
                index.IconUrl = dt.Rows[0]["Icon_Url"].ToString();
                index.ParentId = dt.Rows[0]["Parent_Id"].ToString();
                index.OrderId = dt.Rows[0]["Order_Id"].GetDecimal();
                index.Notes = dt.Rows[0]["Notes"].ToString();
                index.Status = dt.Rows[0]["STATUS"].GetDecimal();
                return index;
            }
            else
                return null;
        }

        public List<Module> Select(Common.HashTableExp hash, String sqlWhere)
        {
            List<Module> list = new List<Module>();
            string sql = "select Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS from Module where 1=1";
            #region 查询条件
            if (hash["ModuleID"] != null)
            {
                sql += string.Format(" and Module_ID='{0}'", hash["ModuleID"]);
            }
            if (hash["ModuleName"] != null)
            {
                sql += string.Format(" and Module_Name='{0}'", hash["ModuleName"]);
            }
            if (hash["ModuleURL"] != null)
            {
                sql += string.Format(" and Module_URL='{0}'", hash["ModuleURL"]);
            }
            if (hash["IconUrl"] != null)
            {
                sql += string.Format(" and Icon_Url='{0}'", hash["IconUrl"]);
            }
            if (hash["ParentId"] != null)
            {
                sql += string.Format(" and Parent_Id='{0}'", hash["ParentId"]);
            }
            if (hash["OrderId"] != null)
            {
                sql += string.Format(" and Order_Id='{0}'", hash["OrderId"]);
            }
            if (hash["Notes"] != null)
            {
                sql += string.Format(" and Notes='{0}'", hash["Notes"]);
            }
            if (hash["Status"] != null)
            {
                sql += string.Format(" and STATUS='{0}'", hash["Status"]);
            }
            #endregion

            sql += sqlWhere + " order by Order_Id asc";

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Module index = new Module();
                index.ModuleId = dt.Rows[i]["Module_ID"].ToString();
                index.ModuleName = dt.Rows[i]["Module_Name"].ToString();
                index.ModuleUrl = dt.Rows[i]["Module_URL"].ToString();
                index.IconUrl = dt.Rows[i]["Icon_Url"].ToString();
                index.ParentId = dt.Rows[i]["Parent_Id"].ToString();
                index.OrderId = dt.Rows[i]["Order_Id"].GetDecimal();
                index.Notes = dt.Rows[i]["Notes"].ToString();
                index.Status = dt.Rows[i]["STATUS"].GetDecimal();
                list.Add(index);
            }
            return list;
        }

        public List<Module> SelectByUserId(string userId, string status)
        {
            String sql = String.Format(@"select distinct d.Module_ID,d.Module_Name,d.Module_URL,d.Icon_Url,d.Parent_Id,d.Order_Id,d.Notes,d.STATUS from User_To_Role a
                inner join Roles b on  a.Role_Id=b.Role_Id
                inner join Role_To_Module c on b.Role_Id=c.Role_Id
                inner join Module d on c.Module_Id=d.Module_ID
                where a.User_Id='{0}'", userId);
            if (!String.IsNullOrEmpty(status))
                sql += " and d.STATUS=" + status;
            sql += " order by d.Order_Id";
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            List<Module> list = Life.Model.Common<Module>.ConvertToList(dt);

            return list;
        }
    }
}
