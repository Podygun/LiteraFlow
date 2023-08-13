namespace LiteraFlow.Web.ViewModels;

public class ChapterViewModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(64)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(250_000)]
    public string Text { get; set; } = null!;
}
