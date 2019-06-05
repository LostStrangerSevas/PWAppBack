using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces.IManagers;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Implementation.Managers
{
    /// <summary>
    /// Реализация менеджера транзакциюов
    /// </summary>
    public class TransactionDtoManager : ITransactionDtoManager
    {
        /// <summary>
        /// Единый контекст
        /// </summary>
        private readonly IUow _instanceDb;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uow">IUow</param>
        public TransactionDtoManager(IUow uow)
        {
            _instanceDb = uow;
        }
        /// <summary>
        /// Добавление транзакции в БД
        /// </summary>
        /// <param name="item">Транзакция</param>
        public async Task<(bool, string)> AddAsync(TransactionDto item)
        {
            try
            {                
                if (await ExistAsync(item))
                    return (false, "Попытка добавить в БД уже существующую транзакцию.");
                _instanceDb.Transactions.Create(Mapper.Map<TransactionDto, Transaction>(item));
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Получить все транзакции
        /// </summary>
        /// <returns>IEnumerable of TransactionDto</returns>
        public async Task<(bool, string, IEnumerable<TransactionDto>)> GetAllAsync()
        {
            try
            {
                var Transactions = Mapper.Map<List<Transaction>, IEnumerable<TransactionDto>>(await _instanceDb.Transactions.GetAllAsync());
                return (true, string.Empty, Transactions);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Получить транзакцию по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>TransactionDto</returns>
        public async Task<(bool, string, TransactionDto)> GetAsync(int id)
        {
            try
            {
                var Transaction = Mapper.Map<Transaction, TransactionDto>(await _instanceDb.Transactions.GetAsync(id));
                return (true, string.Empty, Transaction);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Обновление транзакции
        /// </summary>
        /// <param name="item">TransactionDto</param>
        public async Task<(bool, string)> UpdateAsync(TransactionDto item)
        {
            try
            {
                if (!await ExistAsync(item.Id))
                    return (false, "Попытка отредактировать в БД несуществующую транзакцию");
                _instanceDb.Transactions.Update(item.Id, Mapper.Map<TransactionDto, Transaction>(item));                
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Удаление транзакции
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public async Task<(bool, string)> RemoveAsync(int id)
        {
            try
            {
                if (!await ExistAsync(id))
                    return (false, "Попытка удалить из БД несуществующую транзакцию");
                _instanceDb.Transactions.Delete(id);
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Сохранение изменений БД
        /// </summary>
        public async Task<(bool, string, int)> SaveChangesAsync()
        {
            try
            {
                var result = await _instanceDb.SaveAsync();
                return (true, string.Empty, result);
            }
            catch (Exception e)
            {
                return (false, e.Message, 0);
            }
        }
        /// <summary>
        /// Проверка существования транзакции по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await _instanceDb.Transactions.ExistAsync(id);
        }        
        /// <summary>
        /// Проверка наличия транзакции по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр транзакции</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(TransactionDto item)
        {
            return await _instanceDb.Transactions.ExistAsync(Mapper.Map<TransactionDto, Transaction>(item));
        }
        /// <summary>
        /// Получение всех транзакций по идентификатору пользователя
        /// Первый список -  пользователь-отправитель
        /// Второй список -  пользователь-получатель
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IEnumerable of TransactionDto</returns>
        public async Task<(bool, string, IEnumerable<TransactionCommonDto>)> GetAllByUserId(string userId)
        {
            try
            {
                //1. Получить все идентификаторы транзакций для пользователя
                var transactionsIds = await Task.Run(() => _instanceDb.BalanceStorages.Find(i => i.UserId == userId).Select(i => i.TransactionId).Distinct());
                //1. Получить все транзакции пользователя     
                var transactions = _instanceDb.Transactions.GetFullTransactionsAsync().Result.Where(i => transactionsIds.Any(x => x == i.Id));
                //var transactions = await Task.Run(() => _instanceDb.Transactions.Find(i => transactionsIds.Contains(i.Id)).Include(t => t.BalanceStorages));//.OrderByDescending(i => i.ExecutionDate));

                var cnt3 = transactions.Count();
                var list3 = transactions.ToList();

                //2. Сформировать список транзакций общего вида
                var transCommon = new List<TransactionCommonDto>();
                foreach(var t in transactions)
                {
                    var userFrom = Mapper.Map<User, UserDto>(await _instanceDb.UserManager
                        .FindByIdAsync(t.BalanceStorages.FirstOrDefault(i => i.IsSender).UserId));
                    var userTo = Mapper.Map<User, UserDto>(await _instanceDb.UserManager
                        .FindByIdAsync(t.BalanceStorages.FirstOrDefault(i => !i.IsSender).UserId));
                    transCommon.Add(new TransactionCommonDto()
                    {
                        ExecutionDate = t.ExecutionDate.ToString(),
                        Value = t.Value,
                        Sender = userFrom.FullName,
                        Recipient = userTo.FullName,
                        RecipientId = userTo.Id
                    });
                }
                return (true, string.Empty, transCommon);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }       
        /// <summary>
        /// Создание новой транзакции
        /// </summary>
        /// <param name="transaction">Данные транзакции</param>
        /// <param name="balanceService">Менеджер балансов</param>
        /// <returns>int</returns>
        public async Task<(bool, string, int)> CreateTransaction(TransactionAddDto transaction, IBalanceDtoManager balanceService)
        {
            try
            {
                //получить баланс отправителя
                var balanceSender = await balanceService.GetByUserIdAsync(transaction.UserId);
                if (!balanceSender.Item1)
                    return (false, balanceSender.Item2, 400);
                //проверить соотношение баланса отправителя и размер транзакции
                if (balanceSender.Item3.Value < transaction.Value)
                    return (false, "Баланс отправителя меньше размера транзакции", 400);
                if (transaction.Value <= 0)
                    return (false, "Размер транзакции невозможен для исполнения (ноль или отрицательный)", 400);               
                //получить баланс получателя
                var balanceRecepient = await balanceService.GetByUserIdAsync(transaction.RecepientId);
                if (!balanceRecepient.Item1)
                    return (false, balanceRecepient.Item2, 500);
                //сформировать новую транзакцию на основе входящих данных
                var newT = new TransactionDto()
                {
                    ExecutionDate = DateTime.Now,
                    Value = transaction.Value,
                    BalanceStorages = new List<BalanceStorageDto>()
                };
                //сформировать список истории балансов для новой транзакции
                //1. Баланс отправителя до транзакции
                newT.BalanceStorages.Add(new BalanceStorageDto()
                {
                    UserId = transaction.UserId,
                    IsSender = true,
                    IsBefore = true,
                    Value = balanceSender.Item3.Value
                });
                //2. Баланс отправителя после транзакции
                newT.BalanceStorages.Add(new BalanceStorageDto()
                {
                    UserId = transaction.UserId,
                    IsSender = true,
                    IsBefore = false,
                    Value = balanceSender.Item3.Value - transaction.Value
                });
                //3. Баланс получателя до транзакции
                newT.BalanceStorages.Add(new BalanceStorageDto()
                {
                    UserId = transaction.RecepientId,
                    IsSender = false,
                    IsBefore = true,
                    Value = balanceRecepient.Item3.Value
                });
                //4. Баланс получателя после транзакции
                newT.BalanceStorages.Add(new BalanceStorageDto()
                {
                    UserId = transaction.RecepientId,
                    IsSender = false,
                    IsBefore = false,
                    Value = balanceRecepient.Item3.Value + transaction.Value
                });
                // добавить транзакцию
                var rezult = await AddAsync(newT);
                if (!rezult.Item1)
                    return (false, rezult.Item2, 500);
                // установить текущий баланс отправителю
                balanceSender.Item3.Value = balanceSender.Item3.Value - transaction.Value;
                var updatedEntity = Mapper.Map<BalanceDto>(balanceSender.Item3);
                var rezultUpdateSender = await balanceService.UpdateAsync(updatedEntity);
                if (!rezultUpdateSender.Item1)
                    return (false, rezultUpdateSender.Item2, 500);
                // установить текущий баланс получателю
                balanceRecepient.Item3.Value = balanceRecepient.Item3.Value + transaction.Value;
                var rezultUpdateRecepient = await balanceService.UpdateAsync(balanceRecepient.Item3);
                if (!rezultUpdateRecepient.Item1)
                    return (false, rezultUpdateRecepient.Item2, 500);
                // сохранить изменения
                var rezultSave = await SaveChangesAsync();
                return (true, string.Empty, 200);
            }
            catch (Exception e)
            {
                return (false, e.Message, 500);
            }
        }
    }
}
