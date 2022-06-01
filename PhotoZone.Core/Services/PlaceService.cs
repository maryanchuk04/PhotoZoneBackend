using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;
using PhotoZone.IServices;

namespace PhotoZone.Services;

public class PlaceService : BaseService<Place> , IPlaceService
{
    public PlaceService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public double MarkPlace(double mark)
    {
        throw new NotImplementedException();
    }

    public void AddNewPlace(PlaceDto placeDto)
    {
        placeDto.Id = Guid.NewGuid();
        var place = Mapper.Map<PlaceDto, Place>(placeDto);

        if (place == null)
            throw new PhotoZoneException("Unhandlered Exception in Insert new Places");

        Insert(place);

        Context.SaveChanges();
    }

    public void DeletePlace(Guid placeId)
    {
        var deletePlace = Context.Places
            .Include(x => x.Location)
            .FirstOrDefault(x => x.Id == placeId);
        if (deletePlace != null)
        {
            Delete(deletePlace);
        }
        else throw new PhotoZoneException("Error when you want delete place");
    }

    public PlaceDto GetPlace(Guid placeId)
    {
        var place = Context.Places.Include(x=>x.Images)
            .Include(x=>x.Location)
            .FirstOrDefault(x => x.Id == placeId);
        if(place != null)
            return Mapper.Map<Place,PlaceDto>(place);

        throw new PhotoZoneException("Not found");
    }

    public List<PlaceDto> GetAllPlaces()
    {
        var places = Context.Places.ToList();

        List<PlaceDto> resultList = new List<PlaceDto>();

        foreach (var item in places)
        {
            resultList.Add(Mapper.Map<PlaceDto>(item));
        }

        return resultList;
    }
}