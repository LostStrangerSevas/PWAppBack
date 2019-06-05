namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO транзакции общего вида
    /// </summary>
    public class TransactionCommonDto : BaseDtoEntity<int>
    {
        /// <summary>
        /// Дата выполнения
        /// </summary>
        public string ExecutionDate { get; set; }
        /// <summary>
        /// Значение PW
        /// </summary>
        public decimal Value { get; set; }        
        /// <summary>
        /// Отправитель
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public string RecipientId { get; set; }
    }
}
