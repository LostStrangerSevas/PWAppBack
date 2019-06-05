using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Interfaces.IManagers;
using PWApp.ViewModels.Classes;
using WebAPITest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WebAPITest.Controllers
{
#if DEBUG
#else
#endif
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("FreePolicy")]
    [Authorize]
    public class BalancesController : ControllerBase, IBalanceController
    {
        #region props 
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<BalancesController> _logger;
        /// <summary>
        /// Экземпляр прокси-контроллера
        /// </summary>
        private readonly IBalanceControllerProxy _BalancesControllerProxy;
        #endregion

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="BalanceService">Менеджер балансов</param>
        /// <param name="ilogger">Логгер</param>
        public BalancesController(IBalanceDtoManager balanceService,
                                    IBalanceStorageDtoManager balanceStorageService,
                                    ITransactionDtoManager transactionService,
                                    ILogger<BalancesController> ilogger)
        {
            _logger = ilogger;
            _BalancesControllerProxy = BalancesControllerProxy.Create(balanceService, 
                                                                        balanceStorageService,
                                                                        transactionService,
                                                                        _logger);
        }
        #endregion

        #region api-methods
        /// <summary>
        /// Метод добавляет новую транзакцию
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/Balances/CreateTransaction
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionAddViewModel transaction)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            return await _BalancesControllerProxy.CreateTransaction(transaction, userId);
        }
        /// <summary>
        /// Метод возвращает список всех транзакций текущего пользователя
        /// </summary>
        /// <returns>IActionResult</returns>       
        /// <remarks>
        /// GET: api/Balances/GetTransactionsByUserId
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionsByUserId()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            return await _BalancesControllerProxy.GetTransactionsByUserId(userId);           
        }
        /// <summary>
        /// Метод возвращает баланс текущего пользователя
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET: api/Balances/GetBalanceByUserId
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBalanceByUserId()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            return await _BalancesControllerProxy.GetBalanceByUserId(userId);            
        }
        #endregion


        #region standart methods
        /// <summary>
        /// Метод возвращает список всех балансов
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET: api/Balances/GetBalances
        /// </remarks>
        [HttpGet]
        [ActionName("GetBalances")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BalanceViewModel<int>>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBalances()
        {
            return await _BalancesControllerProxy.GetBalances();
        }

        /// <summary>
        /// Метод возвращает баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET: api/Balances/GetBalance/id
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BalanceViewModel<int>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBalance([FromRoute] int id)
        {
            return await _BalancesControllerProxy.GetBalance(id);           
        }

        /// <summary>
        /// Метод обновляет баланс полностью
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/Balances/UpdateBalance
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBalance([FromBody] BalanceViewModel<int> Balance)
        {
            return await _BalancesControllerProxy.UpdateBalance(Balance);
        }

        /// <summary>
        /// Метод добавляет новый баланс
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/Balances/CreateBalance
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBalance([FromBody] BalanceViewModel<int> Balance)
        {
            return await _BalancesControllerProxy.CreateBalance(Balance);            
        }

        /// <summary>
        /// Метод удаляет баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/Balances/DeleteBalance/id
        /// </remarks>
        [HttpPost("{id}")]
        [ProducesResponseType(200, Type = typeof(IActionResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBalance([FromRoute] int id)
        {
            return await _BalancesControllerProxy.DeleteBalance(id);          
        }
        #endregion
    }
}
