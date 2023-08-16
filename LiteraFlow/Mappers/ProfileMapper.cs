namespace LiteraFlow.Web.Mappers;


public static class ProfileMapper
{

    /// <summary>
    /// Front to Back
    /// </summary>
    public static ProfileModel ProfileViewModelToModel(ProfileViewModel viewModel)
    {
        return new ProfileModel
        {
            ProfileId = viewModel.ProfileId,
            FullName = viewModel.FullName,
            About = viewModel.About,
            Gender = viewModel.Gender,
            DateBirth = viewModel.DateBirth,
            ProfileImage = viewModel.ProfileImage,
            UserId = viewModel.UserId ?? 0
        };
    }

    /// <summary>
    /// Back to Front
    /// </summary>
    public static ProfileViewModel ProfileModelToViewModel(ProfileModel model)
    {
        return new ProfileViewModel
        {
            ProfileId = model.ProfileId,
            FullName = model.FullName,
            About = model.About,
            Gender = model.Gender,
            DateBirth = model.DateBirth,
            ProfileImage = model.ProfileImage
        };
    }
}
