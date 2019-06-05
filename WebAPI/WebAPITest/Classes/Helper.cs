using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Classes
{
    public static class Helper
    {
        #region methods
        /// <summary>
        /// Формирование ObjectResult
        /// </summary>
        /// <param name="msg">Описание ошибки</param>
        /// <param name="statusCode">Статусный код</param>
        /// <returns>ObjectResult</returns>
        public static ObjectResult SetObjectResultStatus(string msg, int statusCode = 200)
        {
            return new ObjectResult(msg) { StatusCode = statusCode };
        }
        #endregion
    }
}
