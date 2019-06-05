using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория с основынми общими для всех репозиториев методами
    /// </summary>
    /// <typeparam name="T">Ссылочный тип (класс)</typeparam>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    public interface IRepository<T, in TKey> : IRepositoryBase<T, TKey> where T : class
    {
        IQueryable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(TKey id, T item);
        void Delete(TKey id);
        bool IsExist(Func<T, bool> predicate);
    }
}
