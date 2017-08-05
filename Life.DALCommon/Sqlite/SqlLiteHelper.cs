﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SQLite;
using System.Data;

namespace Life.DALCommon.Sqlite
{
    public class SqlLiteHelper
    {
        /// <summary>
        /// 获得数据库连接字符串
        /// </summary>
        public static String connStr = ConfigurationManager.ConnectionStrings["dbSqlite"].ConnectionString;

        /// <summary>
        /// 提取Command公共的方法
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="comm">Command对象</param>
        /// <param name="tran">事务</param>
        /// <param name="cmdText">数据库执行语句</param>
        /// <param name="cmdType">指定执行语句的类型（sql语句或存储过程）</param>
        /// <param name="parm">参数</param>
        private static void PreparedExecute(SQLiteConnection conn, SQLiteCommand comm, SQLiteTransaction tran, string cmdText, CommandType cmdType, SQLiteParameter[] parm)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //为Command指定参数
            comm.Connection = conn;
            comm.CommandText = cmdText;
            comm.CommandType = cmdType;

            //指定事务
            if (tran != null)
            {
                comm.Transaction = tran;
            }

            //指定参数
            if (parm != null)
            {
                comm.Parameters.AddRange(parm);
            }
        }

        /// <summary>
        /// 查询数据库的数据
        /// </summary>
        /// <param name="cmdText">数据库执行语句</param>
        /// <param name="cmdType">指定执行语句的类型（sql语句或存储过程）</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public static SQLiteDataReader ExecuteReader(string cmdText, CommandType cmdType, params SQLiteParameter[] parm)
        {

            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand comm = new SQLiteCommand();

            try
            {
                PreparedExecute(conn, comm, null, cmdText, cmdType, parm);
                SQLiteDataReader sr = comm.ExecuteReader();
                return sr;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行更新语句（无事务）
        /// </summary>
        /// <param name="cmdText">数据库执行语句</param>
        /// <param name="cmdType">指定执行语句的类型（sql语句或存储过程）</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, params SQLiteParameter[] parm)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand comm = new SQLiteCommand();
                PreparedExecute(conn, comm, null, cmdText, cmdType, parm);
                int num = comm.ExecuteNonQuery();
                return num;
            }
        }

        /// <summary>
        /// 根据传入的SQL语句或存储过程返回第一行第一列的数据
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params SQLiteParameter[] parm)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand comm = new SQLiteCommand();
                PreparedExecute(conn, comm, null, cmdText, cmdType, parm);
                object obj = comm.ExecuteScalar();
                return obj;
            }
        }

        /// <summary>
        /// 执行更新语句（有事务）
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="cmdText">数据库执行语句</param>
        /// <param name="cmdType">指定执行语句的类型（sql语句或存储过程）</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SQLiteTransaction tran, string cmdText, CommandType cmdType, params SQLiteParameter[] parm)
        {
            SQLiteCommand comm = new SQLiteCommand();
            PreparedExecute(tran.Connection, comm, tran, cmdText, cmdType, parm);
            int num = comm.ExecuteNonQuery();
            return num;
        }

        /// <summary>
        /// 根据传入的SQL语句或存储过程返回DataTable
        /// </summary>
        /// <param name="cmdText">Sql语句或存储过程</param>
        /// <param name="cmdType">指定是sql语句还是存储过程</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public static DataTable GetTable(string cmdText, CommandType cmdType, params SQLiteParameter[] parm)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                DataSet ds = new DataSet();
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmdText, conn);
                adapter.SelectCommand.CommandType = cmdType;
                if (parm != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parm);
                }

                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 同时执行多条sql语句的增删改操作
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Int32 ExecuteSql(List<SqlHashTable> list)
        {
            SQLiteConnection conn = null;
            SQLiteTransaction tran = null;
            int result = 0;
            try
            {
                conn = new SQLiteConnection(connStr);
                conn.Open();
                tran = conn.BeginTransaction();
                foreach (SqlHashTable hash in list)
                {
                    SQLiteCommand comm = new SQLiteCommand(hash.Sql, conn);
                    if (hash.Par != null)
                    {
                        comm.Parameters.AddRange(hash.Par);
                    }
                    comm.Transaction = tran;
                    result += comm.ExecuteNonQuery();
                }
                tran.Commit();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 分页查询数据并返回DataTable的公共方法
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="field">需要查询的字段</param>
        /// <param name="pageSize">每页显示数据的条数</param>
        /// <param name="start">排除的数据量</param>
        /// <param name="sqlWhere">where条件</param>
        /// <param name="sortName">排序名称</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns></returns>
        public static DataTable GetTable(String tableName, String field, int pageSize, int start, String sqlWhere, String sortName, String sortOrder, out Int32 total)
        {
            String sql = String.Format("select {0} from {1} where {2} order by {3} {4} limit {5} offset {6}",
                field, tableName, sqlWhere, sortName, sortOrder, pageSize, start);
            DataTable dt = GetTable(sql, CommandType.Text, null);
            
            sql = "select count(1) from "+tableName+" where " + sqlWhere;
            total =Convert.ToInt32(SqlLiteHelper.ExecuteScalar(sql, CommandType.Text, null)); 
            
            return dt;
        }
    }
    public class SqlHashTable
    {
        public SqlHashTable()
        {

        }

        public SqlHashTable(string sql, SQLiteParameter[] par)
        {
            this.sql = sql;
            this.par = par;
        }

        private string sql;
        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql
        {
            get { return sql; }
            set { sql = value; }
        }
        private SQLiteParameter[] par;
        /// <summary>
        /// SQL语句的参数数组
        /// </summary>
        public SQLiteParameter[] Par
        {
            get { return par; }
            set { par = value; }
        }
    }
}

