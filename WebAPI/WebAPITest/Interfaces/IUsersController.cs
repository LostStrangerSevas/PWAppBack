using Microsoft.AspNetCore.Mvc;
using PWApp.ViewModels.Classes;
using System.Threading.Tasks;

namespace WebAPITest.Interfaces
{
    /// <summary>
    /// Интерфейс api-контроллера авторизации
    /// </summary>
    interface IUsersController
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Данные пользователя для авторизации</param>
        /// <returns>IActionResult</returns>
        Task<IActionResult> SignIn(SignInViewModel model);
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Данные пользователя для регистрации</param>
        /// <returns>IActionResult</returns>
        Task<IActionResult> RegisterUser(RegistrationViewModel model);
        /// <summary>
        /// Деавторизация
        /// </summary>
        /// <returns>IActionResult</returns>
        Task<IActionResult> LogOut();
    }
}
