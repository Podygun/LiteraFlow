using LiteraFlow.Web.DAL.DBSession;
using LiteraFlow.Web.Models;

namespace LiteraFlow.Web.BL.DBSession;


public class DBSession : IDBSession
{
    private readonly IDBSessionDAL dBSessionDAL;
    private readonly IHttpContextAccessor httpContextAccessor;

    private DBSessionModel? sessionModel = null;

    public DBSession(
        IDBSessionDAL dBSessionDAL, 
        IHttpContextAccessor httpContextAccessor)
    {
        this.dBSessionDAL = dBSessionDAL;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<DBSessionModel> GetDBSession()
    {
        if (sessionModel != null) return sessionModel;


        Guid sessionId = Guid.Empty;
        var cookie = httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault
            (c => c.Key == BLConstants.SESSION_COOKIE_NAME) ?? null;

        //Парс текущей куки (если авторизован)
        if (cookie != null && cookie.Value.Value != null)
        {
            sessionId = Guid.Parse(cookie.Value.Value);
        }
            
        //Если её нет, создаем новую сессию
        else
        {
            var newSession = await CreateSession();
            CreateCookie(newSession.DbSessionId);
            return newSession;
        }
            
        //Сессия из бд по новому guid
        var data = await dBSessionDAL.GetAsync(sessionId);
        return data!;
    }

    public async Task<int?> GetUserId()
    {
        var session = await GetDBSession();
        return session.UserId;
    }

    public async Task<bool> IsLoggedIn()
    {
        var data = await GetDBSession();
        return data?.UserId != null;
    }

    public async Task<int> SetUserId(int userId)
    {
        var session = await GetDBSession();
        await dBSessionDAL.DeleteAsync(session.DbSessionId);
        //await dBSessionDAL.DeleteAsync(userId);
        session.UserId = userId;
        session.DbSessionId = Guid.NewGuid();
        session.CreatedOn = DateTime.Now;
        session.LastEntry = DateTime.Now;
        CreateCookie(session.DbSessionId);
        sessionModel = session;
        return await dBSessionDAL.CreateAsync(session);
    }

    private void CreateCookie(Guid sessionId)
    {
        CookieOptions options = new()
        {
            Path = "/",
            HttpOnly = true,
            Secure = true
        };
        httpContextAccessor?.HttpContext?.Response.Cookies.Delete(BLConstants.SESSION_COOKIE_NAME);
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(BLConstants.SESSION_COOKIE_NAME, sessionId.ToString(), options);
        
    }

    private async Task<DBSessionModel> CreateSession()
    {
        var data = new DBSessionModel() 
        { 
            DbSessionId = Guid.NewGuid(),
            CreatedOn = DateTime.Now,
            LastEntry = DateTime.Now
        };
        await dBSessionDAL.CreateAsync(data);
        sessionModel = data;
        return data;
    }

    public async Task LockAsync()
    {
        var data = await GetDBSession();
        await dBSessionDAL.LockAsync(data.DbSessionId);
    }
}
