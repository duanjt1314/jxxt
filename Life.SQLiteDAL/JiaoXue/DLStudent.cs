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
using Life.Factory.JiaoXue;
using Life.Model.JiaoXue;

namespace Life.SQLiteDAL.JiaoXue
{
    ///<summary>
    ///2014-03-04
    ///段江涛
    ///</summary>
    public class DLStudent : FStudent
    {
        #region Add
        /// <summary>
        /// 增加收入记录表
        /// </summary>
        /// <param name="index">收入记录表对象</param>
        /// <returns></returns>
        public int Add(Student index)
        {
            string sql = string.Format("insert into Student(id,name,card_no,birth_day,sex,addr,remark) values(@id,@name,@card_no,@birth_day,@sex,@addr,@remark)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@id",index.Id),
                new SQLiteParameter("@name",index.Name),
                new SQLiteParameter("@card_no",index.CardNo),
                new SQLiteParameter("@birth_day",index.BirthDay),
                new SQLiteParameter("@sex",index.Sex),
                new SQLiteParameter("@addr",index.Addr),
                new SQLiteParameter("@remark",index.Remark)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增收入记录表
        /// </summary>
        /// <param name="list">收入记录表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Student> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Student(id,name,card_no,birth_day,sex,addr,remark) values(@id,@name,@card_no,@birth_day,@sex,@addr,@remark)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@id",index.Id),
                    new SQLiteParameter("@name",index.Name),
                    new SQLiteParameter("@card_no",index.CardNo),
                    new SQLiteParameter("@birth_day",index.BirthDay),
                    new SQLiteParameter("@sex",index.Sex),
                    new SQLiteParameter("@addr",index.Addr),
                    new SQLiteParameter("@remark",index.Remark)
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
        public int Update(Student index)
        {
            string sql = "update Student set id=@id,name=@name,card_no=@card_no,birth_day=@birth_day,sex=@sex,addr=@addr,remark=@remark where id=@id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@id",index.Id),
                new SQLiteParameter("@name",index.Name),
                new SQLiteParameter("@card_no",index.CardNo),
                new SQLiteParameter("@birth_day",index.BirthDay),
                new SQLiteParameter("@sex",index.Sex),
                new SQLiteParameter("@addr",index.Addr),
                new SQLiteParameter("@remark",index.Remark)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<Student> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Student set id=@id,name=@name,card_no=@card_no,birth_day=@birth_day,sex=@sex,addr=@addr,remark=@remark where id=@id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@id",index.Id),
                    new SQLiteParameter("@name",index.Name),
                    new SQLiteParameter("@card_no",index.CardNo),
                    new SQLiteParameter("@birth_day",index.BirthDay),
                    new SQLiteParameter("@sex",index.Sex),
                    new SQLiteParameter("@addr",index.Addr),
                    new SQLiteParameter("@remark",index.Remark)
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
                String sql = "delete from Student where Id in (" + id + ")";
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
            String sql = "delete from Student";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Student where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sql += string.Format(" and id='{0}'", hash["Id"]);
                }
                if (hash["Name"] != null)
                {
                    sql += string.Format(" and name='{0}'", hash["Name"]);
                }
                if (hash["CardNo"] != null)
                {
                    sql += string.Format(" and card_no='{0}'", hash["CardNo"]);
                }
                if (hash["BirthDay"] != null)
                {
                    sql += string.Format(" and birth_day='{0}'", hash["BirthDay"]);
                }
                if (hash["Sex"] != null)
                {
                    sql += string.Format(" and sex='{0}'", hash["Sex"]);
                }
                if (hash["Addr"] != null)
                {
                    sql += string.Format(" and addr='{0}'", hash["Addr"]);
                }
                if (hash["Remark"] != null)
                {
                    sql += string.Format(" and remark='{0}'", hash["Remark"]);
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
        public Student Select(string Id)
        {
            String sql = "select * from Student where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);

            if (dt.Rows.Count > 0)
            {
                List<Student> list = Life.Model.Common<Student>.ConvertToList(dt);
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
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
        public List<Student> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Student> list = new List<Student>();

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and [id]='{0}'", hash["Id"]);
                }
                if (hash["Name"] != null)
                {
                    sqlWhere += string.Format(" and [name]='{0}'", hash["Name"]);
                }
                if (hash["CardNo"] != null)
                {
                    sqlWhere += string.Format(" and [card_no]='{0}'", hash["CardNo"]);
                }
                if (hash["BirthDay"] != null)
                {
                    sqlWhere += string.Format(" and [birth_day]='{0}'", hash["BirthDay"]);
                }
                if (hash["Sex"] != null)
                {
                    sqlWhere += string.Format(" and [sex]='{0}'", hash["Sex"]);
                }
                if (hash["Addr"] != null)
                {
                    sqlWhere += string.Format(" and [addr]='{0}'", hash["Addr"]);
                }
                if (hash["Remark"] != null)
                {
                    sqlWhere += string.Format(" and [remark]='{0}'", hash["Remark"]);
                }
            }

            sqlWhere = "1=1 " + sqlWhere;
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Student",
                "[id],[name],[card_no],[birth_day],[sex],[addr],[remark]",
                pageSize, start, sqlWhere, "name", "asc", out total);

            list = Model.Common<Student>.ConvertToList(dt);

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Student> Select(HashTableExp hash, String sqlWhere)
        {
            List<Student> list = new List<Student>();
            string sql = "select [id],[name],[card_no],[birth_day],[sex],[addr],[remark] from Student where 1=1 ";

            #region 查询条件
            if (hash != null)
            {
                if (hash["Id"] != null)
                {
                    sqlWhere += string.Format(" and [id]='{0}'", hash["Id"]);
                }
                if (hash["Name"] != null)
                {
                    sqlWhere += string.Format(" and [name]='{0}'", hash["Name"]);
                }
                if (hash["CardNo"] != null)
                {
                    sqlWhere += string.Format(" and [card_no]='{0}'", hash["CardNo"]);
                }
                if (hash["BirthDay"] != null)
                {
                    sqlWhere += string.Format(" and [birth_day]='{0}'", hash["BirthDay"]);
                }
                if (hash["Sex"] != null)
                {
                    sqlWhere += string.Format(" and [sex]='{0}'", hash["Sex"]);
                }
                if (hash["Addr"] != null)
                {
                    sqlWhere += string.Format(" and [addr]='{0}'", hash["Addr"]);
                }
                if (hash["Remark"] != null)
                {
                    sqlWhere += string.Format(" and [remark]='{0}'", hash["Remark"]);
                }
            }

            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            list = Model.Common<Student>.ConvertToList(dt);
            return list;
        }

        #endregion

        
    }
}


