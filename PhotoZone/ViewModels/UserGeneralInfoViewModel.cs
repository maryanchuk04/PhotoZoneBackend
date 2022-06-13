using PhotoZone.Enums;

namespace PhotoZone.ViewModels;

public class UserGeneralInfoViewModel
{
    public string UserName { get; set; }

    public string FullName { get; set; }

    public string Avatar { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; }

    public Gender Gender { get; set; }

    public string Location { get; set; }

    public DateTime Birthday { get; set; }

    public string Hobby { get; set; }

}