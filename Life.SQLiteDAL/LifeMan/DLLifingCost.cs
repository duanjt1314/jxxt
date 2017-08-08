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
    ///2014-03-06
    ///段江涛
    ///</summary>
    public class DLLifingCost : FLifingCost
    {
        #region Add
        /// <summary>
        /// 增加生活费操作管理
        /// </summary>
        /// <param name="index">生活费操作管理对象</param>
        /// <returns></returns>
        public int Add(LifingCost index)
        {
            string sql = string.Format(@"insert into Lifing_Cost(Id,TIME,Reason,Price,Cost_Type_Id,Notes,Img_Url,Create_By,Create_Time,UpDate_By,UpDate_Time,IsMark,FamilyPay,CusGroup) 
                values(@Id,@TIME,@Reason,@Price,@Cost_Type_Id,@Notes,@Img_Url,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@IsMark,@FamilyPay,@CusGroup)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Reason",index.Reason),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                new SQLiteParameter("@Notes",index.Notes),
                new SQLiteParameter("@Img_Url",index.ImgUrl),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                new SQLiteParameter("@IsMark",index.IsMark),
                new SQLiteParameter("@FamilyPay",index.FamilyPay),
                new SQLiteParameter("@CusGroup",index.CusGroup)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增生活费操作管理
        /// </summary>
        /// <param name="list">生活费操作管理对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<LifingCost> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format(@"insert into Lifing_Cost(Id,TIME,Reason,Price,Cost_Type_Id,Notes,Img_Url,Create_By,Create_Time,UpDate_By,UpDate_Time,IsMark,FamilyPay,CusGroup) 
                    values(@Id,@TIME,@Reason,@Price,@Cost_Type_Id,@Notes,@Img_Url,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@IsMark,@FamilyPay,@CusGroup)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Reason",index.Reason),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                    new SQLiteParameter("@Notes",index.Notes),
                    new SQLiteParameter("@Img_Url",index.ImgUrl),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                    new SQLiteParameter("@IsMark",index.IsMark),
                    new SQLiteParameter("@FamilyPay",index.FamilyPay),
                    new SQLiteParameter("@CusGroup",index.CusGroup)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改生活费操作管理
        /// </summary>
        /// <param name="index">生活费操作管理对象</param>
        /// <returns></returns>
        public int Update(LifingCost index)
        {
            string sql = "update Lifing_Cost set Id=@Id,TIME=@TIME,Reason=@Reason,Price=@Price,Cost_Type_Id=@Cost_Type_Id,Notes=@Notes,Img_Url=@Img_Url,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,IsMark=@IsMark,FamilyPay=@FamilyPay,CusGroup=@CusGroup where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Reason",index.Reason),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                new SQLiteParameter("@Notes",index.Notes),
                new SQLiteParameter("@Img_Url",index.ImgUrl),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                new SQLiteParameter("@IsMark",index.IsMark),
                new SQLiteParameter("@FamilyPay",index.FamilyPay),
                new SQLiteParameter("@CusGroup",index.CusGroup)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<LifingCost> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Lifing_Cost set Id=@Id,TIME=@TIME,Reason=@Reason,Price=@Price,Cost_Type_Id=@Cost_Type_Id,Notes=@Notes,Img_Url=@Img_Url,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,IsMark=@IsMark,FamilyPay=@FamilyPay,CusGroup=@CusGroup where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Reason",index.Reason),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                    new SQLiteParameter("@Notes",index.Notes),
                    new SQLiteParameter("@Img_Url",index.ImgUrl),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                    new SQLiteParameter("@IsMark",index.IsMark),
                    new SQLiteParameter("@FamilyPay",index.FamilyPay),
                    new SQLiteParameter("@CusGroup",index.CusGroup)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除生活费操作管理
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
                String sql = "delete from Lifing_Cost where Id in (" + id + ")";
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
            String sql = "delete from Lifing_Cost";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Lifing_Cost where 1=1 ";

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
                if (hash["Reason"] != null)
                {
                    sql += string.Format(" and Reason='{0}'", hash["Reason"]);
                }
                if (hash["Price"] != null)
                {
                    sql += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["CostTypeId"] != null)
                {
                    sql += string.Format(" and Cost_Type_Id='{0}'", hash["CostTypeId"]);
                }
                if (hash["Notes"] != null)
                {
                    sql += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
                if (hash["ImgUrl"] != null)
                {
                    sql += string.Format(" and Img_Url='{0}'", hash["ImgUrl"]);
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
        /// 根据编号查询生活费操作管理
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public LifingCost Select(string Id)
        {
            String sql = "select * from Lifing_Cost where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);

            List<LifingCost> list = Model.Common<LifingCost>.ConvertToList(dt);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        /// <summary>
        /// 分页查询生活费操作管理
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VLifingCost> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<VLifingCost> list = new List<VLifingCost>();
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
                if (hash["Reason"] != null)
                {
                    sqlWhere += string.Format(" and Reason='{0}'", hash["Reason"]);
                }
                if (hash["Price"] != null)
                {
                    sqlWhere += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["CostTypeId"] != null)
                {
                    sqlWhere += string.Format(" and Cost_Type_Id='{0}'", hash["CostTypeId"]);
                }
                if (hash["Notes"] != null)
                {
                    sqlWhere += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
                if (hash["ImgUrl"] != null)
                {
                    sqlWhere += string.Format(" and Img_Url='{0}'", hash["ImgUrl"]);
                }
                if (hash["IsMark"] != null)
                {
                    sqlWhere += string.Format(" and IsMark='{0}'", hash["IsMark"]);
                }
                if (hash["FamilyPay"] != null)
                {
                    sqlWhere += string.Format(" and FamilyPay='{0}'", hash["FamilyPay"]);
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
                if (hash["CostTypeName"] != null)
                {
                    sqlWhere += string.Format(" and COST_TYPE_NAME='{0}'", hash["CostTypeName"]);
                }
                if (hash["CreateName"] != null)
                {
                    sqlWhere += string.Format(" and CREATE_NAME='{0}'", hash["CreateName"]);
                }
                if (hash["UpdateName"] != null)
                {
                    sqlWhere += string.Format(" and UPDATE_NAME='{0}'", hash["UpdateName"]);
                }

                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and strftime('%Y-%m',Time) in({0})", hash["YearMonth"]);
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


            DataTable dt = SqlLiteHelper.GetTable("V_LIFING_COST",
                "Id,TIME,Reason,Price,Cost_Type_Id,Notes,Img_Url,Create_By,Create_Time,UpDate_By,UpDate_Time,COST_TYPE_NAME,CREATE_NAME,UPDATE_NAME,IsMark,FamilyPay,CusGroup",
                pageSize, start, sqlWhere, "time desc,Create_Time", "desc", out total);

            list = Model.Common<VLifingCost>.ConvertToList(dt);

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<LifingCost> Select(HashTableExp hash, String sqlWhere)
        {
            List<LifingCost> list = new List<LifingCost>();
            string sql = "select Id,TIME,Reason,Price,Cost_Type_Id,Notes,Img_Url,Create_By,Create_Time,UpDate_By,UpDate_Time,IsMark,FamilyPay,CusGroup from Lifing_Cost where 1=1 ";

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
                if (hash["Reason"] != null)
                {
                    sql += string.Format(" and Reason='{0}'", hash["Reason"]);
                }
                if (hash["Price"] != null)
                {
                    sql += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["CostTypeId"] != null)
                {
                    sql += string.Format(" and Cost_Type_Id='{0}'", hash["CostTypeId"]);
                }
                if (hash["Notes"] != null)
                {
                    sql += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
                if (hash["ImgUrl"] != null)
                {
                    sql += string.Format(" and Img_Url='{0}'", hash["ImgUrl"]);
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
                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and substr(time,1,7)='{0}'", hash["YearMonth"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);

            list = Model.Common<LifingCost>.ConvertToList(dt);

            return list;
        }

        #endregion

        #region Collection
        /// <summary>
        /// 汇总月份生活费消费信息
        /// Sqlite 的ifnull函数和SqlServer的isnull函数功能一样
        /// </summary>
        /// <returns></returns>
        public DataTable GetCollectionByMonth(String sqlWhere, String beginTime, String endTime, String isCalc)
        {
            if (!String.IsNullOrEmpty(beginTime))
            {
                sqlWhere += String.Format(" and substr(time,1,7)>='{0}'", beginTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                sqlWhere += String.Format(" and substr(time,1,7)<='{0}'", endTime);
            }
            String sql = String.Format(@"select substr(time,1,7) time,SUM(Price) price,
                ifnull((select SUM(price) from Income b where substr(a.time,1,7)=substr(b.time,1,7) {0}),0) interPrice 
                from Lifing_Cost a where 1=1 {0} {1} 
                group by substr(time,1,7) order by substr(time,1,7) asc", sqlWhere, isCalc);

            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 根据月份汇总生活费
        /// </summary>
        /// <param name="time">时间，如:2013-01</param>
        /// <returns></returns>
        public DataTable GetCollectionByDay(string time, String sqlWhere)
        {
            String sql = @"select TIME time,SUM(Price) price from Lifing_Cost ";
            if (!String.IsNullOrEmpty(time))
                sql += " where substr(time,1,7)='" + time + "' " + sqlWhere;
            sql += " group by time order by time";
            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 根据月份汇总生活费类型
        /// </summary>
        /// <param name="time">时间，如:2013-01</param>
        /// <returns></returns>
        public DataTable GetCollectionByType(string time, String sqlWhere)
        {
            String sql = @"select COST_TYPE_NAME costTypeName,SUM(price) price from V_LIFING_COST";
            if (!String.IsNullOrEmpty(time))
                sql += " where substr(time,1,7)='" + time + "' " + sqlWhere;
            sql += " group by COST_TYPE_NAME";
            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }
        #endregion

        /// <summary>
        /// 查询生活费的所有消费名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<LifingCost> GetReasons(string key)
        {
            List<LifingCost> list = new List<LifingCost>();
            string sql = "select Reason from Lifing_Cost group by Reason having Reason like '%@key%'";

            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@key",key)
            };

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LifingCost index = new LifingCost();
                index.Reason = dt.Rows[i]["Reason"].GetString();
                list.Add(index);
            }
            return list;
        }

        public List<CostMonthCol> GetCollectionByMonth(string beginTime, string endTime, bool? isMark, bool? family, string costTypeIds, String userId)
        {
            String costSqlWhere = String.Empty;
            String incomeSqlWhere = String.Empty;
            #region 组装条件
            if (!String.IsNullOrEmpty(beginTime))
            {
                costSqlWhere += String.Format(" and time>='{0}'", beginTime);
                incomeSqlWhere += String.Format(" and time>='{0}'", beginTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                costSqlWhere += String.Format(" and time<datetime('{0}','+1 hour')", endTime);
                incomeSqlWhere += String.Format(" and time<datetime('{0}','+1 hour')", endTime);
            }
            if (isMark != null)
            {
                costSqlWhere += String.Format(" and isMark='{0}'", isMark.GetInt32());
                incomeSqlWhere += String.Format(" and isMark='{0}'", isMark.GetInt32());
            }
            if (family != null)
            {
                costSqlWhere += String.Format(" and FamilyPay='{0}'", family.GetInt32());
                incomeSqlWhere += String.Format(" and FamilyIncome='{0}'", family.GetInt32());
            }
            if (!String.IsNullOrEmpty(costTypeIds))
            {
                costSqlWhere += String.Format(" and Cost_Type_Id in({0})", costTypeIds);
            }
            //不是管理员
            if (userId != "-1")
            {
                costSqlWhere += String.Format(" and Create_By='{0}'", userId);
                incomeSqlWhere += String.Format(" and Create_By='{0}'", userId);
            }
            #endregion

            String sql = String.Format(@"select a.PayTime Time,round(a.Pay,2) Pay,ifnull(round(b.Income,2),0) Income,round((ifnull(b.Income,0)-ifnull(a.Pay,0)),2) Balance from (
            select strftime('%Y-%m',Time) PayTime,SUM(Price) Pay from Lifing_Cost where 1=1 {0}
            group by strftime('%Y-%m',Time)
            ) a left join 
            (
            select strftime('%Y-%m',Time) InTime,SUM(Price) Income from Income where 1=1 {1}
            group by strftime('%Y-%m',Time)
            ) b on a.PayTime=b.InTime
            order by a.PayTime desc", costSqlWhere, incomeSqlWhere);

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            return Life.Model.Common<CostMonthCol>.ConvertToList(dt);
        }

        /// <summary>
        /// 根据月份汇总生活费
        /// </summary>
        /// <param name="time">时间,如:'2014-01','2014-12'</param>
        /// <param name="beginTime">开始统计的时间</param>
        /// <param name="endTime">结束统计的时间</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内收支</param>
        /// <param name="costTypeId">消费类型编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public DataTable GetCollectionByDay(string time, string beginTime, string endTime, bool? isMark, bool? family, string costTypeIds, string userId)
        {
            String costSqlWhere = String.Empty;
            #region 组装条件
            if (!String.IsNullOrEmpty(time))
            {
                costSqlWhere += String.Format(" and strftime('%Y-%m',Time) in ({0})", time);
            }
            if (!String.IsNullOrEmpty(beginTime))
            {
                costSqlWhere += String.Format(" and time>='{0}'", beginTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                costSqlWhere += String.Format(" and time<datetime('{0}','+1 hour')", endTime);
            }
            if (isMark != null)
            {
                costSqlWhere += String.Format(" and isMark='{0}'", isMark.GetInt32());
            }
            if (family != null)
            {
                costSqlWhere += String.Format(" and FamilyPay='{0}'", family.GetInt32());
            }
            if (!String.IsNullOrEmpty(costTypeIds))
            {
                costSqlWhere += String.Format(" and Cost_Type_Id in({0})", costTypeIds);
            }
            //不是管理员
            if (userId != "-1")
            {
                costSqlWhere += String.Format(" and Create_By='{0}'", userId);
            }
            #endregion
            String sql = String.Format(@"select TIME time,SUM(Price) price 
            from Lifing_Cost where 1=1 {0} 
            group by time order by time", costSqlWhere);
            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 汇总生活费类型
        /// </summary>
        /// <param name="time">时间,如:'2014-01','2014-12'</param>
        /// <param name="beginTime">开始统计的时间</param>
        /// <param name="endTime">结束统计的时间</param>
        /// <param name="isMark">特殊标识</param>
        /// <param name="family">家庭内收支</param>
        /// <param name="costTypeId">消费类型编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public DataTable GetCollectionType(string time, string beginTime, string endTime, bool? isMark, bool? family, string costTypeIds, string userId)
        {
            String costSqlWhere = String.Empty;
            #region 组装条件
            if (!String.IsNullOrEmpty(time))
            {
                costSqlWhere += String.Format(" and strftime('%Y-%m',Time) in ({0})", time);
            }
            if (!String.IsNullOrEmpty(beginTime))
            {
                costSqlWhere += String.Format(" and time>='{0}'", beginTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                costSqlWhere += String.Format(" and time<datetime('{0}','+1 hour')", endTime);
            }
            if (isMark != null)
            {
                costSqlWhere += String.Format(" and isMark='{0}'", isMark.GetInt32());
            }
            if (family != null)
            {
                costSqlWhere += String.Format(" and FamilyPay='{0}'", family.GetInt32());
            }
            if (!String.IsNullOrEmpty(costTypeIds))
            {
                costSqlWhere += String.Format(" and Cost_Type_Id in({0})", costTypeIds);
            }
            //不是管理员
            if (userId != "-1")
            {
                costSqlWhere += String.Format(" and Create_By='{0}'", userId);
            }
            #endregion

            String sql = String.Format(@"select COST_TYPE_NAME costTypeName,SUM(price) price 
            from V_LIFING_COST where 1=1 {0} 
            group by COST_TYPE_NAME", costSqlWhere);

            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 汇总月份生活费消费信息--图形汇总
        /// </summary>
        /// <returns></returns>
        public DataTable GetCollection(String sqlWhere, String beginTime, String endTime)
        {
            if (!String.IsNullOrEmpty(beginTime))
            {
                sqlWhere += String.Format(" and strftime('%Y-%m',Time)>='{0}'", beginTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                sqlWhere += String.Format(" and strftime('%Y-%m',Time)<='{0}'", endTime);
            }
            String sql = String.Format(@"select strftime('%Y-%m',Time) time,SUM(price) price,
                ifnull((select SUM(price) from Income b 
                    where strftime('%Y-%m',a.Time)=strftime('%Y-%m',b.Time) {0}
                ),0) interPrice from Lifing_Cost a where 1=1 {0}
                group by strftime('%Y-%m',Time) order by time", sqlWhere);

            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 根据条件查询消费总金额
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public double GetTotalPrice(HashTableExp hash, string sqlWhere)
        {
            List<VLifingCost> list = new List<VLifingCost>();
            string sql = "SELECT ifnull(round(SUM(Price),2),0) FROM V_LIFING_COST where 1=1 ";
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
                if (hash["Reason"] != null)
                {
                    sqlWhere += string.Format(" and Reason='{0}'", hash["Reason"]);
                }
                if (hash["Price"] != null)
                {
                    sqlWhere += string.Format(" and Price='{0}'", hash["Price"]);
                }
                if (hash["CostTypeId"] != null)
                {
                    sqlWhere += string.Format(" and Cost_Type_Id='{0}'", hash["CostTypeId"]);
                }
                if (hash["Notes"] != null)
                {
                    sqlWhere += string.Format(" and Notes='{0}'", hash["Notes"]);
                }
                if (hash["ImgUrl"] != null)
                {
                    sqlWhere += string.Format(" and Img_Url='{0}'", hash["ImgUrl"]);
                }
                if (hash["IsMark"] != null)
                {
                    sqlWhere += string.Format(" and IsMark='{0}'", hash["IsMark"]);
                }
                if (hash["FamilyPay"] != null)
                {
                    sqlWhere += string.Format(" and FamilyPay='{0}'", hash["FamilyPay"]);
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
                if (hash["CostTypeName"] != null)
                {
                    sqlWhere += string.Format(" and COST_TYPE_NAME='{0}'", hash["CostTypeName"]);
                }
                if (hash["CreateName"] != null)
                {
                    sqlWhere += string.Format(" and CREATE_NAME='{0}'", hash["CreateName"]);
                }
                if (hash["UpdateName"] != null)
                {
                    sqlWhere += string.Format(" and UPDATE_NAME='{0}'", hash["UpdateName"]);
                }

                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and strftime('%Y-%m',Time) in({0})", hash["YearMonth"]);
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
        public int ModifyCusGroup(string ids, string value)
        {
            try
            {
                List<SqlHashTable> sqls = new List<SqlHashTable>();
                String[] temp = ids.Split(',');
                foreach (var item in temp)
                {
                    String sql = String.Format("update Lifing_Cost set CusGroup='{0}' where Id='{1}'", value, item);
                    sqls.Add(new SqlHashTable(sql, null));
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
            String sql = "select CusGroup from Lifing_Cost where CusGroup is NOT NULL GROUP BY CusGroup";
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            List<String> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i][0].GetString());
            }
            return list;
        }

        /// <summary>
        /// 查询所有的消费名称汇总,如:李子、桃子等
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReasons()
        {
            List<String> list = new List<String>();
            string sql = "select Reason from Lifing_Cost group by Reason";

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]["Reason"].GetString());
            }
            return list;
        }

        /// <summary>
        /// 批量保存生活费信息,存在就修改,否则就新增
        /// </summary>
        /// <param name="lifes"></param>
        /// <returns></returns>
        public bool Save(List<LifingCost> lifes)
        {
            try
            {
                List<SqlHashTable> list = new List<SqlHashTable>();

                foreach (var index in lifes)
                {
                    var b = Select(index.Id);
                    if (b == null)
                    {
                        #region 新增
                        string sql = string.Format(@"insert into Lifing_Cost(Id,TIME,Reason,Price,Cost_Type_Id,Notes,Img_Url,Create_By,Create_Time,UpDate_By,UpDate_Time,IsMark,FamilyPay,CusGroup) 
                values(@Id,@TIME,@Reason,@Price,@Cost_Type_Id,@Notes,@Img_Url,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@IsMark,@FamilyPay,@CusGroup)");
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Reason",index.Reason),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                            new SQLiteParameter("@Notes",index.Notes),
                            new SQLiteParameter("@Img_Url",index.ImgUrl),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                            new SQLiteParameter("@IsMark",index.IsMark),
                            new SQLiteParameter("@FamilyPay",index.FamilyPay),
                            new SQLiteParameter("@CusGroup",index.CusGroup)
                        };
                        list.Add(new SqlHashTable(sql, parm));
                        #endregion
                    }
                    else
                    {
                        #region 修改
                        string sql = "update Lifing_Cost set Id=@Id,TIME=@TIME,Reason=@Reason,Price=@Price,Cost_Type_Id=@Cost_Type_Id,Notes=@Notes,Img_Url=@Img_Url,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,IsMark=@IsMark,FamilyPay=@FamilyPay,CusGroup=@CusGroup where Id=@Id";
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Reason",index.Reason),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Cost_Type_Id",index.CostTypeId),
                            new SQLiteParameter("@Notes",index.Notes),
                            new SQLiteParameter("@Img_Url",index.ImgUrl),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                            new SQLiteParameter("@IsMark",index.IsMark),
                            new SQLiteParameter("@FamilyPay",index.FamilyPay),
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
                LogHelper.Log.Error("批量保存生活费信息", ex);
                return false;
            }
        }
    }
}


