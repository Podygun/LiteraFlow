using System.Reflection;

namespace LiteraFlow.Web.DAL.Books;

public class BooksDAL : IBooksDAL
{
    public async Task<int?> CreateAsync(BookModel model)
    {
        var result = await DBHelper.ExecuteScalarAsync<int?>(
            @"insert into Users (Email,Password,Salt,Status) 
              values (@email,@password,@salt,@status) returning UserId", model);
        return result;
    }

    public async Task<IList<BookModel>> GetUserBooks(int userId)
    {
        return await DBHelper.QueryCollectionAsync<BookModel>(
            @"select * from Books where userid = @userid", 
            new {userid = userId}) ?? new List<BookModel>();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BookModel> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookModel>> SearchAsync(int genreId = 0, int bookType = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookModel>> SearchAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Task<int?> UpdateAsync(BookModel model)
    {
        throw new NotImplementedException();
    }
}
