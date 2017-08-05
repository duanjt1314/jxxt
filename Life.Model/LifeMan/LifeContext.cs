using System.Data.Entity;

namespace Life.Model.LifeMan
{
    public class LifeContext : DbContext
    {
        #region Constructor
        /// <summary>
        /// 初始化当前对象
        /// </summary>
        public LifeContext() : base("dbContext") { }
        
        #endregion

        #region Class
        /// <summary>
        /// 文章类型表
        /// </summary>
        public DbSet<ArtCategory> ArtCategorys { get; set; }

        /// <summary>
        /// 文章表
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// 银行卡操作记录表
        /// </summary>
        public DbSet<BankCard> BankCards { get; set; }

        /// <summary>
        /// 字典表
        /// </summary>
        public DbSet<Diction> Dictions { get; set; }

        /// <summary>
        /// 收入记录表
        /// </summary>
        public DbSet<Income> Incomes { get; set; }

        /// <summary>
        /// 生活费操作管理
        /// </summary>
        public DbSet<LifingCost> LifingCosts { get; set; }

        /// <summary>
        /// 生活费日志记录表
        /// </summary>
        public DbSet<LifingCostLog> LifingCostlogs { get; set; }

        /// <summary>
        /// 模块表
        /// </summary>
        public DbSet<Module> Modules { get; set; }

        /// <summary>
        /// 角色模块对应表
        /// </summary>
        public DbSet<RoleToModule> RoleToModules { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Roles> Roless { get; set; }

        /// <summary>
        /// 用户角色对应表
        /// </summary>
        public DbSet<UserToRole> UserToRoles { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<Users> Userss { get; set; }

        #endregion
    }
}
