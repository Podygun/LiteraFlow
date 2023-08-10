using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Excepions;
using LiteraFlow.Web.BL.Exceptions;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.DAL.Auth;

namespace LiteraFlow.Web.BL.Auth;

public class Auth : IAuth
{
    private readonly IAuthDAL authDAL;
    private readonly IDBSession dBSessionBL;

    public Auth(IAuthDAL authDAL,IDBSession dBSessionBL)
    {
        this.authDAL = authDAL;
        this.dBSessionBL = dBSessionBL;
    }

    public async Task<int> AuthenticateAsync(string email, string password, bool rememberMe)
    {
        var user = await authDAL.GetUserAsync(email);
        if (user.UserId == 0) throw new AuthorizationException();
        if (user.Password == Encrypter.HashPassword(password, user.Salt))
        {
            await SaveIdUserToSession(user.UserId);
            return user.UserId;
        }
        throw new AuthorizationException();
    }

    public async Task<int> CreateUserAsync(UserModel model)
    {
        model.Salt = Guid.NewGuid().ToString();
        model.Password = Encrypter.HashPassword(model.Password, model.Salt);
        int id = await authDAL.CreateUserAsync(model);
        await SaveIdUserToSession(id);
        return id;
    }

    public async Task SaveIdUserToSession(int id)
    {
       await dBSessionBL.SetUserId(id);  
       // httpContextAccessor.HttpContext?.Session.SetInt32(BLConstants.AUTH_SESSION_PARAM_NAME, id);
    }

    //Depricated
    //public async Task<ValidationResult?> ValidateEmail(string email)
    //{
    //    var user = await authDAL.GetUserAsync(email);
    //    if (user?.UserId == 0) return null;
    //    return new ValidationResult("Пользователь с такой почтой уже есть");
    //}

    public async Task ValidateEmail(string email)
    {
        var user = await authDAL.GetUserAsync(email);
        if (user?.UserId != 0) 
            throw new DuplicateEmailException();
    }

    public async Task RegisterUserAsync(UserModel user)
    {
        using(var scope = Helper.CreateTransactionScope())
        {
            await dBSessionBL.LockAsync();
            await ValidateEmail(user.Email);
            await CreateUserAsync(user);
            scope.Complete();
        }
        
    }
}
