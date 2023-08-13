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
