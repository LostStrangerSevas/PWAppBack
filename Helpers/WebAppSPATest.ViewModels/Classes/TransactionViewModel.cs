using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    /// <summary>
    /// Базовый класс представления транзакции с валидацией
    /// </summary>
    public class TransactionViewModel<Tkey> : ModelValidationBase<Tkey>, IValidatableObject where Tkey : struct
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
        /// История балансов транзакций
        /// </summary>
        public virtual ICollection<BalanceStorageViewModel<int>> BalanceStorages { get; set; }
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
