namespace LiteraFlow.Web.BL
{
    public interface ICurrentUser
    {
        public Task<bool> IsLoggedIn();
        public Task<bool> IsAuthorized();
        public Task<int?> GetUserIdByToken();

    }
}
