namespace LiteraFlow.Web.BL
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<bool> IsAuthorized();
        Task<int?> GetUserIdByToken();
        Task<int?> GetCurrentUserId();
        Task<ProfileModel> GetProfile();
        Task<int> GetProfileId();
    }
}
