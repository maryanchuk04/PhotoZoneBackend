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

    public Guid AddNewPlace(PlaceDto placeDto)
    {
        placeDto.Id = Guid.NewGuid();
        var place = Mapper.Map<PlaceDto, Place>(placeDto);

        if (place == null)
            throw new PhotoZoneException("Unhandlered Exception in Insert new Places");

        Insert(place);

        Context.SaveChanges();

        return place.Id;
    }

    public void DeletePlace(Guid placeId)
    {
        var deletePlace = Context.Places
            .Include(x => x.Location)
            .Include(x=>x.Images)
            .Include(x=>x.Comments)
            .FirstOrDefault(x => x.Id == placeId);
        if (deletePlace != null)
        {
            Delete(deletePlace);
            Context.SaveChanges();
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
        var places = Context.Places
            .Include(x=>x.Images)
            .Include(x=>x.Location)
            .Include(x=>x.Comments)
            .ToList();

        List<PlaceDto> resultList = new List<PlaceDto>();

        foreach (var item in places)
        {
            resultList.Add(Mapper.Map<PlaceDto>(item));
        }

        return resultList;
    }

    public List<PlaceDto> GetAllPlacesByUserId(Guid id)
    {
        var places = Context.Places.Where(x=>x.OwnerId == id)
            .Include(x=>x.Images)
            .Include(x=>x.Location)
            .ToList();

        return Mapper.Map<List<PlaceDto>>(places);
    }

    public List<PlaceDto> SearchPlaces(string searchText)
    {
        var places = Context.Places
            .Include(x=>x.Images)
            .Include(x=>x.Location)
            .ToList();

        var res = places.FindAll(x => x.Title.Contains(searchText));

        return Mapper.Map<List<Place>, List<PlaceDto>>(res);
    }
}