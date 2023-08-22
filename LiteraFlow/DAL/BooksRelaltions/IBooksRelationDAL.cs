namespace LiteraFlow.Web.DAL.BooksRelaltions
{
    public interface IBooksRelationDAL
    {
        Task<List<BookGenreModel>> GetBookGenres();
        Task<List<BookStatusModel>> GetBookStatuses();
        Task<List<TagModel>> GetBookTags();
        Task<List<BookTypeModel>> GetBookTypes();
        Task<List<PermissionModel>> GetPermissions();
    }
}