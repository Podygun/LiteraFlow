using System.Reflection;

namespace LiteraFlow.Web.Mappers;

public static class BookMapper
{
    /// <summary>
    /// back to front
    /// </summary>
    public static BookViewModel ModelToViewModel(BookModel model)
    {
        return new BookViewModel()
        {
            BookId = model.BookId,
            Title = model.Title,
            AmountUnlockedChapters = model.AmountUnlockedChapters,
            BookImage = model.BookImage,
            Description = model.Description,
            GenreId = model.GenreId,
            IsAdultContent = model.IsAdultContent,
            AuthorNote = model.AuthorNote,
            Price = model.Price,
            StatusId = model.StatusId,
            TypeId = model.TypeId,
            WhoCanComment = model.WhoCanComment,
            WhoCanDownload = model.WhoCanDownload,
            WhoCanWatch = model.WhoCanWatch   
        };
    }

    /// <summary>
    /// front to back
    /// </summary>
    public static BookModel ViewModelToModel(BookViewModel viewmodel)
    {
        return new BookModel()
        {
            BookId = viewmodel.BookId,
            Title = viewmodel.Title,
            AmountUnlockedChapters = viewmodel.AmountUnlockedChapters,
            BookImage = viewmodel.BookImage,
            Description = viewmodel.Description,
            GenreId = viewmodel.GenreId,
            IsAdultContent = viewmodel.IsAdultContent,
            AuthorNote = viewmodel.AuthorNote,
            Price = viewmodel.Price,
            StatusId = viewmodel.StatusId,
            TypeId = viewmodel.TypeId,
            WhoCanComment = viewmodel.WhoCanComment,
            WhoCanDownload = viewmodel.WhoCanDownload,
            WhoCanWatch = viewmodel.WhoCanWatch
        }; 
    }

    /// <summary>
    /// [List] back to front
    /// </summary>
    public static IEnumerable<BookViewModel> ModelToViewModel(IList<BookModel> modelList)
    {
        foreach (var model in modelList)
        {
            yield return new BookViewModel()
            {
                BookId = model.BookId,
                Title = model.Title,
                AmountUnlockedChapters = model.AmountUnlockedChapters,
                BookImage = model.BookImage,
                Description = model.Description,
                GenreId = model.GenreId,
                IsAdultContent = model.IsAdultContent,
                AuthorNote = model.AuthorNote,
                Price = model.Price,
                StatusId = model.StatusId,
                TypeId = model.TypeId,
                WhoCanComment = model.WhoCanComment,
                WhoCanDownload = model.WhoCanDownload,
                WhoCanWatch = model.WhoCanWatch
            };
        }
    }
}
