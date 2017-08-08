using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Life.Model.LifeMan;
using System.Linq.Expressions;
using Life.Factory.LifeMan;
using Life.Common;
using Life.Factory.JiaoXue;
using Life.Model.JiaoXue;

namespace Life.BLL.LifeMan
{
    public class BLTeacher
    {
        FTeacher dLTeacher = FactoryManage.GetTeacher();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(Teacher index)
        {
            return dLTeacher.Add(index);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<Teacher> list)
        {
            return dLTeacher.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(Teacher index)
        {
            return dLTeacher.Update(index);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLTeacher.Delete(ids);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLTeacher.Delete();
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere = "")
        {
            return dLTeacher.Delete(hash, sqlWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Teacher> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            return dLTeacher.Select(pageSize, start, hash, out total, sqlWhere);
        }

        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<Teacher> Select(HashTableExp hash, String sqlWhere = "")
        {
            return dLTeacher.Select(hash, sqlWhere);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public Teacher Select(string id)
        {
            return dLTeacher.Select(id);
        }
        
    }
}

