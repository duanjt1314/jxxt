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
    public class DLTeacher : FTeacher
    {
        #region Add
        /// <summary>
        /// 增加收入记录表
        /// </summary>
        /// <param name="index">收入记录表对象</param>
        /// <returns></returns>
        public int Add(Teacher index)
        {
            string sql = string.Format("insert into Teacher(id,name,Sex,native_place,phone,qq,email,graduate_school,professional,good_subjects,self_assessment,create_by,create_time,update_by,update_time) values(@id,@name,@Sex,@native_place,@phone,@qq,@email,@graduate_school,@professional,@good_subjects,@self_assessment,@create_by,@create_time,@update_by,@update_time)");
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@id",index.Id),
                new SQLiteParameter("@name",index.Name),
                new SQLiteParameter("@Sex",index.Sex),
                new SQLiteParameter("@native_place",index.NativePlace),
                new SQLiteParameter("@phone",index.Phone),
                new SQLiteParameter("@qq",index.Qq),
                new SQLiteParameter("@email",index.Email),
                new SQLiteParameter("@graduate_school",index.GraduateSchool),
                new SQLiteParameter("@professional",index.Professional),
                new SQLiteParameter("@good_subjects",index.GoodSubjects),
                new SQLiteParameter("@self_assessment",index.SelfAssessment),
                new SQLiteParameter("@create_by",index.CreateBy),
                new SQLiteParameter("@create_time",index.CreateTime),
                new SQLiteParameter("@update_by",index.UpdateBy),
                new SQLiteParameter("@update_time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量新增收入记录表
        /// </summary>
        /// <param name="list">收入记录表对象集合</param>
        /// <returns>影响的行数</returns>
        public int Add(List<Teacher> list)
        {
            String sql = "";
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = string.Format("insert into Teacher(id,name,Sex,native_place,phone,qq,email,graduate_school,professional,good_subjects,self_assessment,create_by,create_time,update_by,update_time) values(@id,@name,@Sex,@native_place,@phone,@qq,@email,@graduate_school,@professional,@good_subjects,@self_assessment,@create_by,@create_time,@update_by,@update_time)");
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@id",index.Id),
                    new SQLiteParameter("@name",index.Name),
                    new SQLiteParameter("@Sex",index.Sex),
                    new SQLiteParameter("@native_place",index.NativePlace),
                    new SQLiteParameter("@phone",index.Phone),
                    new SQLiteParameter("@qq",index.Qq),
                    new SQLiteParameter("@email",index.Email),
                    new SQLiteParameter("@graduate_school",index.GraduateSchool),
                    new SQLiteParameter("@professional",index.Professional),
                    new SQLiteParameter("@good_subjects",index.GoodSubjects),
                    new SQLiteParameter("@self_assessment",index.SelfAssessment),
                    new SQLiteParameter("@create_by",index.CreateBy),
                    new SQLiteParameter("@create_time",index.CreateTime),
                    new SQLiteParameter("@update_by",index.UpdateBy),
                    new SQLiteParameter("@update_time",index.UpdateTime)
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
        public int Update(Teacher index)
        {
            string sql = "update Teacher set id=@id,name=@name,Sex=@Sex,native_place=@native_place,phone=@phone,qq=@qq,email=@email,graduate_school=@graduate_school,professional=@professional,good_subjects=@good_subjects,self_assessment=@self_assessment,create_by=@create_by,create_time=@create_time,update_by=@update_by,update_time=@update_time where id=@id";
            SQLiteParameter[] parm = new SQLiteParameter[]{
                new SQLiteParameter("@id",index.Id),
                new SQLiteParameter("@name",index.Name),
                new SQLiteParameter("@Sex",index.Sex),
                new SQLiteParameter("@native_place",index.NativePlace),
                new SQLiteParameter("@phone",index.Phone),
                new SQLiteParameter("@qq",index.Qq),
                new SQLiteParameter("@email",index.Email),
                new SQLiteParameter("@graduate_school",index.GraduateSchool),
                new SQLiteParameter("@professional",index.Professional),
                new SQLiteParameter("@good_subjects",index.GoodSubjects),
                new SQLiteParameter("@self_assessment",index.SelfAssessment),
                new SQLiteParameter("@create_by",index.CreateBy),
                new SQLiteParameter("@create_time",index.CreateTime),
                new SQLiteParameter("@update_by",index.UpdateBy),
                new SQLiteParameter("@update_time",index.UpdateTime)
            };
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, parm);
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(List<Teacher> list)
        {
            String sql = String.Empty;
            SQLiteParameter[] parm = null;
            List<SqlHashTable> hashTables = new List<SqlHashTable>();

            foreach (var index in list)
            {
                sql = "update Teacher set id=@id,name=@name,Sex=@Sex,native_place=@native_place,phone=@phone,qq=@qq,email=@email,graduate_school=@graduate_school,professional=@professional,good_subjects=@good_subjects,self_assessment=@self_assessment,create_by=@create_by,create_time=@create_time,update_by=@update_by,update_time=@update_time where id=@id";
                parm = new SQLiteParameter[]{
                    new SQLiteParameter("@id",index.Id),
                    new SQLiteParameter("@name",index.Name),
                    new SQLiteParameter("@Sex",index.Sex),
                    new SQLiteParameter("@native_place",index.NativePlace),
                    new SQLiteParameter("@phone",index.Phone),
                    new SQLiteParameter("@qq",index.Qq),
                    new SQLiteParameter("@email",index.Email),
                    new SQLiteParameter("@graduate_school",index.GraduateSchool),
                    new SQLiteParameter("@professional",index.Professional),
                    new SQLiteParameter("@good_subjects",index.GoodSubjects),
                    new SQLiteParameter("@self_assessment",index.SelfAssessment),
                    new SQLiteParameter("@create_by",index.CreateBy),
                    new SQLiteParameter("@create_time",index.CreateTime),
                    new SQLiteParameter("@update_by",index.UpdateBy),
                    new SQLiteParameter("@update_time",index.UpdateTime)
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
                String sql = "delete from Teacher where Id in (" + id + ")";
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
            String sql = "delete from Teacher";
            return SqlLiteHelper.ExecuteNonQuery(sql, CommandType.Text, null);
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere)
        {
            string sql = "delete from Teacher where 1=1 ";

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
                if (hash["Sex"] != null)
                {
                    sql += string.Format(" and Sex='{0}'", hash["Sex"]);
                }
                if (hash["NativePlace"] != null)
                {
                    sql += string.Format(" and native_place='{0}'", hash["NativePlace"]);
                }
                if (hash["Phone"] != null)
                {
                    sql += string.Format(" and phone='{0}'", hash["Phone"]);
                }
                if (hash["Qq"] != null)
                {
                    sql += string.Format(" and qq='{0}'", hash["Qq"]);
                }
                if (hash["Email"] != null)
                {
                    sql += string.Format(" and email='{0}'", hash["Email"]);
                }
                if (hash["GraduateSchool"] != null)
                {
                    sql += string.Format(" and graduate_school='{0}'", hash["GraduateSchool"]);
                }
                if (hash["Professional"] != null)
                {
                    sql += string.Format(" and professional='{0}'", hash["Professional"]);
                }
                if (hash["GoodSubjects"] != null)
                {
                    sql += string.Format(" and good_subjects='{0}'", hash["GoodSubjects"]);
                }
                if (hash["SelfAssessment"] != null)
                {
                    sql += string.Format(" and self_assessment='{0}'", hash["SelfAssessment"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sql += string.Format(" and create_by='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sql += string.Format(" and create_time='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sql += string.Format(" and update_by='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sql += string.Format(" and update_time='{0}'", hash["UpdateTime"]);
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
        public Teacher Select(string Id)
        {
            String sql = "select * from Teacher where Id=@Id";
            System.Data.SQLite.SQLiteParameter[] parm = new System.Data.SQLite.SQLiteParameter[]{
                new SQLiteParameter("@Id",Id),
            };
            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text, parm);

            if (dt.Rows.Count > 0)
            {
                List<Teacher> list = Life.Model.Common<Teacher>.ConvertToList(dt);
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
        public List<Teacher> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            List<Teacher> list = new List<Teacher>();

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
                if (hash["Sex"] != null)
                {
                    sqlWhere += string.Format(" and [Sex]='{0}'", hash["Sex"]);
                }
                if (hash["NativePlace"] != null)
                {
                    sqlWhere += string.Format(" and [native_place]='{0}'", hash["NativePlace"]);
                }
                if (hash["Phone"] != null)
                {
                    sqlWhere += string.Format(" and [phone]='{0}'", hash["Phone"]);
                }
                if (hash["Qq"] != null)
                {
                    sqlWhere += string.Format(" and [qq]='{0}'", hash["Qq"]);
                }
                if (hash["Email"] != null)
                {
                    sqlWhere += string.Format(" and [email]='{0}'", hash["Email"]);
                }
                if (hash["GraduateSchool"] != null)
                {
                    sqlWhere += string.Format(" and [graduate_school]='{0}'", hash["GraduateSchool"]);
                }
                if (hash["Professional"] != null)
                {
                    sqlWhere += string.Format(" and [professional]='{0}'", hash["Professional"]);
                }
                if (hash["GoodSubjects"] != null)
                {
                    sqlWhere += string.Format(" and [good_subjects]='{0}'", hash["GoodSubjects"]);
                }
                if (hash["SelfAssessment"] != null)
                {
                    sqlWhere += string.Format(" and [self_assessment]='{0}'", hash["SelfAssessment"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sqlWhere += string.Format(" and [create_by]='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sqlWhere += string.Format(" and [create_time]='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sqlWhere += string.Format(" and [update_by]='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sqlWhere += string.Format(" and [update_time]='{0}'", hash["UpdateTime"]);
                }
            }
            sqlWhere = "1=1 " + sqlWhere;
            #endregion

            DataTable dt = SqlLiteHelper.GetTable("Teacher",
                "[id],[name],[Sex],[native_place],[phone],[qq],[email],[graduate_school],[professional],[good_subjects],[self_assessment],[create_by],[create_time],[update_by],[update_time]",
                pageSize, start, sqlWhere, "name", "asc", out total);

            list = Model.Common<Teacher>.ConvertToList(dt);

            return list;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Teacher> Select(HashTableExp hash, String sqlWhere)
        {
            List<Teacher> list = new List<Teacher>();
            string sql = "select [id],[name],[card_no],[birth_day],[sex],[addr],[remark] from Teacher where 1=1 ";

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
                if (hash["Sex"] != null)
                {
                    sqlWhere += string.Format(" and [Sex]='{0}'", hash["Sex"]);
                }
                if (hash["NativePlace"] != null)
                {
                    sqlWhere += string.Format(" and [native_place]='{0}'", hash["NativePlace"]);
                }
                if (hash["Phone"] != null)
                {
                    sqlWhere += string.Format(" and [phone]='{0}'", hash["Phone"]);
                }
                if (hash["Qq"] != null)
                {
                    sqlWhere += string.Format(" and [qq]='{0}'", hash["Qq"]);
                }
                if (hash["Email"] != null)
                {
                    sqlWhere += string.Format(" and [email]='{0}'", hash["Email"]);
                }
                if (hash["GraduateSchool"] != null)
                {
                    sqlWhere += string.Format(" and [graduate_school]='{0}'", hash["GraduateSchool"]);
                }
                if (hash["Professional"] != null)
                {
                    sqlWhere += string.Format(" and [professional]='{0}'", hash["Professional"]);
                }
                if (hash["GoodSubjects"] != null)
                {
                    sqlWhere += string.Format(" and [good_subjects]='{0}'", hash["GoodSubjects"]);
                }
                if (hash["SelfAssessment"] != null)
                {
                    sqlWhere += string.Format(" and [self_assessment]='{0}'", hash["SelfAssessment"]);
                }
                if (hash["CreateBy"] != null)
                {
                    sqlWhere += string.Format(" and [create_by]='{0}'", hash["CreateBy"]);
                }
                if (hash["CreateTime"] != null)
                {
                    sqlWhere += string.Format(" and [create_time]='{0}'", hash["CreateTime"]);
                }
                if (hash["UpdateBy"] != null)
                {
                    sqlWhere += string.Format(" and [update_by]='{0}'", hash["UpdateBy"]);
                }
                if (hash["UpdateTime"] != null)
                {
                    sqlWhere += string.Format(" and [update_time]='{0}'", hash["UpdateTime"]);
                }
            }
            #endregion

            sql += sqlWhere;

            DataTable dt = SqlLiteHelper.GetTable(sql, CommandType.Text);
            list = Model.Common<Teacher>.ConvertToList(dt);
            return list;
        }

        #endregion

        
    }
}


