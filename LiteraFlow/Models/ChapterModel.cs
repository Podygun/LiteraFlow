namespace LiteraFlow.Web.Models;

public class ChapterModel
{
    public int ChapterId { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public int BookId { get; set; }
    public int SerialNumber { get; set; }
    public DateTime UpdatedOn { get; set; }
}
