using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.Profiles;
using LiteraFlow.Web.DAL.UserToken;
using LiteraFlow.Web.Middleware;


namespace LiteraFlow.Web.BL;

[SiteAuthenticate()]
public class CurrentUser : ICurrentUser
{
    private readonly IDBSession dBSession;
    private readonly IWebCookie webCookie;
    private readonly IUserTokenDAL userTokenDAL;
    private readonly IProfileDAL profileDAL;

    public CurrentUser(
        IDBSession dBSession,
        IWebCookie webCookie,
        IUserTokenDAL userTokenDAL,
        IProfileDAL profileDAL)
    {
        this.dBSession = dBSession;
        this.webCookie = webCookie;
        this.userTokenDAL = userTokenDAL;
        this.profileDAL = profileDAL;
    }

    /// <summary>
    /// Check for user authenticated
    /// </summary>
    public async Task<bool> IsLoggedIn()
    {
        //получение id через токен
        int? userId = await GetUserIdByToken();

        await dBSession.UpdateCurrentSession();
        //если нет токена, смотрим сессию в БД

        if (userId == null)
        {            
            return await dBSession.IsLoggedIn();
        }
        await dBSession.SetUserId((int)userId!);
        return true;
    }

    /// <summary>
    /// Check for user have or not profile
    /// </summary>
    public async Task<bool> IsAuthorized()
    {
        int? userIdByToken = await GetUserIdByToken();
        int? userIdBySession = await dBSession.GetUserId();

        if (userIdByToken == null && userIdBySession == null) return false;

        if (userIdByToken != null && userIdBySession == null)
        {
            var profile = await profileDAL.GetAsync((int)userIdByToken);
            return profile.ProfileId != null;
        }
        else if (userIdByToken == null && userIdBySession != null)
        {
            var profile = await profileDAL.GetAsync((int)userIdBySession);
            return profile.ProfileId != null;
        }
        else if ((userIdByToken != null || userIdBySession != null) && (userIdByToken == userIdBySession))
        {
            var profile = await profileDAL.GetAsync((int)userIdByToken);
            return profile.ProfileId != null;
        }

        return false;
    }

    public async Task<int?> GetUserIdByToken()
    {
        string? token = webCookie.Get(BLConstants.REMEMBER_ME_COOKIE_NAME);

        if (String.IsNullOrEmpty(token))
            return null;

        Guid? tokenGuid = Helper.StringToGuidOrDefault(token);

        if (tokenGuid == null)
            return null;

        return await userTokenDAL.GetUserIdAsync(tokenGuid ?? Guid.Empty);
    }

    public async Task<int?> GetCurrentUserId()
    {
        return await dBSession.GetUserId();
    }

    public async Task<ProfileModel> GetProfile()
    {
        int? userId = await GetCurrentUserId();
        if (userId == null)
            throw new Exception("Пользователь не найден");
        return await profileDAL.GetAsync((int)userId);
    }
}
