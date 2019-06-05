using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс общего представления транзакции с валидацией
    /// </summary>
    public class TransactionCommonViewModel<Tkey> : ModelValidationBase<Tkey>, IValidatableObject where Tkey : struct
    {
        #region props
        /// <summary>
        /// Дата выполнения
        /// </summary>
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExecutionDate { get; set; }
        /// <summary>
        /// Значение PW
        /// </summary>
        /// [Display(Name = "Значение")]
        [Required(ErrorMessage = "Не указано значение PW")]
        public decimal Value { get; set; }
        /// <summary>
        /// Отправитель
        /// </summary>
        [Display(Name = "Отправитель")]
        public string Sender { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        [Display(Name = "Получатель")]
        public string Recipient { get; set; }
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public string RecipientId { get; set; }
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
            Validator.TryValidateProperty(ExecutionDate,
                new ValidationContext(this, null, null) { MemberName = "ExecutionDate" },
                results);
            Validator.TryValidateProperty(Value,
                new ValidationContext(this, null, null) { MemberName = "Value" },
                results);
            return results;
        }
        #endregion
    }
}
