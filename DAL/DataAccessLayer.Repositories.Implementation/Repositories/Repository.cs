using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Общий для всех сущностей БД репозиторий
    /// </summary>
    /// <typeparam name="T">ссылочный тип</typeparam>
    /// <typeparam name="TKey">тип идентификатора</typeparam>
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// Набор сущностей типа Т (таблица БД)
        /// </summary>
        private readonly DbSet<T> _set;
        public Repository(DbContext context)
        {
            _set = context.Set<T>();
        }
        /// <summary>
        /// Ссылка на DbSet<T>
        /// </summary>
        public DbSet<T> Set { get { return _set; } }
        /// <summary>
        /// Получить все сущности типа Т из набора
        /// </summary>
        /// <returns>IQueryable для типа Т</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }
        /// <summary>
        /// Найти сущность типа Т по идентификатору id
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>сущность типа Т</returns>      
        public async Task<T> GetAsync(TKey id)
        {
            return await _set.FindAsync(id);
        }
        /// <summary>
        /// Добавить к набору сущность типа Т
        /// </summary>
        /// <param name="item">новая сущность</param>
        public async void Create(T item)
        {
            await _set.AddAsync(item);
        }
        /// <summary>
        /// Обновить существующую сущность типа Т
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="item">обновлённая сущность</param>
        public void Update(TKey id, T item)
        {
            _set.Update(item);
        }
        /// <summary>
        /// Удалить сущность по идентификатору id
        /// </summary>
        /// <param name="id">идентификатор</param>
        public void Delete(TKey id)
        {
            var ad = _set.Find(id);
            if (ad != null)
                _set.Remove(ad);
        }
        /// <summary>
        /// Получить набор сущностей типа Т по заданному условию (предикату) 
        /// </summary>
        /// <param name="predicate">предикат выборки</param>
        /// <returns></returns>
        public IQueryable<T> Find(Func<T, bool> predicate)
        {
            return _set.AsNoTracking().Where(predicate).AsQueryable();
        }
        /// <summary>
        /// Проверить наличие сущностей по предикату
        /// </summary>
        /// <param name="predicate">предикат выборки</param>
        /// <returns>bool</returns>
        /// <remarks>
        /// Добавляем AsNoTracking() для отмены кэшированая получаемыз объектов
        /// </remarks>
        public bool IsExist(Func<T, bool> predicate)
        {
            return _set.AsNoTracking().Any(predicate);
        }
    }
}
