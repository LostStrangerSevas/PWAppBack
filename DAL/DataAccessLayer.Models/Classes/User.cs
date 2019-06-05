using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataAccessLayer.Models.Classes
{
    public class User : IdentityUser
    {
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
        /// <summary>
        /// Текущий баланс
        /// </summary>
        public virtual Balance Balance { get; set; }
        /// <summary>
        /// История балансов транзакций
        /// </summary>
        public virtual ICollection<BalanceStorage> BalanceStorages { get; set; }
    }
}
