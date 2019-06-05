using BusinessLogic.Models.ClassesDto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс представления баланса с валидацией
    /// </summary>
    public class BalanceViewModel<Tkey> : ModelValidationBase<Tkey>, IValidatableObject where Tkey : struct
    {
        #region props
        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        [Required(ErrorMessage = "Не указан пользователь")]
        public string UserId { get; set; }
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        [Display(Name = "Дата последнего изменения")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Значение баланса
        /// </summary>
        [Display(Name = "Значение баланса")]
        [Required(ErrorMessage = "Не указан баланс")]
        public decimal Value { get; set; }
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
            Validator.TryValidateProperty(UserId,
                new ValidationContext(this, null, null) { MemberName = "UserId" },
                results);
            Validator.TryValidateProperty(ModifiedDate,
                new ValidationContext(this, null, null) { MemberName = "ModifiedDate" },
                results);
            Validator.TryValidateProperty(Value,
                new ValidationContext(this, null, null) { MemberName = "Value" },
                results);
            return results;
        }
        #endregion
    }
}
