namespace LiteraFlow.Web.ViewModels;

public class BookViewModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Title { get; set; } = null!;

    [Required]
    public int TypeId { get; set; }

    [Required]
    public int GenreId { get; set; }

    [Required]
    public int StatusId { get; set; } = 1;

    [MaxLength(500)]
    public string? Note { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }


    public bool IsAdultContent { get; set; } = false;


    public int WhoCanWatch { get; set; } = 1;
    public int WhoCanDownload { get; set; } = 1;
    public int WhoCanComment { get; set; } = 1;

    public int? AmountUnlockedChapters { get; set; } = null;
    public string? BookImage { get; set; } = null;
    public double? Price { get; set; } = null;
}
