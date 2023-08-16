using LiteraFlow.Web.DAL.Profiles;

namespace LiteraFlow.Web.BL.Profiles;


public class Profile : IProfile
{
    private readonly IProfileDAL profileDAL;

    public Profile(IProfileDAL profileDAL)
    {
        this.profileDAL = profileDAL;
    }

    public async Task<ProfileModel> GetAsync(int userId)
    {
        return await profileDAL.GetAsync(userId);
    }

    public async Task<int> CreateOrUpdateAsync(ProfileModel model)
    {
        if(model.ProfileId == null)     
            return await profileDAL.CreateAsync(model);       
        else
            return await profileDAL.UpdateAsync(model);
    }
}
