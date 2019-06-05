using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces.IManagers;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementation.Managers
{
    public class UserDtoManager : IUserDtoManager
    {
        /// <summary>
        /// Единый контекст
        /// </summary>
        private readonly IUow _uow;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uow">IUow</param>
        public UserDtoManager(IUow uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>IdentityResult</returns>
        public async Task<(bool, string, IdentityResult)> CreateUserAsync(UserDto userDto)
        {
            if (userDto == null)
                return (false, "Нельзя создать пустого пользователя", null);
            // проверка на наличие email в БД
            var checkEmail = await _uow.UserManager.FindByEmailAsync(userDto.Email);
            if (checkEmail != null)
                return (false, "Указанный email уже зарегистрирован.", null);
            // попытка создания юзера в БД
            var result = await _uow.UserManager.CreateAsync(Mapper.Map<UserDto, User>(userDto), userDto.Password);
            if (result.Errors.Any())
            {
                return (false, result.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + " | ")), null);
            }
            //если удачно создали пользователя
            var user = await _uow.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return (false, $"После удачного создания пользователя не удалось получить его профиль по {userDto.Email}", null);
            //то добавляем ему новый баланс           
            _uow.Balances.Create(new Balance() { UserId = user.Id, ModifiedDate = DateTime.Now, Value = 500 });            
            return (true, string.Empty, result);            
    }
        /// <summary>
        /// Авторизация 
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>SignInResult</returns>
        public async Task<(bool, string, SignInResult, UserDto)> SignInAsync(UserDto userDto)
        {
            if (userDto == null)
                return (false, "Нельзя авторизовать пустого пользователя", null, null);
            var user = await _uow.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return (false, $"Пользователь с электронной почтой {userDto.Email} не найден", null, null);
            var result = await _uow.SignManager.PasswordSignInAsync(user, userDto.Password, false, false);
            return (result.Succeeded)
                ? (result.Succeeded, string.Empty, result, Mapper.Map<User, UserDto>(user))
                : (result.Succeeded, "Не удалось авторизовать пользователя", null, null);
            
        }
        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>UserDto</returns>
        public async Task<(bool, string, UserDto)> GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return (false, "Неизвестный идентификатор пользователя", null);
            var user = await _uow.UserManager.FindByIdAsync(userId);
            if (user == null)
                return (false, $"Пользователь с идентификатором {userId} не найден", null);
            return (true, string.Empty, Mapper.Map<User, UserDto>(user));
        }
        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <returns>UserDto</returns>       
        public async Task<(bool, string, UserDto)> GetUserByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return (false, "Неизвестный логин пользователя", null);
            var user = await _uow.UserManager.FindByNameAsync(userName);
            if (user == null)
                return (false, $"Пользователь с логином {userName} не найден", null);
            return (true, string.Empty, Mapper.Map<User, UserDto>(user));
        }
        /// <summary>
        /// Получение пользователя по электронной почте     
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <returns>UserDto</returns>       
        public async Task<(bool, string, UserDto)> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return (false, "Неизвестная электронная почта пользователя", null);
            var user = await _uow.UserManager.FindByEmailAsync(email);
            if (user == null)
                return (false, $"Пользователь с электронной почтой {email} не найден", null);
            return (true, string.Empty, Mapper.Map<User, UserDto>(user));
        }
        /// <summary>
        /// Выход 
        /// </summary>
        public async Task LogOut()
        {
            await _uow.SignManager.SignOutAsync();
        }
        /// <summary>
        /// Проверка корректности пароля для текущего пользователя
        /// </summary>
        /// <param name="userDto">Пользователь</param>
        /// <param name="password">Пароль</param>
        /// <returns>UserDto</returns>
        public async Task<(bool, string, UserDto)> CheckPasswordAsync(UserDto userDto, string password)
        {
            if (string.IsNullOrEmpty(password))
                return (false, "Неизвестный пароль пользователя", null);
            var boolUser = await _uow.UserManager.CheckPasswordAsync(Mapper.Map<UserDto, User>(userDto), password);
            return (boolUser)
                ? (boolUser, string.Empty, userDto)
                : (false, "Указанный пароль не соответствует указанному email", null);
        }
        /// <summary>
        /// Сохранение изменений БД
        /// </summary>
        public async Task<(bool, string, int)> SaveChangesAsync()
        {
            try
            {
                var result = await _uow.SaveAsync();
                return (true, string.Empty, result);
            }
            catch (Exception e)
            {
                return (false, e.Message, 0);
            }
        }
        /// <summary>
        /// Получение списка зарегистрированных пользователей
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>IEnumerable of UserDto</returns>
        public async Task<(bool, string, IEnumerable<UserDto>)> GetUsers(string userId)
        {
            var users = (await Task.Run(() =>_uow.UserManager.Users)).Where(i => i.Id != userId);
            if (!users.Any())
                return (false, $"Список пользователей пуст", null);
            return (true, string.Empty, Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
        }
    }
}
