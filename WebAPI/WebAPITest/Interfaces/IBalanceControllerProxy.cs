using Microsoft.AspNetCore.Mvc;
using PWApp.ViewModels.Classes;
using System.Threading.Tasks;

namespace WebAPITest.Interfaces
{
    /// <summary>
    /// Интерфейс прокси-контроллера
    /// </summary>
    public interface IBalanceControllerProxy
    {
        /// <summary>
        /// Метод добавляет новую транзакцию
        /// </summary>
        /// <param name="transaction">Экземпляр транзакции</param>
        /// <param name="userId">Идентификатор отправителя</param>
        /// <returns>IActionResult</returns>
        Task<IActionResult> CreateTransaction(TransactionAddViewModel transaction, string userId);
        /// <summary>
        /// Метод возвращает список всех транзакций текущего пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IActionResult</returns>       
        Task<IActionResult> GetTransactionsByUserId(string userId);
        /// <summary>
        /// Метод возвращает баланс текущего пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IActionResult</returns>       
        Task<IActionResult> GetBalanceByUserId(string userId);
        //*********************************************

        /// <summary>
        /// Метод возвращает список всех заявок
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> GetBalances();
        /// <summary>
        /// Метод возвращает заявку по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> GetBalance(int id);
        /// <summary>
        /// Метод обновляет заявку
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> UpdateBalance(BalanceViewModel<int> Balance);
        /// <summary>
        /// Метод добавляет новую заявку
        /// </summary>
        /// <returns>IActionResult</returns>        
        Task<IActionResult> CreateBalance(BalanceViewModel<int> Balance);
        /// <summary>
        /// Метод удаляет заявку по её идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> DeleteBalance(int id);
    }
}
