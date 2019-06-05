using System;
using System.Threading.Tasks;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.Repositories.Interfaces.ModelInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation.Repositories
{
    /// <summary>
    /// Реализация IUow
    /// </summary>
    public class Uow : IUow
    {
        #region props
        /// <summary>
        /// Контекст данных
        /// </summary>
        private readonly DbContext _db;
        /// <summary>
        /// Репозиторий балансов
        /// </summary>
        private BalanceRepository _balanceRepository;
        /// <summary>
        /// Репозиторий истории балансов транзакций
        /// </summary>
        private BalanceStorageRepository _balanceStorageRepository;
        /// <summary>
        /// Репозиторий транзакций
        /// </summary>
        private TransactionRepository _transactionRepository;
        /// <summary>
        /// Признак ликвидации
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// Менеджер пользователей
        /// </summary>
        public UserManager<User> UserManager { get; }
        /// <summary>
        /// Менеджер авторизаций
        /// </summary>
        public SignInManager<User> SignManager { get; }
        /// <summary>
        /// Балансы
        /// </summary>
        public IBalanceRepository<Balance> Balances => _balanceRepository ?? (_balanceRepository = new BalanceRepository(_db));
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        public IBalanceStorageRepository<BalanceStorage> BalanceStorages => _balanceStorageRepository ?? (_balanceStorageRepository = new BalanceStorageRepository(_db));
        /// <summary>
        /// Транзакции
        /// </summary>
        public ITransactionRepository<Transaction> Transactions => _transactionRepository ?? (_transactionRepository = new TransactionRepository(_db));
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"></param>
        public Uow(DbContext context, UserManager<User> userManager, SignInManager<User> signManager)
        {
            _db = context;
            UserManager = userManager;
            SignManager = signManager;
        }
        /// <summary>
        /// Сохранить изменения асинхронно и вернуть количество отработанных записей
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
        /// <summary>
        /// Реализация IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                _db.Dispose();
            _disposed = true;
        }

        
    }
}
