using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.Middleware;


namespace LiteraFlow.Web.Controllers;

[UserAuthorized]
public class MyBooksController : Controller
{
    private readonly IBooks booksBL;
    private readonly ICurrentUser currentUser;

    public MyBooksController(IBooks books, ICurrentUser currentUser)
    {
        this.booksBL = books;
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/mybooks")]
    public async Task<IActionResult> Index()
    {
        var books = await GetBooks();

        if (books == null)
            return Redirect("/profile");

        return View(BookMapper.ModelToViewModel(books).ToList());
    }

    [HttpGet]
    [Route("/mybooks/{bookId}")]
    public async Task<IActionResult> LoadBook([FromRoute] int bookId)
    {
        //TODO Проверка на то, что пользователь является автором книги

        var books = await GetBooks();

        if (books == null)
            Redirect("/profile");

        var book = await booksBL.Get(bookId);

        if (book?.BookId == null)
            return Redirect("/mybooks");


        //если пытается зайти на чужую книгу по id
        if (!Enumerable.Any(books, book => book.BookId == bookId))
            return Redirect("/mybooks");

        return View("Book", BookMapper.ModelToViewModel(book));
    }

    [HttpPost]
    [Route("/mybooks/savesettings")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SaveSettings()
    {
        return Ok();
    }

    [HttpPost]
    [Route("/mybooks/savechapters")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SaveChapters()
    {
        return Ok();
    }

    private async Task<List<BookModel>?> GetBooks()
    {
        var profile = await currentUser.GetProfile();

        if (profile.ProfileId == null)
            return null;

        var books = await booksBL.GetUserBooks((int)profile.ProfileId!);
        return books.ToList();
    }

}
