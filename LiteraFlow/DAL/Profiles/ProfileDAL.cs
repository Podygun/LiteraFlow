namespace LiteraFlow.Web.DAL.Profiles;

public class ProfileDAL : IProfileDAL
{
    /// <summary>
    /// Get user profile on userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>ProfileModel or new() ProfileModel</returns>
    public async Task<ProfileModel> GetAsync(int userId)
    {
        var result = await DBHelper.QueryScalarAsync<ProfileModel>(
            @"select ProfileId, UserId, FullName, ProfileImage, About, Gender, DateBirth
              from Profiles 
              where UserId = @userId", new { userId = userId });
        return result ?? new ProfileModel();
    }

    public async Task<int> CreateAsync(ProfileModel model)
    {
        return await DBHelper.ExecuteScalarAsync<int>(
            @"insert into Profiles (UserId, FullName, ProfileImage, About, Gender, DateBirth) 
              values (@UserId, @FullName, @ProfileImage, @About, @Gender, @DateBirth) returning ProfileId", model);
    }

    public async Task<int> UpdateAsync(ProfileModel model)
    {
        return await DBHelper.ExecuteScalarAsync<int>(
            @"update Profiles set 
              FullName = @FullName, 
              ProfileImage = @ProfileImage, 
              About = @About, 
              Gender = @Gender, 
              DateBirth = @DateBirth where ProfileId = @ProfileId returning ProfileId", model);
    }
}
