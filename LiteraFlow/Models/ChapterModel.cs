namespace LiteraFlow.Web.Models;

public class ChapterModel
{
    public int? ChapterId { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; } 
    public int BookId { get; set; }
    public int SerialNumber { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int? AmountLetters { get; set; }
}
