namespace LiteraFlow.Web.ViewModels;

public class BookViewModel
{
    public int? BookId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Title { get; set; } = null!;

    [Required]
    public int TypeId { get; set; }

    [Required]
    public int GenreId { get; set; } = 1;

    [Required]
    public int StatusId { get; set; } = 1;

    [MaxLength(500)]
    public string? AuthorNote { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsAdultContent { get; set; } = false;

    public int WhoCanWatch { get; set; } = 1;
    public int WhoCanDownload { get; set; } = 1;
    public int WhoCanComment { get; set; } = 1;

    public int AmountUnlockedChapters { get; set; }
    public string? BookImage { get; set; }
    public double? Price { get; set; }
}
