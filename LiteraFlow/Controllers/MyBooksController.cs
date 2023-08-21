using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.Middleware;


namespace LiteraFlow.Web.Controllers;

[SiteAuthenticate]
public class MyBooksController : Controller
{
    private readonly IBooks booksService;
    private readonly ICurrentUser currentUser;

    public MyBooksController(IBooks books, ICurrentUser currentUser)
    {
        this.booksService = books;
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/mybooks")]
    public async Task<IActionResult> Index()
    {
        int? userId = await currentUser.GetCurrentUserId();
        if (userId == null)
            throw new Exception("Non authorize user");

        var books = await booksService.GetUserBooks((int)userId);


        return View(BookMapper.ModelToViewModel(books).ToList());
    }




}
