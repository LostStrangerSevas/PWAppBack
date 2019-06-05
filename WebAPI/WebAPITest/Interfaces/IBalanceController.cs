using Microsoft.AspNetCore.Mvc;
using PWApp.ViewModels.Classes;
using System.Threading.Tasks;

namespace WebAPITest.Interfaces
{
    /// <summary>
    /// Интерфейс api-контроллера
    /// </summary>
    interface IBalanceController
    {
        /// <summary>
        /// Метод добавляет новую транзакцию
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> CreateTransaction(TransactionAddViewModel Transaction);
        /// <summary>
        /// Метод возвращает список всех транзакций текущего пользователя
        /// </summary>
        /// <returns>IActionResult</returns>       
        Task<IActionResult> GetTransactionsByUserId();
        /// <summary>
        /// Метод возвращает баланс текущего пользователя
        /// </summary>
        /// <returns>IActionResult</returns>       
        Task<IActionResult> GetBalanceByUserId();
        //*********************************************

        /// <summary>
        /// Метод возвращает список всех балансов
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> GetBalances();
        /// <summary>
        /// Метод возвращает баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> GetBalance(int id);
        /// <summary>
        /// Метод обновляет балансов
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> UpdateBalance(BalanceViewModel<int> balance);
        /// <summary>
        /// Метод добавляет новый баланс
        /// </summary>
        /// <returns>IActionResult</returns>        
        Task<IActionResult> CreateBalance(BalanceViewModel<int> balance);
        /// <summary>
        /// Метод удаляет баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> DeleteBalance(int id);
    }
}
