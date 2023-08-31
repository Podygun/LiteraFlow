namespace LiteraFlow.Web.ViewModels;

public class BookAndChaptersViewModel
{
    public BookViewModel Book { get; set; } = null!;
    public List<ChapterViewModel>? Chapters { get; set;}
}
