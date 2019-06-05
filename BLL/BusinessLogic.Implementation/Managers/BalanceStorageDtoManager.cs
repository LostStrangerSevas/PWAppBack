using System;
using System.Collections.Generic;
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
    public class BalanceStorageDtoManager : IBalanceStorageDtoManager
    {
        /// <summary>
        /// Единый контекст
        /// </summary>
        private readonly IUow _instanceDb;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uow">IUow</param>
        public BalanceStorageDtoManager(IUow uow)
        {
            _instanceDb = uow;
        }
        /// <summary>
        /// Добавление истории балансов транзакций в БД
        /// </summary>
        /// <param name="item">История балансов транзакций</param>
        public async Task<(bool, string)> AddAsync(BalanceStorageDto item)
        {
            try
            {
                if (await ExistAsync(item))
                    return (false, "Попытка добавить в БД уже существующую историю балансов транзакций.");
                _instanceDb.BalanceStorages.Create(Mapper.Map<BalanceStorageDto, BalanceStorage>(item));
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Получить все истории балансов транзакций
        /// </summary>
        /// <returns>IEnumerable of BalanceStorageDto</returns>
        public async Task<(bool, string, IEnumerable<BalanceStorageDto>)> GetAllAsync()
        {
            try
            {
                var BalanceStorages = Mapper.Map<List<BalanceStorage>, IEnumerable<BalanceStorageDto>>(await _instanceDb.BalanceStorages.GetAllAsync());
                return (true, string.Empty, BalanceStorages);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Получить историю балансов транзакций по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>BalanceStorageDto</returns>
        public async Task<(bool, string, BalanceStorageDto)> GetAsync(int id)
        {
            try
            {
                var BalanceStorage = Mapper.Map<BalanceStorage, BalanceStorageDto>(await _instanceDb.BalanceStorages.GetAsync(id));
                return (true, string.Empty, BalanceStorage);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        /// <summary>
        /// Обновление истории балансов транзакций
        /// </summary>
        /// <param name="item">BalanceStorageDto</param>
        public async Task<(bool, string)> UpdateAsync(BalanceStorageDto item)
        {
            try
            {
                if (!await ExistAsync(item.Id))
                    return (false, "Попытка отредактировать в БД несуществующую историю балансов транзакций");
                _instanceDb.BalanceStorages.Update(item.Id, Mapper.Map<BalanceStorageDto, BalanceStorage>(item));                
                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        /// <summary>
        /// Удаление истории балансов транзакций
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public async Task<(bool, string)> RemoveAsync(int id)
        {
            try
            {
                if (!await ExistAsync(id))
                    return (false, "Попытка удалить из БД несуществующую историю балансов транзакций");
                _instanceDb.BalanceStorages.Delete(id);
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
        /// Проверка существования истории балансов транзакций по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await _instanceDb.BalanceStorages.ExistAsync(id);
        }        
        /// <summary>
        /// Проверка наличия истории балансов транзакций по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр истории балансов транзакций</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(BalanceStorageDto item)
        {
            return await _instanceDb.BalanceStorages.ExistAsync(Mapper.Map<BalanceStorageDto, BalanceStorage>(item));
        }
    }
}
