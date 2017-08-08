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
    public class BLUsers
    {
        FUsers dLUsers = FactoryManage.GetUsers();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(Users users)
        {
            return dLUsers.Add(users);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<Users> list)
        {
            return dLUsers.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(Users users)
        {
            return dLUsers.Update(users);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLUsers.Delete(ids);
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLUsers.Delete();
        }            

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据集合</returns>
        public List<Users> Select()
        {
            return dLUsers.Select();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<Users> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere)
        {
            return dLUsers.Select(pageSize, start, hash, out total, sqlWhere);
        }

        public Users Login(string loginId, string loginPwd)
        {
            return dLUsers.Login(loginId, loginPwd);
        }

        /// <summary>
        /// 根据编号查询
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>数据集合</returns>
        public Users Select(string id)
        {
            return dLUsers.Select(id);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Users> Select(HashTableExp hash)
        {
            return dLUsers.Select(hash,"");
        }

    }
}


