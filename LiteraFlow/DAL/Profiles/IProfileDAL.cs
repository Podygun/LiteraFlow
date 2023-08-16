namespace LiteraFlow.Web.DAL.Profiles;

public interface IProfileDAL
{
    Task<int> CreateAsync(ProfileModel model);
    Task<ProfileModel> GetAsync(int userId);
    Task<int> UpdateAsync(ProfileModel model);
}