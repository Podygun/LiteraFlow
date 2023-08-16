using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Helpers;
using LiteraFlow.Web.BL.Profiles;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.UserToken;
using LiteraFlow.Web.Middleware;
using LiteraFlow.Web.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LiteraFlow.Web.Controllers;


[SiteAuthenticate()]
public class ProfileController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IProfile profile;
    //private readonly IWebCookie webCookie;
    //private readonly IDBSession dBSession;
    //private readonly IUserTokenDAL userTokenDAL;


    public ProfileController(
        ICurrentUser currentUser, 
        IProfile profile
        // IWebCookie webCookie, 
        // DBSession dBSession, 
        // IUserTokenDAL userTokenDAL
        )
    {
        this.currentUser = currentUser;
        this.profile = profile;
        //this.webCookie = webCookie;
        //this.dBSession = dBSession;
        //this.userTokenDAL = userTokenDAL;
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

        // Проверка на совпадение id профиля
        var profile = await currentUser.GetProfile();
        if (profile.ProfileId != model.ProfileId)
            throw new Exception("Ошибка обновления данных");

        
        #region ProfileImage Processing

        if (Request.Form.Files.Count > 0)
            model.ProfileImage = await GetPathOfProcessedImage(Request?.Form?.Files[0]);
        else
            model.ProfileImage = null;

        #endregion

        model.UserId = profile.UserId;
        await this.profile.CreateOrUpdateAsync(ProfileMapper.ProfileViewModelToModel(model));
        

        return Redirect("/");
    }

    public async Task<string?> GetPathOfProcessedImage(IFormFile? imgData)
    {
        if (imgData is null) return null;
        string filename = WebFile.CreateWebFile(imgData.FileName);
        await WebFile.UploadAndResizeImage(imgData.OpenReadStream(), filename, 800, 600);
        return filename;
    }

    [ActionName("exit")]
    public async Task<IActionResult> Exit()
    {
        return Redirect("/");
        //var idSession = webCookie.Get(BLConstants.SESSION_COOKIE_NAME);
        //Guid? guidSession = Helper.StringToGuidOrDefault(idSession ?? "");
        //if (guidSession == null) return NotFound();


        //delete session
        //var session = await dBSession.GetDBSession();
        //await dBSession.Delete(session.DbSessionId);

        ////delete all cookies
        //webCookie.Delete(BLConstants.REMEMBER_ME_COOKIE_NAME);
        //webCookie.Delete(BLConstants.SESSION_COOKIE_NAME);

        ////delete token
        //var userId = await currentUser.GetCurrentUserId();
        //if(userId == null) return NotFound();
        //await userTokenDAL.DeleteAsync((int)userId);

        //return Redirect("/");
    }
}
