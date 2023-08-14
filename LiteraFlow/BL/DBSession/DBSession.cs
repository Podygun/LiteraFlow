using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.DBSession;
using LiteraFlow.Web.Models;

namespace LiteraFlow.Web.BL.DBSession;


public class DBSession : IDBSession
{
    private readonly IDBSessionDAL dBSessionDAL;
    private readonly IWebCookie webCookie;

    private DBSessionModel? sessionModel = null;

    public DBSession(
        IDBSessionDAL dBSessionDAL,
        IWebCookie webCookie)
    {
        this.dBSessionDAL = dBSessionDAL;
        this.webCookie = webCookie;
    }

    public async Task<DBSessionModel> GetDBSession()
    {
        if (sessionModel != null) return sessionModel;


        Guid sessionId = Guid.Empty;
        string? cookie = webCookie.Get(BLConstants.SESSION_COOKIE_NAME) ?? null;

        //Парс текущей куки (если авторизован)
        if (!String.IsNullOrEmpty(cookie))
        {
            sessionId = Guid.Parse(cookie);
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

    //Получение UserId из Сессии
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

    //Создание авторизованой сессии
    public async Task<int> SetUserId(int userId)
    {
        var session = await GetDBSession();
        //TODO
        if (session == null) return 0;
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
        webCookie.AddSecure(BLConstants.SESSION_COOKIE_NAME, sessionId.ToString());     
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
