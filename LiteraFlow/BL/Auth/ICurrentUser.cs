namespace LiteraFlow.Web.BL
{
    public interface ICurrentUser
    {
        public Task<bool> IsLoggedIn();
        public Task<int?> GetUserIdByToken();

    }
}
