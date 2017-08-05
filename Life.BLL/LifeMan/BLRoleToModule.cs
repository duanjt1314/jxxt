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
    public class BLRoleToModule
    {
        FRoleToModule dLRoleToModule = FactoryManage.GetRoleToModule();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(RoleToModule roleToModule)
        {
            return dLRoleToModule.Add(roleToModule);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<RoleToModule> list)
        {
            return dLRoleToModule.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(RoleToModule roleToModule)
        {
            return dLRoleToModule.Update(roleToModule);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLRoleToModule.Delete();
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete(HashTableExp hash,String sqlWhere="")
        {
            return dLRoleToModule.Delete(hash,sqlWhere);
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<RoleToModule> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere="")
        {
            return dLRoleToModule.Select(pageSize, start, hash, out total,sqlWhere);
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<RoleToModule> Select(HashTableExp hash, String sqlWhere = "")
        {
            return dLRoleToModule.Select(hash, sqlWhere);
        }

        /// <summary>
        /// 保存角色对应的模块信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="moduleIds">模块编号数组</param>
        /// <returns></returns>
        public int Save(String roleId, String[] moduleIds)
        {
            return dLRoleToModule.Save(roleId, moduleIds);
        }
    }
}


