namespace LiteraFlow.Web.DAL.Books;


public interface IBooksDAL
{
    Task<int?> CreateAsync(BookModel model);
    Task<int?> UpdateAsync(BookModel model);
    Task DeleteAsync(int id);

    Task<BookModel> GetAsync(int id);
    Task<IList<BookModel>> GetUserBooks(int userId);

    Task<IEnumerable<BookModel>> SearchAsync(int genreId = 0, int bookType = 0);
    Task<IEnumerable<BookModel>> SearchAsync(string title);

}
