using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.DAL.Auth;
using LiteraFlow.Web.DAL.DBSession;

namespace LiteraFlowTest.Helpers;


public class BaseTest
{
    protected readonly IAuthDAL authDAL = new AuthDAL();
    protected readonly IAuth auth;
    protected readonly IDBSessionDAL dBSessionDAL = new DBSessionDAL();
    protected IDBSession dBSession;
    protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

    public BaseTest()
    {
        dBSession = new DBSession(dBSessionDAL, httpContextAccessor);
        auth = new Auth(authDAL, dBSession);
    }

}
