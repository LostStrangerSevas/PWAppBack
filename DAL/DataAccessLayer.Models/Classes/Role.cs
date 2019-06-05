using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models.Classes
{
    public class Role : IdentityRole
    {
        /// <summary>
        /// Описание
        /// </summary>
        public virtual string Description { get; set; }
    }
}
