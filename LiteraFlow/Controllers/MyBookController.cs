using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.Middleware;


namespace LiteraFlow.Web.Controllers;

[SiteAuthenticate]
public class MyBookController : Controller
{
    private readonly IBooks booksService;
    private readonly ICurrentUser currentUser;

    public MyBookController(IBooks books, ICurrentUser currentUser)
    {
        this.booksService = books;
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/mybooks")]
    public async Task<IActionResult> Index()
    {
        int? userId = await currentUser.GetUserIdByToken();
        if (userId == null)
            throw new Exception("Non authorize user");

        var books = await booksService.GetUserBooks((int)userId);
        return View(books);
    }


    #region Editing Book


    [HttpGet]
    [Route("/mybook/new")]
    public IActionResult CreateBook()
    {
        return View();
    }

    [HttpPost]
    [Route("/mybook/new")]
    public IActionResult CreateBook(BookViewModel book)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("/mybook");
        }
        return View();
    }


    #endregion

    #region Editing Chapters


    [HttpPost]
    [Route("/mybook/chapter")]
    public IActionResult PostChapter(ChapterViewModel chapter)
    {
        return View();
    } 


    #endregion


}
