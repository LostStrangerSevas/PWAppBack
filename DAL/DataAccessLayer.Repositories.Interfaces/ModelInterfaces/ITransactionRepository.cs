using DataAccessLayer.Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces.ModelInterfaces
{
    /// <summary>
    /// Интерфейс для определения методов, специфичных только для работы с сущностями Transaction
    /// </summary>
    public interface ITransactionRepository<T> : IRepository<T, int> where T : Transaction
    {
        Task<bool> ExistAsync(T item);
        Task<bool> ExistAsync(int id);
        /// <summary>
        /// Получить все транзакции с историей
        /// </summary>
        /// <returns>List of Transaction</returns>
        Task<List<Transaction>> GetFullTransactionsAsync();
    }
}
