using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Classes
{
    /// <summary>
    /// Текущий баланс пользователя
    /// </summary>
    [Table("Balance")]
    public class Balance : BaseEntity<int>
    {
        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        [Column("UserId", TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        [Column("ModifiedDate", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }        
        /// <summary>
        /// Значение баланса
        /// </summary>
        [Column("Value", TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
