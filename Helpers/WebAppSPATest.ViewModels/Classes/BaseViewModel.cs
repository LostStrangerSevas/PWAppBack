using System.ComponentModel.DataAnnotations;
using System;

namespace PWApp.ViewModels.Classes
{
    public abstract class BaseViewModel<Tkey> where Tkey : struct
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]        
        public Tkey Id { get; set; }
    }
}
