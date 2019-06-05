namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO новой транзакции
    /// </summary>
    public class TransactionAddDto 
    {        
        /// <summary>
        /// Значение PW
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public string RecepientId { get; set; }
        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public string UserId { get; set; }
    }
}
