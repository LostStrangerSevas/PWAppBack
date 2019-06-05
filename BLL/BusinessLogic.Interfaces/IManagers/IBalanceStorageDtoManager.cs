using BusinessLogic.Models.ClassesDto;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.IManagers
{
    /// <summary>
    /// Интерфейс менеджера истории балансов транзакций
    /// </summary>
    public interface IBalanceStorageDtoManager : IManager<BalanceStorageDto, int>
    {
        /// <summary>
        /// Проверка наличия истории балансов транзакций по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр истории балансов транзакций</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(BalanceStorageDto item);
        /// <summary>
        /// Проверка существования истории балансов транзакций по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(int id);        
    }
}
