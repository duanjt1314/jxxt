using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Life.DALCommon.Repository
{
    /// <summary>
    /// EF业务逻辑公共接口
    /// </summary>
    /// <typeparam name="TEntity">处理的对象</typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>IQueryable集合</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 从数据库中获取过滤数据
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>IQueryable集合</returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 从数据库中获取过滤数据，并分页取出
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">过滤条件</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="index">当前页</param>
        /// <param name="size">显示多少条数据</param>
        /// <returns>IQueryable集合</returns>
        IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        /// 带过滤器和OrderBy从数据库获取对象包括领域
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="includeProperties">字符串分割'，'</param>
        /// <returns>IQueryable集合</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="includeProperties">字符串分割'，'</param>
        /// <param name="index">当前页</param>
        /// <param name="size">显示多少条数据</param>
        /// <returns>IQueryable集合</returns>
        IQueryable<TEntity> GetPaged(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int index = 0, int size = 50);

        /// <summary>
        /// 获取对象是否存在于数据库中指定的过滤器中。
        /// </summary>
        /// <param name="predicate">指定过滤器表达式</param>
        /// <returns>是否存在</returns>
        bool Contains(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找键的对象
        /// </summary>
        /// <param name="keys">指定的搜索键</param>
        /// <returns>TEntity 对象</returns>
        TEntity Find(params object[] keys);

        /// <summary>
        /// 查找指定的表达式的对象
        /// </summary>
        /// <param name="predicate">指定过滤器表达式</param>
        /// <returns>TEntity 对象</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 创建一个新的数据库对象
        /// </summary>
        /// <param name="t">指定创建一个新的对象.</param>
        void Create(TEntity t);

        /// <summary>
        /// 从数据库中删除对象
        /// </summary>
        /// <param name="t">指定创建一个删除的对象</param>        
        void Delete(TEntity t);

        /// <summary>
        /// 从数据库中删除对象指定的过滤器表达式
        /// </summary>
        /// <param name="predicate">过滤器表达式</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 更新对象的变化，并保存到数据库.
        /// </summary>
        /// <param name="t">指定保存对象.</param>
        void Update(TEntity t);

        /// <summary>
        /// 保存所有变化
        /// </summary>
        /// <returns>影响的行数</returns>
        int Save();

        /// <summary>
        /// 获取对象总数计数.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 执行原始SQL语句获取对象.
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters);

        /// <summary>
        /// 执行原始SQL语句，返回影响的行数
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteSql(string query, params object[] parameters);
        
    }
}
