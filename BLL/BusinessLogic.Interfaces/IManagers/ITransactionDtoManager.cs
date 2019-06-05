using BusinessLogic.Models.ClassesDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.IManagers
{
    /// <summary>
    /// Интерфейс менеджера транзакций
    /// </summary>
    public interface ITransactionDtoManager : IManager<TransactionDto, int>
    {
        /// <summary>
        /// Проверка наличия транзакций по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр баланса</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(TransactionDto item);
        /// <summary>
        /// Проверка существования транзакций по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(int id);
        /// <summary>
        /// Получение всех транзакций по идентификатору пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IEnumerable of TransactionDto</returns>
        Task<(bool, string, IEnumerable<TransactionCommonDto>)> GetAllByUserId(string userId);
        /// <summary>
        /// Создание новой транзакции
        /// </summary>
        /// <param name="transaction">Данные транзакции</param>
        /// <param name="balanceService">Менеджер балансов</param>
        /// <returns>int</returns>
        Task<(bool, string, int)> CreateTransaction(TransactionAddDto transaction, IBalanceDtoManager balanceService);
    }
}
