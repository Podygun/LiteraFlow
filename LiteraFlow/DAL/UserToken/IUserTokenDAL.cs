namespace LiteraFlow.Web.DAL.UserToken
{
    public interface IUserTokenDAL
    {
        Task<Guid> CreateAsync(int userId);
        Task<int?> GetUserIdAsync(Guid tokenId);
    }
}
