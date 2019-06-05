using BusinessLogic.Models.ClassesDto;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.IManagers
{
    /// <summary>
    /// Интерфейс менеджера балансов
    /// </summary>
    public interface IBalanceDtoManager : IManager<BalanceDto, int>
    {
        /// <summary>
        /// Проверка наличия баланса по экземпляру
        /// </summary>
        /// <param name="item">Экземпляр баланса</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(BalanceDto item);
        /// <summary>
        /// Проверка существования баланса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>bool</returns>
        Task<bool> ExistAsync(int id);
        /// <summary>
        /// Получить баланс по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>BalanceDto</returns>
        Task<(bool, string, BalanceDto)> GetByUserIdAsync(string userId);
    }
}
