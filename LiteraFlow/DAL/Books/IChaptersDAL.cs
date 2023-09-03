namespace LiteraFlow.Web.DAL.Books
{
    public interface IChaptersDAL
    {
        Task<int?> AddAsync(ChapterModel chapter);
        Task AddRangeAsync(int bookId, IEnumerable<ChapterModel> chapters);
        Task<string> GetTextAsync(int chapterId);
        Task<int?> UpdateAsync(ChapterModel chapter);
        Task<List<ChapterModel>>GetAsync(int bookId);
    }
}
