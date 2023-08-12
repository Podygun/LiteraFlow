
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.UserToken;

namespace LiteraFlow.Web.BL
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDBSession dBSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;

        public CurrentUser(
            IDBSession dBSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL)
        {
            this.dBSession = dBSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }

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
}
