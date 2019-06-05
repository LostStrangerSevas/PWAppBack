using System.ComponentModel;

namespace BusinessLogic.Models.ClassesDto
{
    /// <summary>
    /// Класс DTO пользователя
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// ФИО полностью
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Дополнительное описание
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public virtual string MiddleName { get; set; }
    }
}
