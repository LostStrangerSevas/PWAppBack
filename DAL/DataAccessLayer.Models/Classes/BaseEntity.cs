using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Classes
{
    /// <summary>
    /// Базовый класс с идентификатором сущности
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Ключевое поле типа Т
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
