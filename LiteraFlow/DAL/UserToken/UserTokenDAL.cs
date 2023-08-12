namespace LiteraFlow.Web.DAL.UserToken
{
    public class UserTokenDAL : IUserTokenDAL
    {
        public async Task<Guid> CreateAsync(int userId) =>
            await DBHelper.ExecuteScalarAsync<Guid>(
           @"insert into UserTokens 
         (UserTokenId, UserId, CreatedAt)
         values 
         (@tokenid, @userid, NOW()) returning UserTokenId", new { tokenid = Guid.NewGuid(), userid = userId});

        public async Task<int?> GetUserIdAsync(Guid sessionId) =>
        await DBHelper.QueryScalarAsync<int?>(
        @"select UserId 
          from UserTokens 
          where UserTokenId = @id", new { id = sessionId });


        

    }
}
