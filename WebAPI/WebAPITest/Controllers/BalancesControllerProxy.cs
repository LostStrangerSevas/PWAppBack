using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPITest.Interfaces;
using BusinessLogic.Interfaces.IManagers;
using WebAPITest.Classes;
using BusinessLogic.Models.ClassesDto;
using PWApp.ViewModels.Classes;
using AutoMapper;
using WebAPI.Classes;
using System.Linq;

namespace WebAPITest.Controllers
{
    /// <summary>
    /// Реализация прокси-контроллера
    /// </summary>
    public class BalancesControllerProxy : IBalanceControllerProxy
    {
        #region props
        /// <summary>
        /// Менеджер балансов
        /// </summary>
        private readonly IBalanceDtoManager _balanceService;
        /// <summary>
        /// Менеджер истории балансов транзакций
        /// </summary>
        private readonly IBalanceStorageDtoManager _balanceStorageService;
        /// <summary>
        /// Менеджер транзакций
        /// </summary>
        private readonly ITransactionDtoManager _transactionService;
        /// <summary>
        /// Используемый лог
        /// </summary>
        private readonly ILogger _iLogger;
        /// <summary>
        /// Создаёт экземпляр BalancesControllerProxy
        /// </summary>
        /// <param name="iLogger">Логгер</param>
        /// <returns></returns>
        public static IBalanceControllerProxy Create(IBalanceDtoManager balanceService,
                                        IBalanceStorageDtoManager balanceStorageService,
                                        ITransactionDtoManager transactionService,
                                        ILogger iLogger)
        {
            return new BalancesControllerProxy(balanceService,
                                                balanceStorageService,
                                                transactionService,
                                                iLogger);
        }
        #endregion

        #region ctor
        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="iLogger">Логгер</param>        
        private BalancesControllerProxy(IBalanceDtoManager balanceService,
                                        IBalanceStorageDtoManager balanceStorageService,
                                        ITransactionDtoManager transactionService,
                                        ILogger iLogger)
        {
            _balanceService = balanceService;
            _balanceStorageService = balanceStorageService;
            _transactionService = transactionService;
            _iLogger = iLogger;
        }
        #endregion

        #region methods
        /// <summary>
        /// Метод добавляет новую транзакцию
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        /// <param name="userId">Идентификатор отправителя</param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> CreateTransaction(TransactionAddViewModel transaction, string userId)
        {
            try
            {
                if (!transaction.ValidateModel(out string error))
                    return Helper.SetObjectResultStatus(error, 400);               
                var rezultSave = await _transactionService.CreateTransaction(
                    new TransactionAddDto()
                    {
                        RecepientId = transaction.RecepientId,
                        Value = transaction.Value,
                        UserId = userId
                    }, 
                    _balanceService);
                return rezultSave.Item1
                    ? Helper.SetObjectResultStatus(rezultSave.Item3.ToString())
                    : Helper.SetObjectResultStatus(rezultSave.Item2, rezultSave.Item3);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод возвращает список всех транзакций текущего пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IActionResult</returns>       
        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            try
            {
                var rezult = await _transactionService.GetAllByUserId(userId); 
                return rezult.Item1
                    ? (IActionResult)new JsonResult(
                        Mapper.Map<IEnumerable<TransactionCommonDto>, IEnumerable<TransactionCommonViewModel<int>>>(rezult.Item3), 
                        JsonSupport.JsonSerializerSettings)
                    : Helper.SetObjectResultStatus(rezult.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод возвращает баланс текущего пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IActionResult</returns>       
        public async Task<IActionResult> GetBalanceByUserId(string userId)
        {
            try
            {
                var rezult = await _balanceService.GetByUserIdAsync(userId);
                return rezult.Item1
                    ? (IActionResult)new JsonResult(Mapper.Map<BalanceDto, BalanceViewModel<int>>(rezult.Item3), JsonSupport.JsonSerializerSettings)
                    : Helper.SetObjectResultStatus(rezult.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        //****************************************************


        /// <summary>
        /// Метод возвращает список всех балансов
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> GetBalances()
        {
            try
            {
                var rezult = await _balanceService.GetAllAsync();
                return rezult.Item1
                    ? (IActionResult)new JsonResult(Mapper.Map<IEnumerable<BalanceDto>, IEnumerable<BalanceViewModel<int>>>(rezult.Item3), JsonSupport.JsonSerializerSettings)
                    : Helper.SetObjectResultStatus(rezult.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод возвращает баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>        
        public async Task<IActionResult> GetBalance(int id)
        {
            try
            {
                var rezult = await _balanceService.GetAsync(id);
                return rezult.Item1
                    ? (IActionResult)new JsonResult(Mapper.Map<BalanceDto, BalanceViewModel<int>>(rezult.Item3), JsonSupport.JsonSerializerSettings)
                    : Helper.SetObjectResultStatus(rezult.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод обновляет баланс
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> UpdateBalance(BalanceViewModel<int> Balance)
        {
            try
            {
                if (!Balance.ValidateModel(out string error))
                    return Helper.SetObjectResultStatus(error, 400);
                var rezult = await _balanceService.UpdateAsync(Mapper.Map<BalanceViewModel<int>, BalanceDto>(Balance));
                if (!rezult.Item1)
                    return Helper.SetObjectResultStatus(rezult.Item2, 500);
                var rezultSave = await _balanceService.SaveChangesAsync();
                return rezultSave.Item1
                    ? Helper.SetObjectResultStatus(rezultSave.Item3.ToString())
                    : Helper.SetObjectResultStatus(rezultSave.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод добавляет новый баланс
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> CreateBalance(BalanceViewModel<int> Balance)
        {
            try
            {
                if (!Balance.ValidateModel(out string error))
                    return Helper.SetObjectResultStatus(error, 400);
                var rezult = await _balanceService.AddAsync(Mapper.Map<BalanceViewModel<int>, BalanceDto>(Balance));
                if (!rezult.Item1)
                    return Helper.SetObjectResultStatus(rezult.Item2, 500);
                var rezultSave = await _balanceService.SaveChangesAsync();
                return rezultSave.Item1
                    ? Helper.SetObjectResultStatus(rezultSave.Item3.ToString())
                    : Helper.SetObjectResultStatus(rezultSave.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        /// <summary>
        /// Метод удаляет баланс по идентификатору
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> DeleteBalance([FromRoute] int id)
        {
            try
            {
                var rezult = await _balanceService.RemoveAsync(id);
                if (!rezult.Item1)
                    return Helper.SetObjectResultStatus(rezult.Item2, 500);
                var rezultSave = await _balanceService.SaveChangesAsync();
                return rezultSave.Item1
                    ? Helper.SetObjectResultStatus(rezultSave.Item3.ToString())
                    : Helper.SetObjectResultStatus(rezultSave.Item2, 500);
            }
            catch (Exception e)
            {
                _iLogger.LogError(e.ToString());
                return Helper.SetObjectResultStatus(e.ToString(), 500);
            }
        }
        #endregion
    }
}
