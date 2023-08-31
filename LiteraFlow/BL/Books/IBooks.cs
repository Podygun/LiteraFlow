namespace LiteraFlow.Web.BL.Books;

public interface IBooks
{
    Task<int?> AddChapterAsync(ChapterModel chapter);
    Task<int?> CreateAsync(BookModel book, int profileId);
    Task DeleteAsync(int bookId);
    Task DeleteChapterAsync(ChapterModel chapter);
    Task<IList<BookModel>> GetUserBooks(int userId);
    Task<BookModel> Get(int userId);
    Task<int?> UpdateChapterAsync(ChapterModel chapter);
    Task<List<ChapterModel>> GetChaptersAsync(int bookId);
    Task<string> GetChapterText(int chapterId);
    Task<int?> UpdateOrCreateChapterAsync(ChapterModel chapter);
}