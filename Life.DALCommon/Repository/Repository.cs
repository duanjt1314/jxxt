using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Configuration;
using System.Linq.Expressions;
using System.Data;

namespace Life.DALCommon.Repository
{
    public class Repository<TEntity> : DbContext, IDisposable, IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// TEntity 操作集合
        /// </summary>
        public DbSet<TEntity> DbSet{get;set;}

        /// <summary>
        /// 数据库链接
        /// </summary>
        private readonly static String connectionString = ConfigurationManager.ConnectionStrings["dbSqlServer"].ConnectionString;

        /// <summary>
        /// 初始化当前对象
        /// </summary>
        public Repository()
            : base(connectionString)
        {
        }
        /// <summary>
        /// 初始化当前对象
        /// </summary>
        public Repository(String conSting)
            : base(conSting)
        {
        }
        /// <summary>
        /// 获得所有数据
        /// </summary>
        /// <returns>IQueryable集合</returns>
        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();            
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="includeProperties">字符串分割'，'</param>
        /// <param name="index">当前页</param>
        /// <param name="size">显示多少条数据</param>
        /// <returns>IQueryable集合</returns>
        public IQueryable<TEntity> GetPaged(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _reset = Get(filter, orderBy, includeProperties);
            _reset = skipCount == 0 ? _reset.Take(size) : _reset.Skip(skipCount).Take(size);
            return _reset.AsQueryable();
        }
        /// <summary>
        /// 带过滤器和OrderBy从数据库获取对象包括领域
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="includeProperties">字符串分割'，'</param>
        /// <returns>IQueryable集合</returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }
        
        /// <summary>
        /// 从数据库中获取过滤数据
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>IQueryable集合</returns>
        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TEntity>();
        }
        /// <summary>
        /// 从数据库中获取过滤数据，并分页取出
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">过滤条件</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="index">当前页</param>
        /// <param name="size">显示多少条数据</param>
        /// <returns>IQueryable集合</returns>
        public IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }
        /// <summary>
        /// 获取对象是否存在于数据库中指定的过滤器中。
        /// </summary>
        /// <param name="predicate">指定过滤器表达式</param>
        /// <returns>是否存在</returns>
        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }
        /// <summary>
        /// 查找键的对象
        /// </summary>
        /// <param name="keys">指定的搜索键</param>
        /// <returns>TEntity 对象</returns>
        public virtual TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }
        /// <summary>
        /// 查找指定的表达式的对象
        /// </summary>
        /// <param name="predicate">指定过滤器表达式</param>
        /// <returns>TEntity 对象</returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 创建一个新的数据库对象
        /// </summary>
        /// <param name="t">指定创建一个新的对象.</param>
        public virtual void Create(TEntity t)
        {
            DbSet.Add(t);
        }
        /// <summary>
        /// 从数据库中删除对象
        /// </summary>
        /// <param name="t">指定创建一个删除的对象</param> 
        public virtual void Delete(TEntity t)
        {
            if (this.Entry(t).State == EntityState.Detached)
            {
                DbSet.Attach(t);
            }
            DbSet.Remove(t);
        }
        /// <summary>
        /// 从数据库中删除对象指定的过滤器表达式
        /// </summary>
        /// <param name="predicate">过滤器表达式</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var toDelete = Filter(predicate);
            foreach (var obj in toDelete)
            {
                DbSet.Remove(obj);
            }
        }
        /// <summary>
        /// 更新对象的变化，并保存到数据库.
        /// </summary>
        /// <param name="t">指定保存对象.</param>
        public void Update(TEntity t)
        {
            var entry = this.Entry(t);
            DbSet.Attach(t);
            entry.State = EntityState.Modified;
        }
        /// <summary>
        /// 保存所有变化
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Save()
        {
            return this.SaveChanges();
        }
        /// <summary>
        /// 获取对象总数计数.
        /// </summary>
        public int Count
        {
            get { return DbSet.Count(); }
        }
        /// <summary>
        /// 执行原始SQL语句获取对象.
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {            
            return this.Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }

        /// <summary>
        /// 执行原始SQL语句，返回影响的行数
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        public int ExecuteSql(string query, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(query, parameters);
        }
        

        /// <summary>
        /// 销毁当前对象
        /// </summary>
        public void Dispose()
        {
            if (this != null)
                this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
