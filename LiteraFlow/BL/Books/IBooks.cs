namespace LiteraFlow.Web.BL.Books;

public interface IBooks
{
    Task<List<BookModel>> GetUserBooks(int userId);
    Task<BookModel> Get(int bookId);
    Task<int?> CreateAsync(BookModel book, int profileId);
    Task UpdateAsync(BookModel book);
    Task UpdateImageAsync(string imgPath, int bookId);
    Task DeleteAsync(int bookId);
   

    Task<List<ChapterModel>> GetChaptersAsync(int bookId);
    Task<string> GetChapterText(int chapterId);
    Task<int?> AddChapterAsync(ChapterModel chapter);
    Task<int?> UpdateChapterAsync(ChapterModel chapter);
    Task<int?> UpdateOrCreateChapterAsync(ChapterModel chapter);
    Task DeleteChapterAsync(ChapterModel chapter);    
    
}