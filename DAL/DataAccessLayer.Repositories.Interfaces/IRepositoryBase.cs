using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория с базовыми методами на получение данных
    /// </summary>
    /// <typeparam name="T">Ссылочный тип (класс)</typeparam>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    public interface IRepositoryBase<T, in TKey> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(TKey id);
    }
}
