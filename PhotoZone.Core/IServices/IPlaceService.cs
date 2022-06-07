using PhotoZone.Core.DTOs;

namespace PhotoZone.Core.IServices;

public interface IPlaceService
{
    double MarkPlace(double mark);

    Guid AddNewPlace(PlaceDto placeDto);

    void DeletePlace(Guid placeId);

    PlaceDto GetPlace(Guid placeId);

    List<PlaceDto> GetAllPlaces();

    List<PlaceDto> GetAllPlacesByUserId(Guid id);


}