using LiteraFlow.Web.Middleware;
using LiteraFlow.Web.Services;


namespace LiteraFlow.Web.Controllers;


[SiteAuthorize()]
public class ProfileController : Controller
{
    private readonly ICurrentUser currentUser;

    public ProfileController(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/profile")]
    public IActionResult Index()
    {
        return View(new ProfileViewModel());
    }

    [HttpPost]
    [Route("/profile")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(ProfileViewModel model)
    {
        //if(ModelState.IsValid) { }

        var imgData = Request.Form.Files[0];
        if(imgData is null) return View(model);

        string filename = WebFile.CreateWebFile(imgData.FileName);
        await WebFile.UploadAndResizeImage(imgData.OpenReadStream(), filename, 800, 600);

        return View("Index", new ProfileViewModel());
    }
}
