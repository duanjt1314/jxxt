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
    public class BLArtCategory
    {
        FArtCategory dLArtCategory = FactoryManage.GetArtCategory();

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Add(ArtCategory artCategory)
        {
            return dLArtCategory.Add(artCategory);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns>影响的行数</returns>
        public int Add(List<ArtCategory> list)
        {
            return dLArtCategory.Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(ArtCategory artCategory)
        {
            return dLArtCategory.Update(artCategory);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update(List<ArtCategory> artCategories)
        {
            return dLArtCategory.Update(artCategories);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Delete(string ids)
        {
            return dLArtCategory.Delete(ids);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return dLArtCategory.Delete();
        }    
        
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Delete(HashTableExp hash,String sqlWhere="")
        {
            return dLArtCategory.Delete(hash,sqlWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="start">排除多少条数据</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="total">总共多少条</param>
        /// <returns>数据集合</returns>
        public List<VArtCategory> Select(int pageSize, int start, HashTableExp hash, out int total,String sqlWhere)
        {
            return dLArtCategory.Select(pageSize, start, hash, out total,sqlWhere);
        }

        /// <summary>
        /// 按条件查询所有符合条件的数据(不分页)
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>信息集合</returns>
        public List<ArtCategory> Select(HashTableExp hash,String sqlWhere="")
        {
            return dLArtCategory.Select(hash,sqlWhere) ;
        }


    }
}


