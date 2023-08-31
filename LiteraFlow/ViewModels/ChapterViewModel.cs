namespace LiteraFlow.Web.ViewModels;

public class ChapterViewModel
{
    public int? ChapterId { get; set; }

    [Required]
    [MaxLength(250_000)]
    public string? Text { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string? Title { get; set; } = null!;

    public int SerialNumber { get; set; }

    public int? BookId { get; set; }
}
