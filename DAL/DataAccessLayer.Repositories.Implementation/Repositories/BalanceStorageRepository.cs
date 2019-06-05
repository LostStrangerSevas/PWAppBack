using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностями BalanceStorage
    /// </summary>
    public class BalanceStorageRepository : Repository<BalanceStorage, int>, IBalanceStorageRepository<BalanceStorage>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public BalanceStorageRepository(DbContext context) : base(context)
        {
        }
        /// <summary>
        /// Проверка наличия истории баланса транзакции
        /// по идентификаторам транзакции, пользователя, участника, признака времени и значению транзакции
        /// </summary>
        /// <param name="item">Экземпляр истории баланса транзакции</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(BalanceStorage item)
        {
            return await Task.FromResult(Find(c => c.TransactionId == item.TransactionId
                                                    && c.UserId == item.UserId 
                                                    && c.Value == item.Value).Any());
        }
        /// <summary>
        /// Проверка наличия истории баланса транзакции по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await Task.FromResult(IsExist(c => c.Id == id));
        }
    }
}
