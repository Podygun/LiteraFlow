namespace LiteraFlow.Web.Models;

public class ProfileModel
{
    public int? ProfileId { get; set; } = null;
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? ProfileImage { get; set; }   
    public string? About { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateBirth { get; set; }
}

