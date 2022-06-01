using PhotoZone.Enums;

namespace PhotoZone.ViewModels;

public class UserViewModel
{
    public string UserName { get; set; }

    public string FullName { get; set; }

    public string Avatar { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public string Location { get; set; }

    public int SubsctibersCount { get; set; }

    public int SubscriptionCount { get; set; }

    public DateTime Birthday { get; set; }

    public string Hobby { get; set; }

    public string? InstLink { get; set; }

    public string? TikTokLink { get; set; }

    public string? FacebookLink { get; set; }

    public string? GitHubLink { get; set; }

    public List<SubscribeViewModel> Subscribers { get; set; }

    public List<SubscribeViewModel> Subscriptions { get; set; }

}