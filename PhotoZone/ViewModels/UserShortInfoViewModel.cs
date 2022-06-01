using PhotoZone.Enums;

namespace PhotoZone.ViewModels;

public class UserShortInfoViewModel
{
    public string UserName { get; set; }

    public string FullName { get; set; }

    public int Age { get; set; }

    public string Location { get; set; }

    public Gender Gender { get; set; }

    public string Avatar { get; set; }
}