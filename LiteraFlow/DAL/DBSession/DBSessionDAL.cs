namespace LiteraFlow.Web.DAL.DBSession;


public class DBSessionDAL : IDBSessionDAL
{
    /// <summary>
    /// Create DbSession in DB
    /// </summary>
    /// <param name="model">Object DBSessionModel to create</param>
    /// <returns>DbSessionId just created</returns>
    public async Task<int> CreateAsync(DBSessionModel model) => 
        await DBHelper.ExecuteAsync(
       @"insert into DbSessions 
         (DbSessionId, SessionData, CreatedOn, LastEntry, UserId)
         values 
         (@DbSessionId, @SessionData, @CreatedOn, @LastEntry, @UserId)", model);


    /// <summary>
    /// Update DbSession in DB
    /// </summary>
    /// <param name="model">Update the DBSessionModel on Guid</param>
    /// <returns>Numbers of rows affected</returns>
    public async Task<int> UpdateAsync(DBSessionModel model) => 
        await DBHelper.ExecuteAsync(
        @"update DbSessions 
          set DbSessionId = @DbSessionId, SessionData = @SessionData , CreatedOn = @CreatedOn, LastEntry = @LastEntry, UserId = @UserId
          where DbSessionId = @DbSessionId", model);



    public async Task<DBSessionModel?> GetAsync(Guid sessionId) => 
        await DBHelper.QueryScalarAsync<DBSessionModel>(
        @"select DbSessionId, SessionData, CreatedOn, LastEntry, UserId 
          from DbSessions 
          where DbSessionId = @id", new { id = sessionId});


    public async Task DeleteAsync(Guid sessionId) =>
        await DBHelper.ExecuteAsync(
            @"delete from DbSessions where DbSessionId = @id", new { id = sessionId });


    public async Task<DBSessionModel?> LockAsync(Guid sessionId) => 
        await DBHelper.QueryScalarAsync<DBSessionModel>(
        @"select DbSessionId 
          from DbSessions 
          where DbSessionId = @id for update", new { id = sessionId });

}
