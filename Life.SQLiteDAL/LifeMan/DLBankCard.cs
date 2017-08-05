using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class DLBankCard : FBankCard
    {
        #region Add
        /// <summary>
        /// 增加银行卡操作记录表
        /// 并自动计算余额
        /// </summary>
        /// <param name="index">银行卡操作记录表对象</param>
        /// <returns></returns>
        public int Add(BankCard index)
        {
            CalcBalance(index, true);
            string sql = string.Format("insert into Bank_Card(Id,TIME,Price,Save_Type,Balance,Bank_Type,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,ImgUrl) values(@Id,@TIME,@Price,@Save_Type,@Balance,@Bank_Type,@Note,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@ImgUrl)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Save_Type",index.SaveType),
                new SQLiteParameter("@Balance",index.Balance),
                new SQLiteParameter("@Bank_Type",index.BankType),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                new SQLiteParameter("@ImgUrl",index.ImgUrl)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增银行卡操作记录表
        /// </summary>
        /// <param name="list">银行卡操作记录表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<BankCard> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Bank_Card(Id,TIME,Price,Save_Type,Balance,Bank_Type,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,ImgUrl) values(@Id,@TIME,@Price,@Save_Type,@Balance,@Bank_Type,@Note,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@ImgUrl)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Save_Type",index.SaveType),
                    new SQLiteParameter("@Balance",index.Balance),
                    new SQLiteParameter("@Bank_Type",index.BankType),
                    new SQLiteParameter("@Note",index.Note),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                    new SQLiteParameter("@ImgUrl",index.ImgUrl)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Update

        /// <summary>
        /// 修改银行卡操作记录表
        /// </summary>
        /// <param name="index">银行卡操作记录表对象</param>
        /// <returns></returns>
        public int Update(BankCard index)
        {
            string sql = "update Bank_Card set Id=@Id,TIME=@TIME,Price=@Price,Save_Type=@Save_Type,Balance=@Balance,Bank_Type=@Bank_Type,Note=@Note,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,ImgUrl=@ImgUrl where Id=@Id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@Id",index.Id),
                new SQLiteParameter("@TIME",index.Time),
                new SQLiteParameter("@Price",index.Price),
                new SQLiteParameter("@Save_Type",index.SaveType),
                new SQLiteParameter("@Balance",index.Balance),
                new SQLiteParameter("@Bank_Type",index.BankType),
                new SQLiteParameter("@Note",index.Note),
                new SQLiteParameter("@Create_By",index.CreateBy),
                new SQLiteParameter("@Create_Time",index.CreateTime),
                new SQLiteParameter("@UpDate_By",index.UpdateBy),
                new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                new SQLiteParameter("@ImgUrl",index.ImgUrl)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<BankCard> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Bank_Card set Id=@Id,TIME=@TIME,Price=@Price,Save_Type=@Save_Type,Balance=@Balance,Bank_Type=@Bank_Type,Note=@Note,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,ImgUrl=@ImgUrl where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",index.Id),
                    new SQLiteParameter("@TIME",index.Time),
                    new SQLiteParameter("@Price",index.Price),
                    new SQLiteParameter("@Save_Type",index.SaveType),
                    new SQLiteParameter("@Balance",index.Balance),
                    new SQLiteParameter("@Bank_Type",index.BankType),
                    new SQLiteParameter("@Note",index.Note),
                    new SQLiteParameter("@Create_By",index.CreateBy),
                    new SQLiteParameter("@Create_Time",index.CreateTime),
                    new SQLiteParameter("@UpDate_By",index.UpdateBy),
                    new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                    new SQLiteParameter("@ImgUrl",index.ImgUrl)
                };
                hashTables.Add(new SqlHashTable(sql, parm));
            }
            return SqlLiteHelper.ExecuteSql(hashTables);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除银行卡操作记录表
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
                    CalcBalance(new DLBankCard().Select(item), false);
                    if (!String.IsNullOrEmpty(id))
                    {
                        id += ",";
                    }
                    id += String.Format("'{0}'", item);
                }
                String sql = "delete from Bank_Card where Id in (" + id + ")";
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
            String sql = "delete from Bank_Card";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Bank_Card where 1=1 ";

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
                if (hash["SaveType"] != null)
                {
                    sql += string.Format(" and Save_Type='{0}'", hash["SaveType"]);
                }
                if (hash["Balance"] != null)
                {
                    sql += string.Format(" and Balance='{0}'", hash["Balance"]);
                }
                if (hash["BankType"] != null)
                {
                    sql += string.Format(" and Bank_Type='{0}'", hash["BankType"]);
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
        /// 根据编号查询银行卡操作记录表
        /// </summary>
        /// <param name="Id">编号</param>
        /// <returns>数据集合</returns>
        public BankCard Select(string Id)
        {
            String sql = "select * from Bank_Card where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);
            List<BankCard> list = Life.Model.Common<BankCard>.ConvertToList(dt);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        /// <summary>
        /// 分页查询银行卡操作记录表
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="hash">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VBankCard> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<VBankCard> list = new List<VBankCard>();
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
                if (hash["SaveType"] != null)
                {
                    sqlWhere += string.Format(" and Save_Type='{0}'", hash["SaveType"]);
                }
                if (hash["Balance"] != null)
                {
                    sqlWhere += string.Format(" and Balance='{0}'", hash["Balance"]);
                }
                if (hash["BankType"] != null)
                {
                    sqlWhere += string.Format(" and Bank_Type='{0}'", hash["BankType"]);
                }
                if (hash["Note"] != null)
                {
                    sqlWhere += string.Format(" and Note='{0}'", hash["Note"]);
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
                if (hash["BankTypeName"] != null)
                {
                    sqlWhere += string.Format(" and BANK_TYPE_NAME='{0}'", hash["BankTypeName"]);
                }
                if (hash["SaveName"] != null)
                {
                    sqlWhere += string.Format(" and SAVE_NAME='{0}'", hash["SaveName"]);
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
                    sqlWhere += string.Format(" and substr(time,1,7)='{0}'", hash["YearMonth"]);
                }
            }
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("V_BANK_CARD",
                "Id,TIME,Price,Save_Type,Balance,Bank_Type,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,BANK_TYPE_NAME,SAVE_NAME,CREATE_NAME,UPDATE_NAME,ImgUrl",
                pageSize, start, sqlWhere, "time", "desc,Create_Time desc", out total);

            list = Life.Model.Common<VBankCard>.ConvertToList(dt);

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<BankCard> Select(HashTableExp hash, String sqlWhere)
        {
            List<BankCard> list = new List<BankCard>();
            string sql = "select Id,TIME,Price,Save_Type,Balance,Bank_Type,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,ImgUrl from Bank_Card where 1=1 ";

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
                if (hash["SaveType"] != null)
                {
                    sql += string.Format(" and Save_Type='{0}'", hash["SaveType"]);
                }
                if (hash["Balance"] != null)
                {
                    sql += string.Format(" and Balance='{0}'", hash["Balance"]);
                }
                if (hash["BankType"] != null)
                {
                    sql += string.Format(" and Bank_Type='{0}'", hash["BankType"]);
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
                //格式2013-01
                if (hash["YearMonth"] != null)
                {
                    sqlWhere += string.Format(" and substr(time,1,7)='{0}'", hash["YearMonth"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            list = Life.Model.Common<BankCard>.ConvertToList(dt);
            return list;
        }

        #endregion

        #region Other

        public DataTable GetCollectionByMonth()
        {
            String sql = @"select substr(time,1,7) time,
(select ifnull(SUM(price),0) from V_BANK_CARD where substr(a.time,1,7)=substr(time,1,7) and Save_Type='1000200001') moneyInsert,
(select ifnull(SUM(price),0) from V_BANK_CARD where substr(a.time,1,7)=substr(time,1,7) and Save_Type='1000200002') moneyOut
from V_BANK_CARD a  
group by substr(time,1,7)";
            return SqlLiteHelper.GetTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 计算所有信息的余额
        /// </summary>
        /// <returns></returns>
        public bool CalcAllBalance()
        {
            Double cal = 0;
            SQLiteParameter[] parm;
            String sql = String.Empty;

            List<Diction> dics = new DLDiction().Select(new HashTableExp("ParentId", "1000100000"), "");
            foreach (var item in dics)
            {
                item.Note = "0";
            }
            List<BankCard> banks = new DLBankCard().Select(null, "").OrderBy(f => f.Time).ThenBy(f => f.CreateTime).ToList();

            foreach (var item in banks)
            {
                //表示存入
                if (item.SaveType == 1000200001)
                {
                    cal = dics.Where(f => f.Id == item.BankType).SingleOrDefault().Note.GetDouble();
                    cal += item.Price;
                    dics.Where(f => f.Id == item.BankType).SingleOrDefault().Note = cal.GetString();
                }
                //表示取出
                else
                {
                    cal = dics.Where(f => f.Id == item.BankType).SingleOrDefault().Note.GetDouble();
                    cal -= item.Price;
                    dics.Where(f => f.Id == item.BankType).SingleOrDefault().Note = cal.GetString();
                }
                item.Balance = cal;
            }

            List<SqlHashTable> hashList = new List<SqlHashTable>();
            foreach (var item in dics)
            {
                sql = "update Diction set Note=@Note where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Note",item.Note),
                    new SQLiteParameter("@Id",item.Id)
                };

                hashList.Add(new SqlHashTable(sql, parm));
            }

            foreach (var item in banks)
            {
                sql = "update Bank_Card set Balance=@Balance where Id=@Id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",item.Id),
                    new SQLiteParameter("@Balance",item.Balance)
                };
                hashList.Add(new SqlHashTable(sql, parm));
            }

            return SqlLiteHelper.ExecuteSql(hashList) > 0;
        }

        /// <summary>
        /// 自动计算对象的余额,isAdd只能是新增或者删除
        /// 如果需要修改就先删除再新增
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BankCard CalcBalance(BankCard index, Boolean isAdd)
        {
            Diction dic = new DLDiction().Select(index.BankType);
            var bal = dic.Note.GetDouble();
            if (isAdd)
            {
                //新增
                if (index.SaveType == 1000200001)//存入
                    bal += index.Price;
                else
                    bal -= index.Price;
            }
            else
            {
                //删除
                if (index.SaveType == 1000200001)//存入
                    bal -= index.Price;
                else
                    bal += index.Price;
            }
            dic.Note = bal.GetString();
            index.Balance = bal;
            new DLDiction().Update(dic);
            return index;
        }
        #endregion

        /// <summary>
        /// 查询所有银行卡的最后一条数据 time格式：2015-01-02
        /// </summary>
        /// <returns></returns>
        public List<VBankCard> SelectCalc(String time)
        {
            String sql = String.Empty;
            List<VBankCard> list = new List<VBankCard>();
            //查询所有银行卡
            List<Diction> dics = new DLDiction().Select(new HashTableExp("ParentId", "1000100000"), null);
            foreach (var item in dics)
            {
                //查询最近一次的记录      
                String sqlWhere = String.Format("Bank_Type='{0}'", item.Id);
                if (!String.IsNullOrEmpty(time))
                    sqlWhere += String.Format(" and time<='{0}'", time);
                sql = String.Format("select * from V_Bank_Card where {0} order by time desc,create_time desc limit 1 offset 0", sqlWhere);

                DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
                list = Life.Model.Common<VBankCard>.ConvertToList(dt);
            }
            return list;
        }

        /// <summary>
        /// 保存银行卡信息,存在就修改,否则就新增
        /// </summary>
        /// <param name="banks"></param>
        /// <returns></returns>
        public bool Save(List<BankCard> banks)
        {
            try
            {
                List<SqlHashTable> list = new List<SqlHashTable>();

                foreach (var index in banks)
                {
                    var b = Select(index.Id);
                    if (b == null)
                    {
                        #region 新增
                        String sql = string.Format("insert into Bank_Card(Id,TIME,Price,Save_Type,Balance,Bank_Type,Note,Create_By,Create_Time,UpDate_By,UpDate_Time,ImgUrl) values(@Id,@TIME,@Price,@Save_Type,@Balance,@Bank_Type,@Note,@Create_By,@Create_Time,@UpDate_By,@UpDate_Time,@ImgUrl)");
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Save_Type",index.SaveType),
                            new SQLiteParameter("@Balance",index.Balance),
                            new SQLiteParameter("@Bank_Type",index.BankType),
                            new SQLiteParameter("@Note",index.Note),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                            new SQLiteParameter("@ImgUrl",index.ImgUrl)
                        };
                        list.Add(new SqlHashTable(sql, parm)); 
                        #endregion
                    }
                    else
                    {
                        #region 修改
                        String sql = "update Bank_Card set Id=@Id,TIME=@TIME,Price=@Price,Save_Type=@Save_Type,Balance=@Balance,Bank_Type=@Bank_Type,Note=@Note,Create_By=@Create_By,Create_Time=@Create_Time,UpDate_By=@UpDate_By,UpDate_Time=@UpDate_Time,ImgUrl=@ImgUrl where Id=@Id";
                        SQLiteParameter[] parm = new SQLiteParameter[]{
                            new SQLiteParameter("@Id",index.Id),
                            new SQLiteParameter("@TIME",index.Time),
                            new SQLiteParameter("@Price",index.Price),
                            new SQLiteParameter("@Save_Type",index.SaveType),
                            new SQLiteParameter("@Balance",index.Balance),
                            new SQLiteParameter("@Bank_Type",index.BankType),
                            new SQLiteParameter("@Note",index.Note),
                            new SQLiteParameter("@Create_By",index.CreateBy),
                            new SQLiteParameter("@Create_Time",index.CreateTime),
                            new SQLiteParameter("@UpDate_By",index.UpdateBy),
                            new SQLiteParameter("@UpDate_Time",index.UpdateTime),
                            new SQLiteParameter("@ImgUrl",index.ImgUrl)
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
                LogHelper.Log.Error("批量保存银行卡信息出错", ex);
                return false;
            }
        }
    }
}


