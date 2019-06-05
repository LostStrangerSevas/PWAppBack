using DataAccessLayer.Models.Classes;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces.ModelInterfaces
{
    /// <summary>
    /// Интерфейс для определения методов, специфичных только для работы с сущностями BalanceStorage
    /// </summary>
    public interface IBalanceStorageRepository<T> : IRepository<T, int> where T : BalanceStorage
    {
        Task<bool> ExistAsync(T item);
        Task<bool> ExistAsync(int id);
    }
}
