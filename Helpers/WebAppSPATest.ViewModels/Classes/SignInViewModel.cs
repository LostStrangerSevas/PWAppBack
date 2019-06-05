using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    public class SignInViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }
        //[Display(Name = "Логин")]
        //[Required(ErrorMessage = "Не указан логин")]
        //public string Login { get; set; }
    }
}
