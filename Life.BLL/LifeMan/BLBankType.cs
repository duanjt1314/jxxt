using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Life.Factory.LifeMan;
using Life.Model.LifeMan;
using Life.Common;

namespace Life.BLL.LifeMan
{
    public class BLBankType
    {
        FBankType dLBankType = FactoryManage.GetBankType();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(BankType banktype)
        {
            return dLBankType.Add(banktype);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<BankType> list)
        {
            return dLBankType.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(BankType banktype)
        {
            return dLBankType.Update(banktype);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLBankType.Delete(ids);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLBankType.Delete();
        }
        
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据集合</returns>
        public List<BankType> Select(HashTableExp hash,String sqlWhere)
        {
            return dLBankType.Select(hash,sqlWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<BankType> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere)
        {
            return dLBankType.Select(pageSize, start, hash, out total,sqlWhere);
        }
        
        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public BankType Select(string id)
        {
            return dLBankType.Select(id);
        }

    }
}