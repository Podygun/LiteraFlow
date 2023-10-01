using LiteraFlow.Web.BL.Profiles;

namespace LiteraFlow.Web.DAL.Books;


public class BooksDAL : IBooksDAL
{
    public async Task<int?> CreateAsync(BookModel model, int profileId)
    {
        int? bookId = await DBHelper.AddBookWithTransactionAsync(
            @"insert into Books 
            (title, typeid, genreid, authornote, description, isadultcontent, createdon, whocanwatch, whocandownload, whocancomment, amountunlockedchapters, bookimage, price, statusid)
            values 
            (@title, @typeid, @genreid, @authornote, @description, @isadultcontent, NOW(), @whocanwatch, @whocandownload, @whocancomment, @amountunlockedchapters, @bookimage, @price, @statusid) returning bookid",
            model, 

            @"insert into BooksAuthors(bookid, profileid) values (@bookid, @profileid)", 
            profileId);

        return bookId;  
    }

    public async Task UpdateAsync(BookModel model)
    {
        await DBHelper.ExecuteAsync(
            @"update Books set 
                title=@title, 
                typeid=@typeid, 
                genreid=@genreid, 
                authornote=@authornote, 
                description=@description, 
                isadultcontent=@isadultcontent, 
                whocanwatch=@whocanwatch, 
                whocandownload=@whocandownload, 
                whocancomment=@whocancomment, 
                amountunlockedchapters=@amountunlockedchapters, 
                amountletters=@amountletters, 
                bookimage=@bookimage, 
                price=@price, 
                statusid=@statusid
                where bookId=@bookid", 
            model);
    }

    public async Task<List<BookModel>> GetUserBooks(int profileId)
    {
        return await DBHelper.QueryCollectionAsync<BookModel>(
            @"select * from Books as b
              join booksauthors as ba on b.bookid = ba.bookid
              where ba.profileid = @profileId", 
            new { profileId = profileId }) ?? new List<BookModel>();
    }

    public async Task DeleteAsync(int id)=>
        await DBHelper.ExecuteAsync(
            "delete from books where bookid = @bookid",
            new { bookid = id});
    

    public async Task<BookModel> GetAsync(int bookId)
    {
        return await DBHelper.QueryScalarAsync<BookModel>(
            @"select * from Books
              where BookId = @id",
            new { id = bookId }) ?? new BookModel();
    }

    public Task<IEnumerable<BookModel>> SearchAsync(int genreId = 0, int bookType = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookModel>> SearchAsync(string title)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateImageAsync(string imgPath, int bookId)
    {
        await DBHelper.ExecuteAsync(
            "update books set bookimage=@img where bookid=@id", 
            new { id = bookId, img = imgPath });
    }
}
