namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO истории балансов транзакций
    /// </summary>
    public class BalanceStorageDto : BaseDtoEntity<int>
    {
        /// <summary>
        /// Идентификатор транзакции 
        /// </summary>
        public int TransactionId { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Флаг роли пользователя
        /// </summary>
        public bool IsSender { get; set; }
        /// <summary>
        /// Флаг времени
        /// </summary>
        public bool IsBefore { get; set; }
        /// <summary>
        /// Значение баланса
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Транзакция
        /// </summary>
        public virtual TransactionDto Transaction { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual UserDto User { get; set; }
    }
}
