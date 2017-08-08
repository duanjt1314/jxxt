using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Life.Model.LifeMan;
using System.Linq.Expressions;
using Life.Common;

namespace Life.Factory.LifeMan
{
    public interface FModule
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        int Add(Module index);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        int Add(List<Module> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Update(Module users);

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
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        List<Module> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere);

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        Module Select(string id);

        /// <summary>
        /// 按条件查询数据
        /// </summary>
        /// <returns></returns>
        List<Module> Select(HashTableExp hash, String sqlWhere);

        /// <summary>
        /// 根据用户编号查询用户模块(排除重复项)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Module> SelectByUserId(String userId, String status);
    }
}

