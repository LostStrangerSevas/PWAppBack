using System;
using System.Collections.Generic;

namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO транзакции
    /// </summary>
    public class TransactionDto : BaseDtoEntity<int>
    {
        /// <summary>
        /// Дата выполнения
        /// </summary>
        public DateTime ExecutionDate { get; set; }
        /// <summary>
        /// Значение PW
        /// </summary>
        public decimal Value { get; set; }        
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        public virtual ICollection<BalanceStorageDto> BalanceStorages { get; set; }
    }
}
