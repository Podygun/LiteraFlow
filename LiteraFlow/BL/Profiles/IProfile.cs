namespace LiteraFlow.Web.BL.Profiles;

public interface IProfile
{
    Task<int> CreateOrUpdateAsync(ProfileModel model);
    Task<ProfileModel> GetAsync(int userId);
}