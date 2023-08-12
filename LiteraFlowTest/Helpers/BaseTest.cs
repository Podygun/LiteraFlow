using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.Auth;
using LiteraFlow.Web.DAL.DBSession;
using LiteraFlow.Web.DAL.UserToken;

namespace LiteraFlowTest.Helpers;


public class BaseTest
{
    protected readonly IAuthDAL authDAL = new AuthDAL();
    protected readonly IAuth auth;
    protected readonly IDBSessionDAL dBSessionDAL = new DBSessionDAL();
    protected IDBSession dBSession;
    protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
    protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
    protected IWebCookie webCookie;

    public BaseTest()
    {
        webCookie = new TestCookie();
        dBSession = new DBSession(dBSessionDAL, webCookie);
        auth = new Auth(authDAL, dBSession, userTokenDAL, webCookie);
    }

}
