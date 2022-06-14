using AutoMapper;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;

namespace PhotoZone.Services;

public class ImagesService : BaseService<Images>, IImagesService
{
    public ImagesService(AppDbContext context, IMapper mapper = null) : base(context, mapper)
    {
    }

    public void AddImageToPlace(Guid id, string image)
    {
        Images img = new Images()
        {
            Id = Guid.NewGuid(),
            Image = image,
            PlaceId = id
        };

        Insert(img);
        Context.SaveChanges();
    }
}