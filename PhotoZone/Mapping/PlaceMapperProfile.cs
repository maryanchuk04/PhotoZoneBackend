using System.Security;
using AutoMapper;
using PhotoZone.Core.DTOs;
using PhotoZone.Entities;
using PhotoZone.ViewModels;

namespace PhotoZone.Mapping;

public class PlaceMapperProfile : Profile
{
    public PlaceMapperProfile()
    {
        CreateMap<Place, PlaceDto>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImages(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage))
            .ForMember(dest => dest.Comments, opts => opts.MapFrom(src => src.Comments));

        CreateMap<PlaceViewModel, PlaceDto>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImages(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage));

        CreateMap<PlaceDto, Place>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocations(src)))
            .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImagess(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage));

        CreateMap<PlaceDto, PlaceViewModel>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
            .ForMember(dest => dest.Owner, opts => opts.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImages(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage));
    }

    private static LocationDto MapLocation(PlaceViewModel placeViewModel)
    {
        if (placeViewModel.Location != null)
        {
            return new LocationDto
            {
                Id = Guid.NewGuid(),
                LocationName = placeViewModel.Location.LocationString,
                Latitude = placeViewModel.Location.Latitude,
                Longitude = placeViewModel.Location.Longitude
            };
        }

        return null;
    }

    private static ICollection<ImagesDto> MapImages(PlaceViewModel placeViewModel)
    {
        if (placeViewModel.Images != null)
        {
            ICollection<ImagesDto> imagesDtos = new List<ImagesDto>();
            foreach (var image in placeViewModel.Images)
            {
                imagesDtos.Add(new ImagesDto()
                {
                    Id = Guid.NewGuid(),
                    Image = image
                });
            }

            return imagesDtos;
        }

        return null;
    }

    private static List<string> MapImages(PlaceDto placeDto)
    {
        if (placeDto.Images != null)
        {
            List<string> images = new List<string>();

            foreach (var image in placeDto.Images)
            {
                images.Add(image.Image);
            }

            return images;
        }

        return null;
    }

    private static LocationViewModel MapLocation(PlaceDto placeDto)
    {
        if (placeDto.Location != null)
        {
            return new LocationViewModel()
            {

                LocationString = placeDto.Location.LocationName,
                Latitude = placeDto.Location.Latitude,
                Longitude = placeDto.Location.Longitude
            };
        }

        return null;
    }

    private static ICollection<ImagesDto> MapImages(Place place)
    {
        if (place.Images != null)
        {
            ICollection<ImagesDto> images = new List<ImagesDto>();
            foreach (var image in place.Images)
            {
                images.Add( new ImagesDto()
                {
                    Id = image.Id,
                    Image = image.Image
                });
            }

            return images;
        }

        return null;
    }

    private static LocationDto MapLocation(Place place)
    {
        if (place.Location != null)
        {
            return new LocationDto
            {
                Id = place.LocationId.Value,
                LocationName = place.Location.LocationName,
                Latitude = place.Location.Latitude,
                Longitude = place.Location.Longitude
            };
        }

        return null;
    }

    private static List<string> MapComments(PlaceDto placeDto)
    {
        if (placeDto.Comments.Count == 0)
        {
            return null;
        }

        List<string> comments = new List<string>();

        foreach (var comment in placeDto.Comments)
        {
            comments.Add(comment.CommentText);
        }

        return comments;
    }

    public static Location MapLocations(PlaceDto placeDto)
    {
        if (placeDto.Location != null)
        {
            return new Location()
            {
                Id = placeDto.Location.Id,
                LocationName = placeDto.Location.LocationName,
                Latitude = placeDto.Location.Latitude,
                Longitude = placeDto.Location.Longitude
            };
        }

        return null;
    }

    private static ICollection<Images> MapImagess(PlaceDto placeDto)
    {
        if (placeDto.Images != null)
        {
            ICollection<Images> imagesDtos = new List<Images>();
            foreach (var image in placeDto.Images)
            {
                imagesDtos.Add(new Images()
                {
                    Id = Guid.NewGuid(),
                    Image  = image.Image
                });
            }

            return imagesDtos;
        }

        return null;
    }

}