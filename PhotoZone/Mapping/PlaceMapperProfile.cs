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
            .ForMember(dest => dest.Owner, opts => opts.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImages(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage));

        CreateMap<PlaceViewModel, PlaceDto>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
            .ForMember(dest => dest.Owner, opts => opts.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => MapImages(src)))
            .ForMember(dest => dest.MainImage, opts => opts.MapFrom(src => src.MainImage));

        CreateMap<PlaceDto, Place>()
            .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocation(src)))
            .ForMember(dest => dest.Owner, opts => opts.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.Rate, opts => opts.MapFrom(src => src.Rate))
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

    private static ICollection<Images> MapImages(PlaceDto placeDto)
    {
        if (placeDto.Images != null)
        {
            ICollection<Images> images = new List<Images>();
            foreach (var image in placeDto.Images)
            {
                images.Add( new Images()
                {
                    PlaceId = placeDto.Id,
                    Id = image.Id,
                    Image = image.Image
                });
            }

            return images;
        }

        return null;
    }

    private static Location MapLocation(PlaceDto placeDto)
    {
        if (placeDto.Location != null)
        {
            return new Location
            {
                Id = Guid.NewGuid(),
                LocationName = placeDto.Location.LocationName,
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
}