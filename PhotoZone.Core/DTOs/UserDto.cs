using PhotoZone.Entities;
using PhotoZone.Enums;

namespace PhotoZone.Core.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public int? Age { get; set; }

    public string? Hobby { get; set; }

    public string? Avatar { get; set; }

    public DateTime Birthday { get; set; }

    public Gender? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Subscribers> Subscribes { get; set; }

    public virtual ICollection<Subscribtions> Subscribtions { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<PlaceDto> Places { get; set; }

    public string? InstLink { get; set; }

    public string? TikTokLink { get; set; }

    public string? FacebookLink { get; set; }

    public string? GitHubLink { get; set; }
}