namespace LiteraFlow.Web.Models;


public class BookModel
{
    public int? BookId { get; set; }
    public string? Title { get; set; } = null!;
    public int TypeId { get; set; }
    public int GenreId { get; set; }
    public int StatusId { get; set; }
    public string? AuthorNote { get; set; }
    public string? Description { get; set; }
    public bool IsAdultContent { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int WhoCanWatch { get; set; }
    public int WhoCanDownload { get; set; }
    public int WhoCanComment { get; set; }
    public int AmountUnlockedChapters { get; set; }
    public string? BookImage { get; set; }
    public double? Price { get; set; }
    public int? AmountLetters { get; set; }
}
