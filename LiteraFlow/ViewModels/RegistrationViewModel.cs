namespace LiteraFlow.Web.ViewModels;


public class RegistrationViewModel
{
    [Required(ErrorMessage = "Введите почту")]
    //[EmailAddress(ErrorMessage = "Некорректный формат")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6, ErrorMessage = "Слишком короткий пароль")]
    [MaxLength(24, ErrorMessage = "Слишком длинный пароль")]
    //[RegularExpression("^.*(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$", ErrorMessage ="Пароль слишком простой")]
    public string? Password { get; set; }

   
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage ="Пароли не совпадают")]
    public string? ConfirmPassword { get; set; }
    


     /// <summary>
    /// Реализовывает IValidatableObject, вызывается при проверке модели IsModelValid
    /// Для реализации доп. проверки свойств модели
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Password == "123456")
        {
            //1 - Сообщение, 2 - Поле, под которым должна появиться ошибка
            yield return new ValidationResult("Не надо так", new[] { "Password" });
        }
    }
}
