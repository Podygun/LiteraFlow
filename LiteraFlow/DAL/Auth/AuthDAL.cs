namespace LiteraFlow.Web.DAL.Auth;

public class AuthDAL : IAuthDAL
{
    public async Task<int> CreateUserAsync(UserModel model)
    {
        var result = await DBHelper.ExecuteScalarAsync<int>(
            @"insert into Users (Email,Password,Salt,Status) 
              values (@email,@password,@salt,@status) returning UserId", model);
        return result;       
    }
    
    public async Task<UserModel> GetUserAsync(int userId)
    {
        var result = await DBHelper.QueryScalarAsync<UserModel>(
            @"select UserId,Email,Password,Salt,Status
              from Users 
              where UserId = @userid", new { userid = userId });

        return result ?? new UserModel();
        
    }

    public async Task<UserModel> GetUserAsync(string email)
    {
        var result = await DBHelper.QueryScalarAsync<UserModel>(
            @"select UserId,Email,Password,Salt,Status
              from Users 
              where email = @email", new { email = email });

        return result ?? new UserModel();
    }
}
