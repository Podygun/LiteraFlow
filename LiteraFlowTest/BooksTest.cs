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

        await booksDAL.DeleteAsync((int)bookId);
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
            BookId = (int)bookId,
            Text = "Lorem ispum. Lorem ispum! Lorem ispum? Lorem ispum, yes",
        };
        int? chapterId = await chaptersDAL.AddAsync(chapter);

        Assert.That(chapterId, Is.Not.Null);

        await booksDAL.DeleteAsync((int)bookId);
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
            BookId = (int)bookId
        };
        int? chapterId = await chaptersDAL.AddAsync(chapter);

        //обновление главы
        chapter = new()
        {
            ChapterId = chapterId,
            Title = "Test title 2",
            Text = "Not Lorem"
            
        };

        var chapterId_2 = await chaptersDAL.UpdateAsync(chapter);
        Assert.That(chapterId_2, Is.EqualTo(chapterId));

        string chapterText = await chaptersDAL.GetTextAsync((int)chapterId_2);

        
        Assert.That(chapterText, Is.EqualTo("Not Lorem"));

        await booksDAL.DeleteAsync((int)bookId);
    }
}
