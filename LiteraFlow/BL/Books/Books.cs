using LiteraFlow.Web.DAL.Books;
using LiteraFlow.Web.Services;


namespace LiteraFlow.Web.BL.Books;

public class Books : IBooks
{
    private readonly IBooksDAL booksDAL;
    private readonly IChaptersDAL chaptersDAL;
    private readonly ICacheService cache;

    public Books(IBooksDAL booksDAL, IChaptersDAL chaptersDAL, ICacheService cache)
    {
        this.booksDAL = booksDAL;
        this.chaptersDAL = chaptersDAL;
        this.cache = cache;
    }

    public async Task<int?> CreateAsync(BookModel book, int profileId)
    { 
        int? bookId = await booksDAL.CreateAsync(book, profileId);
        return bookId;
    }

    public async Task<List<ChapterModel>> GetChaptersAsync(int bookId)
    {
        var chapters = await chaptersDAL.GetAsync(bookId);
        return chapters;
    }
    
    [Obsolete("Use only in tests")]
    public async Task<int?> AddChapterAsync(ChapterModel chapter)
    {
        return await chaptersDAL.AddAsync(chapter);
    }

    [Obsolete("Use only in tests")]
    public async Task<int?> UpdateChapterAsync(ChapterModel chapter)
    {
        return await chaptersDAL.UpdateAsync(chapter);
    }

    public async Task<int?> UpdateOrCreateChapterAsync(ChapterModel chapter)
    {
        if (chapter.ChapterId == null)
            return await AddChapterAsync(chapter);
        return await UpdateChapterAsync(chapter);  
    }

    public async Task DeleteChapterAsync(ChapterModel chapter)
    {
        await chaptersDAL.UpdateAsync(chapter);
    }

    public async Task DeleteAsync(int bookId)
    {
        await booksDAL.DeleteAsync(bookId);
    }

    public async Task<List<BookModel>> GetUserBooks(int profileId)
    {
        var books = cache.GetOrNull<List<BookModel>>(CacheConstants.USER_BOOKS);
        if(books == null)
        {
            books = await booksDAL.GetUserBooks(profileId);
            cache.Set(CacheConstants.USER_BOOKS, books, 3600);
        }           
        return books;
    }

    //TODO WTF method?
    public async Task<BookModel> Get(int bookId)
    {
        return await booksDAL.GetAsync(bookId);
    }

    public async Task<string> GetChapterText(int chapterId)
    {
        return await chaptersDAL.GetTextAsync(chapterId);
    }


}

