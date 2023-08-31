using LiteraFlow.Web.DAL.Books;

namespace LiteraFlow.Web.BL.Books;

public class Books : IBooks
{
    private readonly IBooksDAL booksDAL;
    private readonly IChaptersDAL chaptersDAL;

    public Books(IBooksDAL booksDAL, IChaptersDAL chaptersDAL)
    {
        this.booksDAL = booksDAL;
        this.chaptersDAL = chaptersDAL;
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
        bool isExists = await chaptersDAL.IsExists(chapter.ChapterId);
        if (isExists)
            return await UpdateChapterAsync(chapter);
        return await AddChapterAsync(chapter);
        
    }

    public async Task DeleteChapterAsync(ChapterModel chapter)
    {
        await chaptersDAL.UpdateAsync(chapter);
    }

    public async Task DeleteAsync(int bookId)
    {
        await booksDAL.DeleteAsync(bookId);
    }

    public async Task<IList<BookModel>> GetUserBooks(int profileId)
    {
        return await booksDAL.GetUserBooks(profileId);
    }

    //TODO What is this method?
    public async Task<BookModel> Get(int userId)
    {
        return await booksDAL.GetAsync(userId);
    }

    public async Task<string> GetChapterText(int chapterId)
    {
        return await chaptersDAL.GetTextAsync(chapterId);
    }


}

