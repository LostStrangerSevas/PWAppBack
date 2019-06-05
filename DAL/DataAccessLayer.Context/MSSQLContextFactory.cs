using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Context
{
    /// <summary>
    /// Фабрика контекстов используется для создания и применения миграций
    /// Этот класс нужен для работы механизма миграций БД, 
    /// который ищет этот класс в проекте и юзает его самостоятельно
    /// </summary>
    public class MSSQLContextFactory : IDesignTimeDbContextFactory<MSSQLContext>
    {
        /// <summary>
        /// Метод для создания БД на основе миграций
        /// </summary>
        /// <param name="args">Массив аргументов</param>
        /// <returns>MSSQLContext</returns>
        public MSSQLContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MSSQLContext>();
            var projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new[] { @"bin\" }, StringSplitOptions.None)[0];
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            var conn = "DBConnection";
            var connectionString = configuration.GetConnectionString(conn);
            builder.UseSqlServer(connectionString);
            return new MSSQLContext(builder.Options);
        }
    }
}
