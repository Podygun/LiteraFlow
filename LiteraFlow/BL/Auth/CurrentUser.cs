using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.Profiles;
using LiteraFlow.Web.DAL.UserToken;

namespace LiteraFlow.Web.BL;


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

        if (userId != null)
        {
            //TODO Not creating new, but updating LastEnrty time
            await dBSession.SetUserId((int)userId!);
            return true;
        }

        return await dBSession.IsLoggedIn();
    }

    /// <summary>
    /// Check for user have or not profile
    /// </summary>
    public async Task<bool> IsAuthorized()
    {
        int? userIdByToken = await GetUserIdByToken();
        int? userIdBySession = await dBSession.GetUserId();

        if (userIdByToken == null && userIdBySession == null) return false;

        if(userIdByToken != null && userIdBySession == null)
        {
            var profile = await profileDAL.GetAsync((int)userIdByToken);
            return profile.ProfileId != 0;
        }
        else if (userIdByToken == null && userIdBySession != null)
        {
            var profile = await profileDAL.GetAsync((int)userIdBySession);
            return profile.ProfileId != 0;
        }
        else if ((userIdByToken != null || userIdBySession != null) && (userIdByToken == userIdBySession))
        {
            var profile = await profileDAL.GetAsync((int)userIdByToken);
            return profile.ProfileId != 0;
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
}
