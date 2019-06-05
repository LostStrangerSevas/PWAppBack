using System;

namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO баланса
    /// </summary>
    public class BalanceDto : BaseDtoEntity<int>
    {
        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Значение баланса
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual UserDto User { get; set; }
    }
}
