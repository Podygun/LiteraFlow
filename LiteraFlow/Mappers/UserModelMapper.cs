using LiteraFlow.Web.ViewModels;

namespace LiteraFlow.Web.Mappers
{
    public class UserModelMapper
    {
        public static UserModel UserViewModelToModel(RegistrationViewModel userViewModel)
        {
            return new UserModel { 
                Email = userViewModel.Email!,
                Password = userViewModel.Password!,
                Status = 0
            };
        }
    }
}
