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

    public async Task AddChapterAsync(int bookId, ChapterModel chapter)
    {
        await chaptersDAL.AddAsync(bookId, chapter);
    }

    public async Task UpdateChapterAsync(ChapterModel chapter)
    {
        await chaptersDAL.UpdateAsync(chapter);
    }

    public async Task DeleteChapterAsync(ChapterModel chapter)
    {
        await chaptersDAL.UpdateAsync(chapter);
    }

    public async Task DeleteAsync(int bookId)
    {
        await booksDAL.DeleteAsync(bookId);
    }

    public async Task<IList<BookModel>> GetUserBooks(int userId)
    {
        return await booksDAL.GetUserBooks(userId);
    }
}

