using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.BL.Profiles;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.UserToken;
using LiteraFlow.Web.Middleware;
using LiteraFlow.Web.Services;


namespace LiteraFlow.Web.Controllers;


[SiteAuthenticate()]
public class ProfileController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IProfile profile;

    public ProfileController( ICurrentUser currentUser, IProfile profile)
    {
        this.currentUser = currentUser;
        this.profile = profile;
    }

    [HttpGet]
    [Route("/profile")]
    public async Task<IActionResult> Index()
    {
        var profile = await currentUser.GetProfile();
        return View(ProfileMapper.ProfileModelToViewModel(profile));
    }

    [HttpPost]
    [Route("/profile")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(ProfileViewModel model)
    {
        
        if (!ModelState.IsValid)
        {
            //загружаем форму с исходными полями
            int? uid = await currentUser.GetCurrentUserId();
            model = ProfileMapper.ProfileModelToViewModel(await this.profile.GetAsync((int)uid));
            return View("Index", model);
        }

        if (model.Gender == "Empty")
            model.Gender = null;


        // Проверка на совпадение id профиля
        var profile = await currentUser.GetProfile();
        if (profile.ProfileId != model.ProfileId)
            throw new Exception("Ошибка обновления данных");

        if(profile.UserId == 0)
        {
            int? userId = await currentUser.GetCurrentUserId();
            model.UserId = userId;
        }
        
        await this.profile.CreateOrUpdateAsync(ProfileMapper.ProfileViewModelToModel(model));

        return Redirect("/");
    }


    [ActionName("exit")]
    public async Task<IActionResult> Exit(
        [FromServices] IWebCookie wc,  
        [FromServices] IDBSession dbs,
        [FromServices] IUserTokenDAL ut        
        )
    {
        var idSession = wc.Get(BLConstants.SESSION_COOKIE_NAME);
        Guid? guidSession = Helper.StringToGuidOrDefault(idSession ?? "");
        if (guidSession == null) 
            return NotFound();


        //delete session
        var session = await dbs.GetDBSession();
        await dbs.Delete(session.DbSessionId);

        //delete all cookies
        wc.Delete(BLConstants.REMEMBER_ME_COOKIE_NAME);
        wc.Delete(BLConstants.SESSION_COOKIE_NAME);

        //delete token
        var userId = await currentUser.GetCurrentUserId();
        if(userId == null) 
            return NotFound();
        await ut.DeleteAsync((int)userId);

        return Redirect("/");
    }


    [HttpPost]
    [Route("/profile/saveimage")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SaveImage(int? profileId)
    {
        if (!ModelState.IsValid)
            return Redirect("/profile");
        
        var profile = await currentUser.GetProfile();
        if (profile.ProfileId != profileId)
            throw new Exception("Ошибка обновления данных");


        if (Request.Form.Files.Count > 0)
            profile.ProfileImage = await WebFile.SaveAsync(Request.Form.Files[0], WebFile.PROFILE_PATH);
        else
            profile.ProfileImage = null;

        await this.profile.CreateOrUpdateAsync(profile);

        return Redirect("/profile");
    }
}
