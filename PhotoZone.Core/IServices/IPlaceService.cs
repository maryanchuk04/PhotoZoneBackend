using PhotoZone.Core.DTOs;

namespace PhotoZone.Core.IServices;

public interface IPlaceService
{
    void MarkPlace(Guid id, double mark);

    Guid AddNewPlace(PlaceDto placeDto);

    void DeletePlace(Guid placeId);

    PlaceDto GetPlace(Guid placeId);

    List<PlaceDto> GetAllPlaces();

    List<PlaceDto> GetAllPlacesByUserId(Guid id);

    List<PlaceDto> SearchPlaces(string searchText);

    PlaceDto WriteComment(Guid id, string CommentText);
}