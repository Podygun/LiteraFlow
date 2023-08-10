
using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.Exceptions;

namespace LiteraFlow.Web.Controllers;

public class RegistrationController : Controller
{
    private readonly IAuth auth;

    public RegistrationController(IAuth auth)
    {
        this.auth = auth;
    }

    [HttpGet]
    [Route("/registration")]
    public IActionResult Index()
    {
        return View("Index", new RegistrationViewModel());
    }

    [HttpPost]
    [Route("/registration")]
    public async Task<IActionResult> IndexPost(RegistrationViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        try
        {
            await auth.RegisterUserAsync(UserModelMapper.UserViewModelToModel(model));
            return Redirect("/");
        }
        catch (DuplicateEmailException)
        {
            ModelState.TryAddModelError("Email", "Email уже существует");
        }

        return View("Index", model);

    }


}
