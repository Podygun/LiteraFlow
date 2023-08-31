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

    public async Task<IActionResult> Details(int id)
    {
        #region Validation
        var books = await GetBooks();

        if (books == null)
            Redirect("/profile");

        var book = await booksBL.Get(id);

        if (book?.BookId == null)
            return Redirect("/mybooks");


        //если пытается зайти на чужую книгу по id
        if (!Enumerable.Any(books, book => book.BookId == id))
            return Redirect("/mybooks");
        #endregion

        // Load chapters
        var chapters = await booksBL.GetChaptersAsync(id);

        // Add template chapter
        if (chapters.Count == 0) chapters.Add(new()
        {
            Title = "Глава 1",
            SerialNumber = 1,
            BookId = (int)book.BookId,
        });

        
        var viewModel = new BookAndChaptersViewModel 
        { 
            Book = BookMapper.ModelToViewModel(book), 
            Chapters = ChapterMapper.ModelToViewModel(chapters)
        };


        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> GetChapterText(int? chapterId)
    {
        //TODO Maybe need to check that this book has this chapter
        // But i dont now book id here
        if (chapterId == null) 
            return Json(String.Empty);

        string txt = await booksBL.GetChapterText((int)chapterId);
        return Json(txt);
    }

    [HttpPost]
    [Route("/mybooks/book/savesettings")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> SaveSettings()
    {
        return Ok();
    }


    [HttpPost]

    public async Task<IActionResult> SaveChapter([FromBody] ChapterViewModel vm)
    {
        if(!ModelState.IsValid) 
            return View();


        ChapterModel model = new()
        {
            BookId = (int)vm.BookId,
            Title = vm.Title,
            Text = vm.Text,
            ChapterId = vm.ChapterId,
            AmountLetters = vm.Text.Length,
            SerialNumber = 1,
            UpdatedOn = DateTime.Now
        };

        int? id = await booksBL.UpdateOrCreateChapterAsync(model);

        if(id is null)
            return Problem();

        //TODO Update book's amount letter

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
