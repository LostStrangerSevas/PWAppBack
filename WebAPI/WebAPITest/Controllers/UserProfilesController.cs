using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces.IManagers;
using BusinessLogic.Models.ClassesDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWApp.ViewModels.Classes;
using WebAPITest.Classes;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("FreePolicy")]
    public class UserProfileController : ControllerBase
    {
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<UserProfileController> _logger;
        /// <summary>
        /// Менеджер пользователей
        /// </summary>
        private readonly IUserDtoManager _userService;
        public UserProfileController(IUserDtoManager userService, ILogger<UserProfileController> ilogger)
        {
            _userService = userService;
            _logger = ilogger;
        }
        /// <summary>
        /// Получение авторизованного пользователя
        /// </summary>
        /// <param name="model">Данные пользователя для авторизации</param>
        /// <returns>IActionResult</returns>
        /// <remarks>
        /// GET : /api/UserProfile
        /// </remarks>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserId").Value;
                var user = await _userService.GetUserById(userId);
                return (IActionResult)new JsonResult(Mapper.Map<UserDto, UserViewModel>(user.Item3), JsonSupport.JsonSerializerSettings);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}