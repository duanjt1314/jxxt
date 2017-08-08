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
    public class BLUserToRole
    {
        FUserToRole dLUserToRole = FactoryManage.GetUserToRole();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(UserToRole userToRole)
        {
            return dLUserToRole.Add(userToRole);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<UserToRole> list)
        {
            return dLUserToRole.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(UserToRole userToRole)
        {
            return dLUserToRole.Update(userToRole);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLUserToRole.Delete(ids);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLUserToRole.Delete();
        }            
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<UserToRole> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere="")
        {
            return dLUserToRole.Select(pageSize, start, hash, out total,sqlWhere);

        }

        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<UserToRole> Select(HashTableExp hash,String sqlWhere="")
        {
            return dLUserToRole.Select(hash,sqlWhere) ;
        }


        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public UserToRole Select(string id)
        {
            return dLUserToRole.Select(id);
        }

        /// <summary>
        /// 保存用户对应的角色信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="moduleIds">模块编号数组</param>
        /// <returns></returns>
        public int Save(String userId, String[] roleIds)
        {
            return dLUserToRole.Save(userId, roleIds);
        }
    }
}


