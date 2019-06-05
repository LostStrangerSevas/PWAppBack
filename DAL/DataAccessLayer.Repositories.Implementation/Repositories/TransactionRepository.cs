using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностями Transaction
    /// </summary>
    public class TransactionRepository : Repository<Transaction, int>, ITransactionRepository<Transaction>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public TransactionRepository(DbContext context) : base(context)
        {
        }
        /// <summary>
        /// Проверка наличия транзакции по дате и значению
        /// </summary>
        /// <param name="item">Экземпляр транзакции</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(Transaction item)
        {
            return await Task.FromResult(Find(c => c.ExecutionDate == item.ExecutionDate
                                                    && c.Value == item.Value).Any());
        }
        /// <summary>
        /// Проверка наличия транзакции по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await Task.FromResult(IsExist(c => c.Id == id));
        }
        /// <summary>
        /// Получить все транзакции с историей
        /// </summary>
        /// <returns>List of Transaction</returns>
        public async Task<List<Transaction>> GetFullTransactionsAsync()
        {
            return await Set.Include(t => t.BalanceStorages).ToListAsync();
        }
    }
}
