namespace LiteraFlow.Web.DAL.Auth;

public interface IAuthDAL
{
    Task<UserModel> GetUserAsync(int userId);
    Task<UserModel> GetUserAsync(string email);
    Task<int> CreateUserAsync(UserModel model);
}
