using System.Diagnostics.Contracts;

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
    public async Task UpdateBookSettingsTest()
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

        var bookFromDb = await booksDAL.GetAsync((int)bookId);

        
        bookFromDb.Title = "New Title";       
        bookFromDb.Description = "Description";
        bookFromDb.AuthorNote = "note";
        bookFromDb.AmountLetters = 1000;
        bookFromDb.Price = 1000.00d;
        bookFromDb.AmountUnlockedChapters = 5;
        bookFromDb.BookImage = "/path";
        bookFromDb.WhoCanComment = 2;
        bookFromDb.WhoCanDownload = 2;
        bookFromDb.WhoCanWatch = 2;
        bookFromDb.StatusId = 2;
        bookFromDb.GenreId = 2;
        bookFromDb.TypeId = 2;
        bookFromDb.IsAdultContent = true;

        await booksDAL.UpdateAsync(bookFromDb);

        //bookFromDbBeforeUpdating
        var b = await booksDAL.GetAsync((int)bookId);
        Assert.Multiple(() =>
        {
            Assert.That(b.Title, Is.EqualTo("New Title"));
            Assert.That(b.Description, Is.EqualTo("Description"));
            Assert.That(b.AuthorNote, Is.EqualTo("note"));
            Assert.That(b.AmountUnlockedChapters, Is.EqualTo(5));
            Assert.That(b.AmountLetters, Is.EqualTo(1000));
            Assert.That(b.BookImage, Is.EqualTo("/path"));
            Assert.That(b.WhoCanComment, Is.EqualTo(2));
            Assert.That(b.WhoCanDownload, Is.EqualTo(2));
            Assert.That(b.WhoCanWatch, Is.EqualTo(2));
            Assert.That(b.StatusId, Is.EqualTo(2));
            Assert.That(b.GenreId, Is.EqualTo(2));
            Assert.That(b.TypeId, Is.EqualTo(2));
            Assert.That(b.IsAdultContent, Is.EqualTo(true));
        });

        await booksDAL.DeleteAsync((int)bookId);
    }

    [Test]
    public async Task UpdateImageInBookTest()
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

        await booksDAL.UpdateImageAsync("newpath", (int)bookId);

        //bookFromDbBeforeUpdating
        var b = await booksDAL.GetAsync((int)bookId);
     
        Assert.That(b.BookImage, Is.EqualTo("newpath"));

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
