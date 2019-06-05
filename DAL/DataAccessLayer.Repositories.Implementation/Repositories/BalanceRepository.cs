using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностями Balance
    /// </summary>
    public class BalanceRepository : Repository<Balance, int>, IBalanceRepository<Balance>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public BalanceRepository(DbContext context) : base(context)
        {
        }
        /// <summary>
        /// Проверка наличия баланса по дате и значению баланса и идентификатору пользователя
        /// </summary>
        /// <param name="item">Экземпляр баланса</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(Balance item)
        {
            return await Task.FromResult(Find(c => c.UserId == item.UserId 
                                                    && c.ModifiedDate == item.ModifiedDate 
                                                    &&  c.Value == item.Value).Any());
        }
        /// <summary>
        /// Проверка наличия баланса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await Task.FromResult(IsExist(c => c.Id == id));
        }
    }
}
