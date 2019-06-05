using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPITest.Classes
{
    /// <summary>
    /// Поддержка для Json
    /// </summary>
    public static class JsonSupport
    {

        /// <summary>
        /// Настройки сериализации
        /// </summary>
        private static JsonSerializerSettings _jsonSerializerSettings = null;

        /// <summary>
        /// Настройки сериализации
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings => _jsonSerializerSettings ?? (_jsonSerializerSettings = GetJsonSerializerSettings());

        /// <summary>
        /// Получение настроек сериализации JSON
        /// </summary>
        /// <returns>JsonSerializerSettings</returns>
        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            };
        }

        /// <summary>
        /// Возвращает ContentResult с @"application/json; charset=utf-8", StatusCode = 200, и содержимым из строки
        /// </summary>
        /// <param name="stringJson"></param>
        /// <returns>ContentResult</returns>
        public static ContentResult ContentResultJson(string stringJson)
        {
            var contentResultJson = new ContentResult
            {
                Content = stringJson,
                ContentType = @"application/json; charset=utf-8",
                StatusCode = 200
            };
            return contentResultJson;
        }
    }
}