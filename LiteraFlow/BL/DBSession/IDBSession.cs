namespace LiteraFlow.Web.BL.DBSession;

public interface IDBSession
{
    Task<DBSessionModel> GetDBSession();   
    Task<int> SetUserId(int userId);
    Task<int?> GetUserId();
    Task<bool> IsLoggedIn();
    Task LockAsync();

}
