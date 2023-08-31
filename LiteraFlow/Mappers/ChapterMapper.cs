
namespace LiteraFlow.Web.Mappers;

public static class ChapterMapper
{
    /// <summary>
    /// back to front
    /// </summary>
    public static ChapterViewModel ModelToViewModel(ChapterModel model)
    {
        ChapterViewModel vm = new()
        {
            ChapterId = model.ChapterId,
            SerialNumber = model.SerialNumber,
            Title = model.Title,
            Text = model.Text,
            BookId = model.BookId,
        };
        return vm;
    }

    /// <summary>
    /// front to back
    /// </summary>
    public static ChapterModel ViewModelToModel(ChapterViewModel vm)
    {
        ChapterModel m = new()
        {
            ChapterId = vm.ChapterId,
            SerialNumber = vm.SerialNumber,
            Title = vm.Title,
            Text = vm.Text,
            BookId = (int)vm.BookId
        };
        return m;
    }

    /// <summary>
    /// back to front [List]
    /// </summary>
    public static List<ChapterViewModel> ModelToViewModel(List<ChapterModel> model) =>
        model.Select(x => new ChapterViewModel
        {
            ChapterId = x.ChapterId,
            SerialNumber = x.SerialNumber,
            Title = x.Title,
            Text = x.Text,
            BookId = x.BookId,
        }).ToList();


    /// <summary>
    /// front to back [List]
    /// </summary>
    public static List<ChapterModel> ViewModelToModel(List<ChapterViewModel> vm) =>
         vm.Select(x => new ChapterModel
         {
             ChapterId = x.ChapterId,
             SerialNumber = x.SerialNumber,
             Title = x.Title,
             Text = x.Text,
             BookId = (int)x.BookId,
         }).ToList();

}