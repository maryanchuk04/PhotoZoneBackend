using PhotoZone.Core.DTOs;

namespace PhotoZone.Core.IServices;

public interface IPlaceService
{
    double MarkPlace(double mark);

    void AddNewPlace(PlaceDto placeDto);

    void DeletePlace(Guid placeId);

    PlaceDto GetPlace(Guid placeId);

    List<PlaceDto> GetAllPlaces();

}