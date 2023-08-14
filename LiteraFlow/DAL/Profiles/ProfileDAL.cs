namespace LiteraFlow.Web.DAL.Profiles;

public class ProfileDAL : IProfileDAL
{
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
        var result = await DBHelper.ExecuteScalarAsync<int>(
            @"insert into Profiles (UserId, FullName, ProfileImage, About, Gender, DateBirth) 
              values (@UserId, @FullName, @ProfileImage, @About, @Gender, @DateBirth) returning ProfileId", model);
        return result;
    }

    public async Task UpdateAsync(ProfileModel model)
    {
        await DBHelper.ExecuteScalarAsync<int>(
            @"update Profiles set 
              FullName = @FullName, 
              ProfileImage = @ProfileImage, 
              About = @About, 
              Gender = @Gender, 
              DateBirth = @DateBirth) where ProfileId = @ProfileId", model);
    }
}
