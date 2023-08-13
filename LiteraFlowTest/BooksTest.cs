namespace LiteraFlowTest;


public class BooksTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateEmptyBookTest()
    {
        BookModel model = new()
        {
            Title = "Test title"
        };
        int? bookId = await booksDAL.CreateAsync(model);
        Assert.That(bookId, Is.GreaterThan(0));
    }


    [Test]
    public async Task CreateBookWithChaptersTest()
    {
        ChapterModel model = new()
        {
            Title = "Test title",
            Text = "Lorem ispum. Lorem ispum! Lorem ispum? Lorem ispum, yes",
        };
        int? chapterId = await chaptersDAL.AddAsync(1, model);
        Assert.That(chapterId, Is.Not.Null);
    }



    [Test]
    public async Task UpdateChapterTest()
    {

    }
}
