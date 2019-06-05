using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PWApp.ViewModels.Classes
{
    public class UserViewModel
    {
        [Display(Name = "Идентификатор")]
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Имя")]
        //[Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Фамилия")]
        //[Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Отчество")]
        //[Required(ErrorMessage = "Не указано отчество")]
        public string MiddleName { get; set; } = string.Empty;

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }
        /*
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Required(ErrorMessage = "Не указано подтверждение пароля")]
        [PasswordPropertyText(true)]
        public string PasswordConfirm { get; set; }
        */
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}
