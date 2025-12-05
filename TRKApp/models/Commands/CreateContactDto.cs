using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands
{
    public class CreateContactDto
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Имя не может превышать 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [StringLength(255, ErrorMessage = "Email не может превышать 255 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вопрос обязателен для заполнения")]
        [StringLength(1000, ErrorMessage = "Вопрос не может превышать 1000 символов")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Необходимо согласиться с политикой конфиденциальности")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Необходимо согласиться с политикой конфиденциальности")]
        public bool Agreement { get; set; }
    }
}