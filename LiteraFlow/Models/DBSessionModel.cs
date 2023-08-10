namespace LiteraFlow.Web.Models;


/// <summary>
/// Изначально сессия анонимна: UserId = null
/// </summary>
public class DBSessionModel
{
    public Guid DbSessionId { get; set; }
    public string? SessionData { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastEntry { get; set; }
    public int? UserId { get; set; }
}
 