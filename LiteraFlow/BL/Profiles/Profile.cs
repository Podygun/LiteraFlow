using LiteraFlow.Web.DAL.Profiles;
using LiteraFlow.Web.Services;

namespace LiteraFlow.Web.BL.Profiles;


public class Profile : IProfile
{
    private readonly IProfileDAL profileDAL;
    private readonly CacheService cache;

    public Profile(IProfileDAL profileDAL, CacheService cache)
    {
        this.profileDAL = profileDAL;
        this.cache = cache;
    }

    public async Task<ProfileModel> GetAsync(int userId)
    {
        var profile = cache.GetOrNull<ProfileModel?>(CacheConstants.PROFILE);
        if(profile == null)
        {
            profile = await profileDAL.GetAsync(userId);
            cache.Set(CacheConstants.PROFILE, profile, 24*3600);
        }
        return profile;
    }

    public async Task<int> CreateOrUpdateAsync(ProfileModel model)
    {
        if(model.ProfileId == null)
        {
            return await profileDAL.CreateAsync(model);
        }

        else
        {
           cache.Remove(CacheConstants.PROFILE);
           return await profileDAL.UpdateAsync(model);
       }
    }
}
