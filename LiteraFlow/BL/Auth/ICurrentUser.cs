namespace LiteraFlow.Web.BL
{
    public interface ICurrentUser
    {
        public Task<bool> IsLoggedIn();
    }
}
