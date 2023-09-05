using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.Middleware;
using LiteraFlow.Web.Services;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;


namespace LiteraFlow.Web.Controllers;

[UserAuthorized]
[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.None, NoStore = true)]
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

    public async Task<IActionResult> Details(int id, [FromServices] ICacheService cache)
    {
        if (!await ValidateBook(id))
            return BadRequest("Ошибка загрузки");
        
        var book = await booksBL.Get(id);

        // Load chapters
        var chapters = await booksBL.GetChaptersAsync(id);

        // Add template chapter
        if (chapters.Count == 0) chapters.Add(new()
        {
            Title = "Глава 1",
            SerialNumber = 1,
            BookId = id,
        });

        cache.Set(CacheConstants.BOOK_CHAPTERS, chapters, 3600);

        
        var viewModel = new BookAndChaptersViewModel 
        { 
            Book = BookMapper.ModelToViewModel(book), 
            Chapters = ChapterMapper.ModelToViewModel(chapters)
        };


        return View(viewModel);
    }


    

    [HttpPost]
    public async Task<IActionResult> GetChapterText(int? chapterId, int bookId, [FromServices] ICacheService cache)
    {
        if (!await ValidateBook(bookId))
            return BadRequest("Ошибка загрузки");

        // null в случае только что созданной главы книги
        if (chapterId == null)
            return Json(String.Empty);


        List<ChapterModel>? chapters = cache.GetOrNull<List<ChapterModel>?>(CacheConstants.BOOK_CHAPTERS);
        if(chapters == null)
        {
            var chaptersFromDb = await booksBL.GetChaptersAsync(bookId);
            cache.Remove(CacheConstants.BOOK_CHAPTERS);
            cache.Set(CacheConstants.BOOK_CHAPTERS, chaptersFromDb, 3600);
        }

        // Если глава из запроса пользователя не имеется в книге 
        if (!Enumerable.Any(chapters, c => c.ChapterId == chapterId))
            return BadRequest("Ошибка загрузки");

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
        //TODO Check that user have this book and chapterid
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

        var books = await booksBL.GetUserBooks((int)profile.ProfileId);
        return books;
    }

    /// <summary>
    /// Сверяет id книги и соответствия профилю пользователя
    /// </summary>
    /// <returns>Redirects</returns>
    private async Task<bool> ValidateBook(int bookId)
    {
        var books = await GetBooks();

        if (books == null)
            return false;

        var book = await booksBL.Get(bookId);

        if (book?.BookId == null)
            return false;


        //если пытается зайти на чужую книгу по id
        if (!Enumerable.Any(books, book => book.BookId == bookId))
            return false;

        return true;
    }



}
