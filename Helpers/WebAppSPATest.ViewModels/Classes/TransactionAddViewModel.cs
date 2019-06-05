using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс представления новой транзакции с валидацией
    /// </summary>
    public class TransactionAddViewModel: ModelValidationBase, IValidatableObject
    {
        #region props
        /// <summary>
        /// Значение PW
        /// </summary>
        [Display(Name = "Значение PW")]
        [Required(ErrorMessage = "Не указано значение PW")]
        public decimal Value { get; set; }
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        [Display(Name = "Идентификатор получателя")]
        [Required(ErrorMessage = "Не указан идентификатор получателя")]
        public string RecepientId { get; set; }
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
            Validator.TryValidateProperty(RecepientId,
                new ValidationContext(this, null, null) { MemberName = "RecepientId" },
                results);
            Validator.TryValidateProperty(Value,
                new ValidationContext(this, null, null) { MemberName = "Value" },
                results);
            return results;
        }
        #endregion
    }
}
