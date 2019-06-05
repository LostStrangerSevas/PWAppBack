using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Classes
{
    /// <summary>
    /// Транзакция
    /// </summary>
    [Table("Transaction")]
    public class Transaction : BaseEntity<int>
    {
        /// <summary>
        /// Дата выполнения
        /// </summary>
        [Column("ExecutionDate", TypeName = "smalldatetime")]
        public DateTime ExecutionDate { get; set; }       
        /// <summary>
        /// Значение PW
        /// </summary>
        [Column("Value", TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        public virtual ICollection<BalanceStorage> BalanceStorages { get; set; }
    }
}
