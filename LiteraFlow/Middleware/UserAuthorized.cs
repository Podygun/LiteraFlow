using Microsoft.AspNetCore.Mvc.Filters;

namespace LiteraFlow.Web.Middleware;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class UserAuthorized : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser == null)
            throw new Exception("No user middleware");
        
        bool isLoggedIn = await currentUser.IsLoggedIn();
        if (!isLoggedIn)
        {
            context.Result = new RedirectResult("/Login");
            return;
        }

        var profile = await currentUser.GetProfile();

        if (profile.ProfileId == null || profile.ProfileId == 0)
        {
            context.Result = new RedirectResult("/profile");
            return;
        }


    }
}
