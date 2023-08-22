namespace LiteraFlow.Web.DAL.BooksRelaltions;

public class BooksRelationDAL : IBooksRelationDAL
{
    public async Task<List<BookTypeModel>> GetBookTypes() =>
        await DBHelper.QueryCollectionAsync<BookTypeModel>("select * from BookTypes");

    public async Task<List<BookGenreModel>> GetBookGenres() =>
        await DBHelper.QueryCollectionAsync<BookGenreModel>("select * from BookGenres");

    public async Task<List<TagModel>> GetBookTags() =>
        await DBHelper.QueryCollectionAsync<TagModel>("select * from Tags");

    public async Task<List<BookStatusModel>> GetBookStatuses() =>
        await DBHelper.QueryCollectionAsync<BookStatusModel>("select * from BookStatuses");

    public async Task<List<PermissionModel>> GetPermissions() =>
        await DBHelper.QueryCollectionAsync<PermissionModel>("select * from PermissionTypes");
}
