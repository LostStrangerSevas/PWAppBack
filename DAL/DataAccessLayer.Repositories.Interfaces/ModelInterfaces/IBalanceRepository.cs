using DataAccessLayer.Models.Classes;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces.ModelInterfaces
{
    /// <summary>
    /// Интерфейс для определения методов, специфичных только для работы с сущностями Balance
    /// </summary>
    public interface IBalanceRepository<T> : IRepository<T, int> where T : Balance
    {
        Task<bool> ExistAsync(T item);
        Task<bool> ExistAsync(int id);
    }
}
