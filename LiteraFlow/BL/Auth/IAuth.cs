
namespace LiteraFlow.Web.BL.Auth;

public interface IAuth
{
    Task<int> CreateUserAsync(UserModel model);
    Task<int> AuthenticateAsync(string email, string password, bool rememberMe);
    Task SaveIdUserToSession(int id);

    //Task<ValidationResult?> ValidateEmail(string email);
    Task ValidateEmail(string email);
    Task RegisterUserAsync(UserModel user);
}
