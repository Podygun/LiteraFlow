namespace LiteraFlowTest;


public class BooksTest : BaseTest
{
    public static int PROFILE_ID = 4;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateBookWithoutChaptersTest()
    {
        BookModel model = new()
        {
            Title = "Test title",
            TypeId = 1,
            GenreId = 1,
            WhoCanComment = 1,
            WhoCanDownload = 1,
            WhoCanWatch = 1,
            StatusId = 1
        };
        int? bookId = await booksDAL.CreateAsync(model, PROFILE_ID);



        Assert.That(bookId, Is.GreaterThan(0));
    }


    [Test]
    public async Task CreateBookWithChaptersTest()
    {
        BookModel model = new()
        {
            Title = "Test title",
            TypeId = 1,
            GenreId = 1,
            WhoCanComment = 1,
            WhoCanDownload = 1,
            WhoCanWatch = 1,
            StatusId = 1
        };
        int? bookId = await booksDAL.CreateAsync(model, PROFILE_ID);

        ChapterModel chapter = new()
        {
            Title = "Test title",
            Text = "Lorem ispum. Lorem ispum! Lorem ispum? Lorem ispum, yes",
        };
        int? chapterId = await chaptersDAL.AddAsync((int)bookId, chapter);

        Assert.That(chapterId, Is.Not.Null);
    }



    [Test]
    public async Task UpdateChapterTest()
    {

        //создание книги
        BookModel model = new()
        {
            Title = "Test title",
            TypeId = 1,
            GenreId = 1,
            WhoCanComment = 1,
            WhoCanDownload = 1,
            WhoCanWatch = 1,
            StatusId = 1
        };
        int? bookId = await booksDAL.CreateAsync(model, PROFILE_ID);

        //создание главы
        ChapterModel chapter = new()
        {
            Title = "Test title",
            Text = "Lorem ispum. Lorem ispum! Lorem ispum? Lorem ispum, yes",
        };
        int? chapterId = await chaptersDAL.AddAsync((int)bookId, chapter);

        //обновление главы
        chapter = new()
        {
            Text = "Not Lorem",
        };
        chapterId = await chaptersDAL.UpdateAsync(chapter);

        var chapter = await chaptersDAL.GetAsync(bookId);

    }
}
