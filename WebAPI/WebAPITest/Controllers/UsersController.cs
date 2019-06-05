using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Interfaces.IManagers;
using BusinessLogic.Models.ClassesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PWApp.ViewModels.Classes;
using WebAPI.Classes;
using WebAPITest.Interfaces;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using WebAPITest.Classes;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("FreePolicy")]
    public class UsersController : ControllerBase, IUsersController
    {
        #region props
        /// <summary>
        /// Менеджер пользователей
        /// </summary>
        private readonly IUserDtoManager _userService;
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<UsersController> _logger;
        /// <summary>
        /// Настройки приложения из appsettings.json
        /// </summary>
        private readonly ApplicationSettings _appSettings;
        #endregion

        #region ctors
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userService">Менеджер пользователей</param>
        /// <param name="ilogger">Логгер</param>
        public UsersController(IUserDtoManager userService,
                                ILogger<UsersController> ilogger,
                                IOptions<ApplicationSettings> appSettings)
        {
            _userService = userService;
            _logger = ilogger;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region api-methods
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Данные пользователя для авторизации</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/users/SignIn
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            try
            {
                // проверка модели представления
                if (!ModelState.IsValid)
                    return BadRequest(ConstructErrors(ModelState.Keys.SelectMany(key => this.ModelState[key].Errors)));
                /*
                // попытка получения пользователя по электронной почте
                var userResult = await _userService.GetUserByEmailAsync(model.Email);
                if (!userResult.Item1)
                    return BadRequest(userResult.Item2);
                var userDto = userResult.Item3;          
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", userDto.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });*/


                var userDto = Mapper.Map<SignInViewModel, UserDto>(model);
                var resultSignIn = await _userService.SignInAsync(userDto);
                if (!resultSignIn.Item1)
                    return BadRequest(resultSignIn.Item2);
                userDto = resultSignIn.Item4;
                // формирование токена для ответа
                var token = GetToken(userDto);
                Response.ContentType = "application/json";
                return Ok(new { token, userDto.FullName, userDto.Id });
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Данные пользователя для регистрации</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// POST: api/users/RegisterUser
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationViewModel model)
        {
            try
            {
                // проверка модели представления
                if (!ModelState.IsValid)
                    return BadRequest(ConstructErrors(ModelState.Keys.SelectMany(key => this.ModelState[key].Errors)));
                // сверка паролей
                if (!model.Password.Equals(model.PasswordConfirm))
                    return BadRequest("Пароли не совпадают");
                // создание пользователя
                var result = await _userService.CreateUserAsync(Mapper.Map<RegistrationViewModel, UserDto>(model));
                if (!result.Item1)
                    return BadRequest(result.Item2);
                var rezultSave = await _userService.SaveChangesAsync();
                return rezultSave.Item1
                    ? Ok("Регистрация успешна. Попробуйте авторизоваться.")
                    : Helper.SetObjectResultStatus(rezultSave.Item2, 500);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        /// <summary>
        /// Деавторизация
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        ///  GET: api/users/LogOut
        /// </remarks>
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _userService.LogOut();
                var token = string.Empty;
                return Ok(new { token }); //вернуть пустой токен в ui
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return BadRequest(e);
            }
        }
        /// <summary>
        /// Заглушка на старт
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET: api/users/GetSome
        /// </remarks>
        [HttpGet]
        [ActionName("GetSome")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSome()
        {
            return await Task.Run(() => Ok(new JsonResult(new string[] { "value1", "value2" })));
        }
        /// <summary>
        /// Получение списка зарегистрированных пользователей, кроме текущего авторизованного
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET : /api/users/GetUsers
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserId").Value;
                var users = await _userService.GetUsers(userId);
                return (IActionResult)new JsonResult(Mapper.Map<IEnumerable<UserDto>, IEnumerable<UserViewModel>>(users.Item3), JsonSupport.JsonSerializerSettings);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        #endregion

        #region help-methods
        /// <summary>
        /// Получение токена
        /// </summary>
        /// <param name="userDto">Пользователь</param>
        /// <returns>object</returns>
        private string GetToken(UserDto userDto)
        {
            /*var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(userDto),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDesciptor);
            return tokenHandler.WriteToken(securityToken);*/
             
            // аналогичный вариант
            //получить токен и вернуть в ui:
            var identity = GetClaimsIdentity(userDto);
            var opt = new AuthOptions() { Key = _appSettings.JWT_Secret };
            //1. создаем JWT-токен
            var jwt = new JwtSecurityToken(
                opt.Issuer,
                opt.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(opt.Lifetime)),
                signingCredentials: new SigningCredentials(opt.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt); //создается Json-представление токена
            HttpContext.Session.SetString("token", token); //запоминаем токен сессии
            //2. вернуть в ui
            /*var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                password = userDto.Password,
                id = userDto.Id
            };*/
            return token;            
        }
        /// <summary>
        /// Формирование требований идентификации
        /// </summary>
        /// <param name="userDto">Пользователь</param>
        /// <returns>ClaimsIdentity</returns>
        private ClaimsIdentity GetClaimsIdentity(UserDto userDto)
        {
            /*return new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userDto.Id.ToString())
                });*/
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "userRole"),
                new Claim("UserId", userDto.Id.ToString())
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        /// <summary>
        /// Формирование списка ошибок
        /// </summary>
        /// <param name="modelErrors">Список ошибок модели</param>
        /// <param name="errors"></param>
        /// <param name="separator"></param>
        /// <returns>string</returns>
        private string ConstructErrors(IEnumerable<ModelError> modelErrors, string separator = "; ")
        {
            var errorsCollection = new Collection<string>();
            foreach (var error in modelErrors)
            {
                errorsCollection.Add(error.ErrorMessage);
            }
            return string.Join(separator, errorsCollection);
        }
        #endregion

    }
}