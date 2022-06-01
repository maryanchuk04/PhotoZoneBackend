using AutoMapper;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;

namespace PhotoZone.Services;

public class SubscribtionService : BaseService<Subscribtions> , ISubscribtionService
{
    public SubscribtionService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task AddNewSubscribtion(Guid myId,Guid subscribtionId)
    {
        var newSubscribes = new Subscribtions
        {
            Id = Guid.NewGuid(),
            UserId = myId,
            SubscribtionId = subscribtionId
        };

        Insert(newSubscribes);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveSubscribtion(Guid myId, Guid subscribtionId)
    {
        throw new NotImplementedException();
    }

    public List<Guid> GetAllSubscribtion(Guid userId)
    {
        var subscribtions = Context.Subscribtions.Where(x => x.UserId == userId).ToList();

        var listIds = new List<Guid>();

        foreach (var id in subscribtions)
        {
            listIds.Add(id.SubscribtionId);
        }

        return listIds;
    }
}