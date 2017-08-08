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
    public class DLIncome : FIncome
    {
        #region Add
        /// <summary>
        /// 增加收入记录表
        /// </summary>
        /// <param name="index">收入记录表对象</param>
        /// <returns></returns>
        public int Add(Income index)
        {
            string sql = string.Format("insert into Income(Id,TIME,Price,Note,FamilyIncome,IsMark,CusGroup,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@TIME,@Price,@Note,@FamilyIncome,@IsMark,@CusGroup,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                new SQLiteParameter("@IsMark",index.IsMark),
                new SQLiteParameter("@CusGroup",index.CusGroup),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增收入记录表
        /// </summary>
        /// <param name="list">收入记录表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Income> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Income(Id,TIME,Price,Note,FamilyIncome,IsMark,CusGroup,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@TIME,@Price,@Note,@FamilyIncome,@IsMark,@CusGroup,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Note",index.Note),
                    new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                    new SQLiteParameter("@IsMark",index.IsMark),
                    new SQLiteParameter("@CusGroup",index.CusGroup),
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
        /// 修改收入记录表
        /// </summary>
        /// <param name="index">收入记录表对象</param>
        /// <returns></returns>
        public int Update(Income index)
        {
            string sql = "update Income set Id=@Id,TIME=@TIME,Price=@Price,Note=@Note,FamilyIncome=@FamilyIncome,IsMark=@IsMark,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,CusGroup=@CusGroup where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                new SQLiteParameter("@IsMark",index.IsMark),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                new SQLiteParameter("@CusGroup",index.CusGroup)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<Income> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Income set Id=@Id,TIME=@TIME,Price=@Price,Note=@Note,FamilyIncome=@FamilyIncome,IsMark=@IsMark,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,CusGroup=@CusGroup where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Note",index.Note),
                    new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                    new SQLiteParameter("@IsMark",index.IsMark),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                    new SQLiteParameter("@CusGroup",index.CusGroup)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除收入记录表
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
                String sql = "delete from Income where Id in (" + id + ")";
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
            String sql = "delete from Income";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Income where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Time"] != null)
                {
                    sql += string.Format(" and TIME='{0}'", hash["Time"]);
                }
                if (hash["Price"] != null)
                {
                    sql += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["Note"] != null)
                {
                    sql += string.Format(" and Note='{0}'", hash["Note"]);
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
        /// 根据编号查询收入记录表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public Income Select(string Id)
        {
            String sql = "select * from Income where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            if (dt.Rows.Count > 0)
            {
                Income index = new Income();
                index.Id = dt.Rows[0]["Id"].GetString();
                index.Time = dt.Rows[0]["TIME"].GetDateTime();
                index.Price = dt.Rows[0]["Price"].GetDouble();
                index.Note = dt.Rows[0]["Note"].GetString();
                index.CreateBy = dt.Rows[0]["Create_By"].GetString();
                index.CreateTime = dt.Rows[0]["Create_Time"].GetDateTime();
                index.UpdateBy = dt.Rows[0]["UpDate_By"].GetString();
                index.UpdateTime = dt.Rows[0]["UpDate_Time"].GetDateTime();
                index.FamilyIncome =dt.Rows[0]["FamilyIncome"].GetBoolean();
                index.IsMark = dt.Rows[0]["IsMark"].GetBoolean();      
                return index;
            }
            else
                return null;
        }

        /// <summary>
        /// 分页查询收入记录表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VIncome> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<VIncome> list = new List<VIncome>();
            sqlWhere = "1=1 " + sqlWhere;

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Time"] != null)
                {
                    sqlWhere += string.Format(" and TIME='{0}'", hash["Time"]);
                }
                if (hash["Price"] != null)
                {
                    sqlWhere += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["Note"] != null)
                {
                    sqlWhere += string.Format(" and Note='{0}'", hash["Note"]);
                }
                if (hash["FamilyIncome"] != null)
                {
                    sqlWhere += string.Format(" and FamilyIncome='{0}'", hash["FamilyIncome"]);
                }
                if (hash["IsMark"] != null)
                {
                    sqlWhere += string.Format(" and IsMark='{0}'", hash["IsMark"]);
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
                if (hash["CusGroup"] != null)
                {
                    sqlWhere += string.Format(" and CusGroup='{0}'", hash["CusGroup"]);
                }
                if (hash["CreateName"] != null)
                {
                    sqlWhere += string.Format(" and Create_Name='{0}'", hash["CreateName"]);
                }
                if (hash["UpdateName"] != null)
                {
                    sqlWhere += string.Format(" and Update_Name='{0}'", hash["UpdateName"]);
                }
                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and substr(time,1,7)='{0}'", hash["YearMonth"]);
                }
                //多个自定义分组用逗号分隔
                if (hash["CusGroupMore"] != null)
                {
                    sqlWhere += string.Format(" and CusGroup in({0})", hash["CusGroupMore"].GetIds());
                }
                //不包含的标识
                if (hash["CusGroupNo"] != null)
                {
                    sqlWhere += string.Format(" and (CusGroup not in({0}) OR CusGroup IS NULL)", hash["CusGroupNo"].GetIds());
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("V_Income",
                "Id,TIME,Price,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,Create_Name,Update_Name,FamilyIncome,IsMark,CusGroup",
                pageSize, start, sqlWhere, "Time desc,Create_Time", "desc", out total);

            list = Model.Common<VIncome>.ConvertToList(dt);

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Income> Select(HashTableExp hash, String sqlWhere)
        {
            List<Income> list = new List<Income>();
            string sql = "select Id,TIME,Price,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,FamilyIncome,IsMark,CusGroup from Income where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Time"] != null)
                {
                    sql += string.Format(" and TIME='{0}'", hash["Time"]);
                }
                if (hash["Price"] != null)
                {
                    sql += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["Note"] != null)
                {
                    sql += string.Format(" and Note='{0}'", hash["Note"]);
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

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            list = Model.Common<Income>.ConvertToList(dt);
            return list;
        }

        #endregion

        /// <summary>
        /// 根据条件查询收入总金额
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public double GetTotalPrice(HashTableExp hash, string sqlWhere)
        {
            List<VIncome> list = new List<VIncome>();
            string sql = "SELECT isnull(SUM(Price),0) FROM V_Income where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and Id='{0}'", hash["Id"]);
                }
                if (hash["Time"] != null)
                {
                    sqlWhere += string.Format(" and TIME='{0}'", hash["Time"]);
                }
                if (hash["Price"] != null)
                {
                    sqlWhere += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["Note"] != null)
                {
                    sqlWhere += string.Format(" and Note='{0}'", hash["Note"]);
                }
                if (hash["FamilyIncome"] != null)
                {
                    sqlWhere += string.Format(" and FamilyIncome='{0}'", hash["FamilyIncome"]);
                }
                if (hash["IsMark"] != null)
                {
                    sqlWhere += string.Format(" and IsMark='{0}'", hash["IsMark"]);
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
                if (hash["CusGroup"] != null)
                {
                    sqlWhere += string.Format(" and CusGroup='{0}'", hash["CusGroup"]);
                }
                if (hash["CreateName"] != null)
                {
                    sqlWhere += string.Format(" and Create_Name='{0}'", hash["CreateName"]);
                }
                if (hash["UpdateName"] != null)
                {
                    sqlWhere += string.Format(" and Update_Name='{0}'", hash["UpdateName"]);
                }
                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and substr(time,1,7)='{0}'", hash["YearMonth"]);
                }
                //多个自定义分组用逗号分隔
                if (hash["CusGroupMore"] != null)
                {
                    sqlWhere += string.Format(" and CusGroup in({0})", hash["CusGroupMore"].GetIds());
                }
                //不包含的标识
                if (hash["CusGroupNo"] != null)
                {
                    sqlWhere += string.Format(" and (CusGroup not in({0}) OR CusGroup IS NULL)", hash["CusGroupNo"].GetIds());
                }
            }
            #endregion

            sql += sqlWhere;

            Object result = SqlLiteHelper.ExecuteScalar(sql, CommandType.Text);

            return result.GetDouble();
        }

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ModifyCusGroup(String ids, String value)
        {
            try
            {
                List<SqlHashTable> sqls=new List<SqlHashTable>();
                String[] temp = ids.Split(',');
                foreach (var item in temp)
                {
                    String sql = String.Format("update Income set CusGroup='{0}' where Id='{1}'", value, item);
                    sqls.Add(new SqlHashTable(sql,null));
                }
                return SqlLiteHelper.ExecuteSql(sqls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>        
        public List<string> GetAllCusGroup()
        {
            String sql = "select CusGroup from Income where CusGroup is NOT NULL GROUP BY CusGroup";
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            List<String> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i][0].GetString());
            }
            return list;
        }

        /// <summary>
        /// 批量保存纯收入信息，存在就修改，否则就新增
        /// </summary>
        /// <param name="incomes"></param>
        /// <returns></returns>
        public bool Save(List<Income> incomes)
        {
            try
            {
                List<SqlHashTable> list = new List<SqlHashTable>();

                foreach (var index in incomes)
                {
                    var b = Select(index.Id);
                    if (b == null)
                    {
                        #region 新增
                        string sql = string.Format("insert into Income(Id,TIME,Price,Note,FamilyIncome,IsMark,CusGroup,Create_By,Create_Time,UpDate_By,UpDate_Time) values(@Id,@TIME,@Price,@Note,@FamilyIncome,@IsMark,@CusGroup,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time)");
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Note",index.Note),
                            new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                            new SQLiteParameter("@IsMark",index.IsMark),
                            new SQLiteParameter("@CusGroup",index.CusGroup),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime)
                        };
                        list.Add(new SqlHashTable(sql, parm));
                        #endregion
                    }
                    else
                    {
                        #region 修改
                        string sql = "update Income set Id=@Id,TIME=@TIME,Price=@Price,Note=@Note,FamilyIncome=@FamilyIncome,IsMark=@IsMark,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,CusGroup=@CusGroup where Id=@Id";
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Note",index.Note),
                            new SQLiteParameter("@FamilyIncome",index.FamilyIncome),
                            new SQLiteParameter("@IsMark",index.IsMark),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                            new SQLiteParameter("@CusGroup",index.CusGroup)
                        };
                        list.Add(new SqlHashTable(sql, parm));
                        #endregion
                    }
                }
                SqlLiteHelper.ExecuteSql(list);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("批量保存纯收入信息", ex);
                return false;
            }
            throw new NotImplementedException();
        }
    }
}


