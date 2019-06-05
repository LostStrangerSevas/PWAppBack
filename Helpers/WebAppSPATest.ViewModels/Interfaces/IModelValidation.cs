using System.Collections.Generic;

namespace PWApp.ViewModels.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для реализации валидации
    /// </summary>
    public interface IModelValidation
    {
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <param name="separator">Разделитель</param>
        /// <returns>bool</returns>
        bool ValidateModel(out string errors, string separator = "; ");
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>bool</returns>
        bool ValidateModel(out ICollection<string> errors);
    }
}
