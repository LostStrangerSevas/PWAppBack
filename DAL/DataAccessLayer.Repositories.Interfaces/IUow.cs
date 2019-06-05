using System;
using System.Threading.Tasks;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Repositories.Interfaces
{
    /// <summary>
    /// Объединённый интерфейс в рамках unit of worK
    /// </summary>
    public interface IUow : IDisposable
    {
        /// <summary>
        /// Балансы
        /// </summary>
        IBalanceRepository<Balance> Balances { get; }
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        IBalanceStorageRepository<BalanceStorage> BalanceStorages { get; }
        /// <summary>
        /// Транзакции
        /// </summary>
        ITransactionRepository<Transaction> Transactions { get; }
        /// <summary>
        /// Сохранение изменений асинхронно
        /// </summary>
        Task<int> SaveAsync();        
        /// <summary>
        /// Менеджер пользователей
        /// </summary>
        UserManager<User> UserManager { get; }
        /// <summary>
        /// Менеджер авторизаций
        /// </summary>
        SignInManager<User> SignManager { get; }     
    }
}
