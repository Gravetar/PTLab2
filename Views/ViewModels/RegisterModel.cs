using System.ComponentModel.DataAnnotations;

namespace PTLab2.Views.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string? LastName { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string? ConfirmPassword { get; set; }
    }
}
