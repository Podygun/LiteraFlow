
namespace LiteraFlow.Web.ViewModels;

public class ProfileViewModel
{
    
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string? FullName { get; set; }

    [MaxLength(7)]
    public string? Gender { get; set; }

    public string? ProfileImage { get; set; }

    public DateTime? DateBirth { get; set; }

    [MaxLength(512)]
    public string? About { get; set; }
}
