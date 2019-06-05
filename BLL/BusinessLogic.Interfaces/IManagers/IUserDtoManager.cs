using BusinessLogic.Models.ClassesDto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.IManagers
{
    /// <summary>
    /// Интерфейс менеджера пользователей
    /// </summary>
    public interface IUserDtoManager
    {
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>IdentityResult</returns>
        Task<(bool, string, IdentityResult)> CreateUserAsync(UserDto user);
        /// <summary>
        /// Авторизация 
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>SignInResult</returns>
        Task<(bool, string, SignInResult, UserDto)> SignInAsync(UserDto user);
        /// <summary>
        /// Выход 
        /// </summary>
        Task LogOut();
        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>UserDto</returns>
        Task<(bool, string, UserDto)> GetUserById(string userId);
        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <returns>UserDto</returns>       
        Task<(bool, string, UserDto)> GetUserByNameAsync(string userName);
        /// <summary>
        /// Получение пользователя по электронной почте     
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <returns>UserDto</returns>       
        Task<(bool, string, UserDto)> GetUserByEmailAsync(string email);
        /// <summary>
        /// Проверка корректности пароля для текущего пользователя
        /// </summary>
        /// <param name="userDto">Пользователь</param>
        /// <param name="password">Пароль</param>
        /// <returns>UserDto</returns>
        Task<(bool, string, UserDto)> CheckPasswordAsync(UserDto userDto, string password);
        /// <summary>
        /// Сохранение изменений БД
        /// </summary>
        Task<(bool, string, int)> SaveChangesAsync();
        /// <summary>
        /// Получение списка зарегистрированных пользователей
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IEnumerable of UserDto</returns>
        Task<(bool, string, IEnumerable<UserDto>)> GetUsers(string userId);
    }
}
