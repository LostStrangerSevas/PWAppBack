using BusinessLogic.Models.ClassesDto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс представления истории балансов транзакций с валидацией
    /// </summary>
    public class BalanceStorageViewModel<Tkey> : ModelValidationBase<Tkey>, IValidatableObject where Tkey : struct
    {
        #region props
        /// <summary>
        /// Идентификатор транзакции 
        /// </summary>
        [Required(ErrorMessage = "Не указана транзакция")]
        public int TransactionId { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Не указан пользователь")]
        public string UserId { get; set; }
        /// <summary>
        /// Флаг роли пользователя
        /// </summary>
        [Required(ErrorMessage = "Не указана роль пользователя")]
        public bool IsSender { get; set; }
        /// <summary>
        /// Флаг времени
        /// </summary>
        [Required(ErrorMessage = "Не указан признак времени")]
        public bool IsBefore { get; set; }
        /// <summary>
        /// Значение баланса
        /// </summary>
        [Display(Name = "Значение баланса")]
        [Required(ErrorMessage = "Не указано значение баланса")]
        public decimal Value { get; set; }
        /// <summary>
        /// Транзакция
        /// </summary>
        public virtual TransactionDto Transaction { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual UserDto User { get; set; }
        #endregion

        #region IValidatableObject
        /// <summary>
        /// Встроенный валидатор
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new Collection<ValidationResult>();
            Validator.TryValidateProperty(TransactionId,
                new ValidationContext(this, null, null) { MemberName = "TransactionId" },
                results);
            Validator.TryValidateProperty(UserId,
                new ValidationContext(this, null, null) { MemberName = "UserId" },
                results);
            Validator.TryValidateProperty(Value,
                new ValidationContext(this, null, null) { MemberName = "Value" },
                results);
            return results;
        }
        #endregion
    }
}
