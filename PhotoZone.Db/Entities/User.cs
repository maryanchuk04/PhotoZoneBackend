using PhotoZone.Enums;

namespace PhotoZone.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string? FullName { get; set; }

    public int? Age { get; set; }

    public string? Hobby { get; set; }

    public string Avatar { get; set; } = "https://sbcf.fr/wp-content/uploads/2018/03/sbcf-default-avatar.png";

    public DateTime? Birthday { get; set; }

    public Gender? Gender { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Guid? SubscribesId { get; set; }

    public Guid? SubscribtionsId { get; set; }

    public virtual ICollection<Subscribers> Subscribes { get; set; }

    public virtual ICollection<Subscribtions> Subscrioptions { get; set; }

    public string? Location { get; set; }

    public string? InstLink { get; set; }

    public string? TikTokLink { get; set; }

    public string? FacebookLink { get; set; }

    public string? GitHubLink { get; set; }

}
