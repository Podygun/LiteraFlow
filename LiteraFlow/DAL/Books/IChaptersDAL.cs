namespace LiteraFlow.Web.DAL.Books
{
    public interface IChaptersDAL
    {
        public Task<int?> AddAsync(int bookId, ChapterModel chapter);

        public Task AddRangeAsync(int bookId, IEnumerable<ChapterModel> chapters);

        public Task<int> UpdateAsync(ChapterModel chapter);
    }
}
