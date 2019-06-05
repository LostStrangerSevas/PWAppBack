using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces.IManagers;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogic.Implementation.Managers
{
    /// <summary>
    /// Реализация менеджера балансов
    /// </summary>
    public class BalanceDtoManager : IBalanceDtoManager
    {
        /// <summary>
        /// Единый контекст
        /// </summary>
        private readonly IUow _instanceDb;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uow">IUow</param>
        public BalanceDtoManager(IUow uow)
        {
            _instanceDb = uow;
        }
        /// <summary>
        /// Добавление баланса в БД
        /// </summary>
        /// <param name="item">Баланс</param>
        public async Task<(bool, string)> AddAsync(BalanceDto item)
        {
            try
            {
                if (await ExistAsync(item))
                    return (false, "Попытка добавить в БД уже существующий баланс.");
                _instanceDb.Balances.Create(Mapper.Map<BalanceDto, Balance>(item));
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Получить все балансы
        /// </summary>
        /// <returns>IEnumerable of BalanceDto</returns>
        public async Task<(bool, string, IEnumerable<BalanceDto>)> GetAllAsync()
        {
            try
            {
                var balances = Mapper.Map<List<Balance>, IEnumerable<BalanceDto>>(await _instanceDb.Balances.GetAllAsync());
                return (true, string.Empty, balances);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Получить баланс по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>BalanceDto</returns>
        public async Task<(bool, string, BalanceDto)> GetAsync(int id)
        {
            try
            {           
                var balance = await _instanceDb.Balances.GetAsync(id);
                if (balance == null)
                    return (false, "По указанному идентификатору баланс не найден", null);
                return (true, string.Empty, Mapper.Map<Balance, BalanceDto>(balance));
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Обновление баланса
        /// </summary>
        /// <param name="item">BalanceDto</param>
        public async Task<(bool, string)> UpdateAsync(BalanceDto item)
        {
            try
            {
                if (!await ExistAsync(item.Id))
                    return (false, "Попытка отредактировать в БД несуществующий баланс");
                _instanceDb.Balances.Update(item.Id, Mapper.Map<BalanceDto, Balance>(item));                
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Удаление баланса
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public async Task<(bool, string)> RemoveAsync(int id)
        {
            try
            {
                if (!await ExistAsync(id))
                    return (false, "Попытка удалить из БД несуществующий баланс");
                _instanceDb.Balances.Delete(id);
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
        /// Проверка существования баланса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await _instanceDb.Balances.ExistAsync(id);
        }        
        /// <summary>
        /// Проверка наличия баланса по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр баланса</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(BalanceDto item)
        {
            return await _instanceDb.Balances.ExistAsync(Mapper.Map<BalanceDto, Balance>(item));
        }
        /// <summary>
        /// Получить баланс по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>BalanceDto</returns>
        public async Task<(bool, string, BalanceDto)> GetByUserIdAsync(string userId)
        {
            try
            {
                var balance = await Task.Run(() =>_instanceDb.Balances.Find(i => i.UserId == userId).FirstOrDefault());
                if (balance == null)
                    return (false, "Для указанного пользователя баланс не найден", null);
                return (true, string.Empty, Mapper.Map<Balance, BalanceDto>(balance));
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
    }
}
