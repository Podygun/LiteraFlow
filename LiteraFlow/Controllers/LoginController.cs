using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.Excepions;

namespace LiteraFlow.Web.Controllers;


public class LoginController : Controller
{
    private readonly IAuth _authBL;

    public LoginController(IAuth authBL)
    {
        _authBL = authBL;
    }

    /// <summary>
    /// Форма авторизации
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("/login")] 
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }


    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> IndexPost(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {

            try
            {
                await _authBL.AuthenticateAsync(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch (AuthorizationException)
            {
                ModelState.TryAddModelError("Email", "Неверные Email или пароль");
            }
           
        }
        return View("Index", model);
    }


}
