namespace LiteraFlow.Web.ViewModels;

/// <summary>

/// Contains Permissions, Statuses, Tags, Genres and Book to Post

/// </summary>
public class BookSettingsViewModel
{
    public List<PermissionModel>? Permissions { get; set; } = null;
    public List<BookStatusModel>? Statuses { get; set; } = null;
    public List<TagModel>? Tags{ get; set; } = null;
    public List<BookGenreModel>? Genres{ get; set; } = null;
    public BookViewModel? Book { get; set; } = null;
}
