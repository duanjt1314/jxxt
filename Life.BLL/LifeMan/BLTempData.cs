﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Life.Model.LifeMan;
using System.Linq.Expressions;
using Life.Factory.LifeMan;
using Life.Common;

namespace Life.BLL.LifeMan
{
    public class BLTempData
    {
        FTempData dLTempData = FactoryManage.GetTempData();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(TempData index)
        {
            return dLTempData.Add(index);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<TempData> list)
        {
            return dLTempData.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(TempData index)
        {
            return dLTempData.Update(index);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLTempData.Delete(ids);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLTempData.Delete();
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete(HashTableExp hash,String sqlWhere="")
        {
            return dLTempData.Delete(hash,sqlWhere);
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<TempData> Select(int pageSize, int start,HashTableExp hash, out int total,String sqlWhere)
        {
            return dLTempData.Select(pageSize, start, hash, out total,sqlWhere);
        }

        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<TempData> Select(HashTableExp hash,String sqlWhere="")
        {
            return dLTempData.Select(hash,sqlWhere);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public TempData Select(string id)
        {
            return dLTempData.Select(id);
        }
    }
}

