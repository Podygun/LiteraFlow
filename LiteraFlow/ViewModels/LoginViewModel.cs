
namespace LiteraFlow.Web.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите почту")]
    //[EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    public string? Password { get; set; }

    public bool? RememberMe { get; set; }
}
