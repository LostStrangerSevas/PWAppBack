using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для всех DtoManager, который они должны реализовывать по умолчанию
    /// </summary>
    /// <typeparam name="T">Cсылочный тип</typeparam>
    /// <typeparam name="TKey">Tип идентификатора</typeparam>
    public interface IManager<T, in TKey> where T : class
    {
        Task<(bool, string, IEnumerable<T>)> GetAllAsync();
        Task<(bool, string, T)> GetAsync(TKey id);

        Task<(bool, string)> AddAsync(T item);
        Task<(bool, string)> UpdateAsync(T item);
        Task<(bool, string)> RemoveAsync(TKey id);
        Task<(bool, string, int)> SaveChangesAsync();
    }
}
