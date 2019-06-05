using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models.ModelBuilderHelpers;
using DataAccessLayer.Models.ModelBuilderMapping;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Models.Classes;


namespace DataAccessLayer.Context
{
    /// <summary>
    /// Класс контекста данных
    /// </summary>
    public class MSSQLContext : IdentityDbContext<User, Role, string>
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
        {
        }
        #endregion

        #region DBSets
        /// <summary>
        /// Балансы
        /// </summary>
        public DbSet<Balance> Balances { get; set; }
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        public DbSet<BalanceStorage> BalanceStorages { get; set; }
        /// <summary>
        /// Транзакции
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }       
        #endregion

        #region methods
        /// <summary>
        /// Переопределение OnConfiguring, указывая на файл с настройками appsettings.json и на секцию подключения DBConnection
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            var conn = "DBConnection";
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(conn));
        }
        /// <summary>
        /// Переопределение OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //удаление возможных каскадных обновлений (иначе ошибка создания базы при миграции)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.BuildEntity<Balance, BalanceMapping>();
            modelBuilder.BuildEntity<BalanceStorage, BalanceStorageMapping>();
            modelBuilder.BuildEntity<Transaction, TransactionMapping>();
        }
        #endregion
    }
}
