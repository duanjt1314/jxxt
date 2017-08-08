using System;
using System.Collections.Generic;
using Life.Common;
using Life.Model.LifeMan;

namespace Life.Factory.LifeMan
{
    public interface FBankType
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        int Add(BankType index);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        int Add(List<BankType> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Update(BankType users);

        /// <summary>
        ///  批量修改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Update(List<BankType> list);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号的集合，如：1,2,3...</param>
        /// <returns>影响的行数</returns>
        int Delete(string ids);

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        int Delete();

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <returns></returns>
        int Delete(HashTableExp hash, String sqlWhere);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        List<BankType> Select(int pageSize, int start, HashTableExp hash, out int total, String sqlWhere);

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        BankType Select(string Id);

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <returns></returns>
        List<BankType> Select(HashTableExp hash, String sqlWhere);
    }
}