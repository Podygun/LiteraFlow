namespace LiteraFlow.Web.DAL.DBSession
{
    public interface IDBSessionDAL
    {
        Task<int> CreateAsync(DBSessionModel model);
        Task<int> UpdateAsync(DBSessionModel model);
        Task<DBSessionModel?> GetAsync(Guid sessionId);
        Task DeleteAsync(Guid sessionId);
        Task<DBSessionModel?> LockAsync(Guid sessionId);

    }
}
