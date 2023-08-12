
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


            bool isLoggedIn = await dBSession.IsLoggedIn();

            //если текущая сессия не авторизована, пытаемся по токену
            if (!isLoggedIn)
            {
                int? userId = await GetUserIdByToken();
                if (userId != null)
                {
                    await dBSession.SetUserId((int)userId!);
                    isLoggedIn = true;
                }

            }
            return isLoggedIn;
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
