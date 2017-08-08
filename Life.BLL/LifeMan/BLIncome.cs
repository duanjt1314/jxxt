using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Life.Model.LifeMan;
using System.Linq.Expressions;
using Life.Factory.LifeMan;
using Life.Common;

namespace Life.BLL.LifeMan
{
    public class BLIncome
    {
        FIncome dLIncome = FactoryManage.GetIncome();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(Income index)
        {
            return dLIncome.Add(index);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<Income> list)
        {
            return dLIncome.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(Income index)
        {
            return dLIncome.Update(index);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLIncome.Delete(ids);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLIncome.Delete();
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete(HashTableExp hash, String sqlWhere = "")
        {
            return dLIncome.Delete(hash, sqlWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VIncome> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere)
        {
            return dLIncome.Select(pageSize, start, hash, out total, sqlWhere);
        }

        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<Income> Select(HashTableExp hash, String sqlWhere = "")
        {
            return dLIncome.Select(hash, sqlWhere);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public Income Select(string id)
        {
            return dLIncome.Select(id);
        }

        /// <summary>
        /// 根据条件查询收入总金额
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public double GetTotalPrice(HashTableExp hash, string sqlWhere)
        {
            return dLIncome.GetTotalPrice(hash, sqlWhere);
        }

        /// <summary>
        /// 批量修改自定义分组信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ModifyCusGroup(String ids, String value)
        {
            return dLIncome.ModifyCusGroup(ids, value);
        }

        /// <summary>
        /// 查询所有的自定义分组名称
        /// </summary>
        /// <returns></returns>  
        public List<string> GetAllCusGroup()
        {
            return dLIncome.GetAllCusGroup();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public bool Save(List<Income> list)
        {
            return dLIncome.Save(list);
        }

    }
}

