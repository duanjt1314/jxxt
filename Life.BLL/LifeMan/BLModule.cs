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
    public class BLModule
    {
        FModule dLModule = FactoryManage.GetModule();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(Module module)
        {
            return dLModule.Add(module);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<Module> list)
        {
            return dLModule.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(Module module)
        {
            return dLModule.Update(module);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLModule.Delete(ids);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLModule.Delete();
        }            

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Module> Select(int pageSize, int start,HashTableExp hash, out int total,String sqlWhere="")
        {
            return dLModule.Select(pageSize, start, hash, out total,sqlWhere);
        }
        
        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public Module Select(string id)
        {
            return dLModule.Select(id);
        }

        /// <summary>
        /// 根据用户编号查询用户模块(排除重复项)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Module> SelectByUserId(String userId,String status=null)
        {
            return dLModule.SelectByUserId(userId,status);
        }

        public List<Module> Select(HashTableExp hash, String sqlWhere = "")
        {
            return dLModule.Select(hash, sqlWhere);
        }
    }
}
