using AutoMapper;
using PhotoZone.Core.DTOs;
using PhotoZone.Entities;
using PhotoZone.ViewModels;

namespace PhotoZone.Mapping;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Avatar, opts => opts.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Age, opts => opts.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Hobby, opts => opts.MapFrom(src => src.Hobby))
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.Location))
            .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
            .ForMember(dest => dest.InstLink, opts => opts.MapFrom(src => src.InstLink))
            .ForMember(dest => dest.FacebookLink, opts => opts.MapFrom(src => src.FacebookLink))
            .ForMember(dest => dest.GitHubLink, opts => opts.MapFrom(src => src.GitHubLink))
            .ForMember(dest => dest.TikTokLink, opts => opts.MapFrom(src => src.TikTokLink))
            .ForMember(dest => dest.Subscribtions, opts => opts.MapFrom(src=>src.Subscrioptions))
            .ForMember(dest => dest.Subscribes, opts => opts.MapFrom(src=>src.Subscribes));

        CreateMap<UserDto, UserViewModel>()
            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opts => opts.MapFrom(src => CountAge(src.Birthday)))
            .ForMember(dest => dest.Subscribers, opts => opts.MapFrom(src => MapSubscribers(src)))
            .ForMember(dest => dest.Subscriptions, opts => opts.MapFrom(src => MapSubscriptions(src)))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.SubscriptionCount, opts => opts.MapFrom(src => src.Subscribtions.Count))
            .ForMember(dest => dest.SubsctibersCount, opts => opts.MapFrom(src => src.Subscribes.Count))
            .ForMember(dest => dest.Avatar, opts => opts.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.InstLink, opts => opts.MapFrom(src => src.InstLink))
            .ForMember(dest => dest.FacebookLink, opts => opts.MapFrom(src => src.FacebookLink))
            .ForMember(dest => dest.GitHubLink, opts => opts.MapFrom(src => src.GitHubLink))
            .ForMember(dest => dest.TikTokLink, opts => opts.MapFrom(src => src.TikTokLink));


        CreateMap<UserDto, UserShortInfoViewModel>()
            .ForMember(dest => dest.Avatar, opts => opts.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opts => opts.MapFrom(src => CountAge(src.Birthday)))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.Location));

    }

    private int CountAge(DateTime birthday)
    {
        if (birthday != null)
        {
            int age = 0;
            age = DateTime.Now.Subtract(birthday).Days;
            age = age / 365;
            return age;
        }

        return 0;
    }

    private List<SubscribeViewModel> MapSubscribers(UserDto userDto)
    {
        if (userDto.Subscribes.Count==0)
        {
            return null;
        }

        List<SubscribeViewModel> subscribeViewModels = new List<SubscribeViewModel>();

        foreach (var subscribe in userDto.Subscribes)
        {
            subscribeViewModels.Add(new SubscribeViewModel()
            {
                SubscribeId = subscribe.SubscriberId
            });
        }

        return subscribeViewModels;
    }


    private List<SubscribeViewModel> MapSubscriptions(UserDto userDto)
    {
        if (userDto.Subscribtions.Count==0)
        {
            return null;
        }

        List<SubscribeViewModel> subscribeViewModels = new List<SubscribeViewModel>();

        foreach (var subscribe in userDto.Subscribtions)
        {
            subscribeViewModels.Add(new SubscribeViewModel()
            {
                SubscribeId = subscribe.SubscribtionId
            });
        }

        return subscribeViewModels;
    }

}