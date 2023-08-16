
namespace LiteraFlow.Web.ViewModels;

public class ProfileViewModel
{
    [Key]
    public int? ProfileId { get; set; }

    public int? UserId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string? FullName { get; set; } = null!;

    [MaxLength(7)]
    public string? Gender { get; set; }

    public string? ProfileImage { get; set; }

    public DateTime? DateBirth { get; set; }

    [MaxLength(512)]
    public string? About { get; set; }
}
