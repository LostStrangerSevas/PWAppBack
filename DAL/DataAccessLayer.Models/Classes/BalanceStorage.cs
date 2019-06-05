using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Classes
{
    /// <summary>
    /// История балансов транзакций
    /// </summary>
    [Table("BalanceStorage")]
    public class BalanceStorage : BaseEntity<int>
    {
        /// <summary>
        /// Идентификатор транзакции 
        /// </summary>
        [Column("TransactionId", TypeName = "int")]
        public int TransactionId { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Column("UserId", TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        /// <summary>
        /// Флаг роли пользователя
        /// </summary>
        [Column("IsSender", TypeName = "bit")]
        public bool IsSender { get; set; }
        /// <summary>
        /// Флаг времени
        /// </summary>
        [Column("IsBefore", TypeName = "bit")]
        public bool IsBefore { get; set; }     
        /// <summary>
        /// Значение баланса
        /// </summary>
        [Column("Value", TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        /// <summary>
        /// Транзакция
        /// </summary>
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
