using PWApp.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс входных данных с валидацией
    /// </summary>
    public abstract class ModelValidationBase<Tkey> : BaseViewModel<Tkey>, IModelValidation where Tkey : struct
    {
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>bool</returns>
        public bool ValidateModel(out ICollection<string> errors)
        {
            errors = new Collection<string>();
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(this);
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (isValid) return true;
            foreach (var error in results)
            {
                errors.Add(error.ErrorMessage);
            }
            return false;
        }
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <param name="separator">Разделитель</param>
        /// <returns>bool</returns>
        public bool ValidateModel(out string errors, string separator = " ;")
        {
            errors = string.Empty;
            var errorsCollection = new Collection<string>();
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(this);
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (isValid) return true;
            foreach (var error in results)
            {
                errorsCollection.Add(error.ErrorMessage);
            }
            errors = string.Join(separator, errorsCollection);
            return false;
        }
    }

    public abstract class ModelValidationBase : IModelValidation
    {
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>bool</returns>
        public bool ValidateModel(out ICollection<string> errors)
        {
            errors = new Collection<string>();
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(this);
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (isValid) return true;
            foreach (var error in results)
            {
                errors.Add(error.ErrorMessage);
            }
            return false;
        }
        /// <summary>
        /// Валидация модели
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <param name="separator">Разделитель</param>
        /// <returns>bool</returns>
        public bool ValidateModel(out string errors, string separator = " ;")
        {
            errors = string.Empty;
            var errorsCollection = new Collection<string>();
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(this);
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (isValid) return true;
            foreach (var error in results)
            {
                errorsCollection.Add(error.ErrorMessage);
            }
            errors = string.Join(separator, errorsCollection);
            return false;
        }
    }
}
