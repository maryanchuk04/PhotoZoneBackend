namespace PhotoZone.Core.IServices;

public interface IImagesService
{
    void AddImageToPlace(Guid id, string image);
}