using AutoMapper;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.Entities;

namespace PhotoZone.Services;

public class SubscribesService : BaseService<Subscribers> , ISubscribesService
{
    public SubscribesService(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task AddSubscriberForUser(Guid myId, Guid subscriberId)
    {
        var newSubscribers = new Subscribers()
        {
            Id = Guid.NewGuid(),
            UserId = subscriberId,
            SubscriberId = myId
        };

        Insert(newSubscribers);

        await Context.SaveChangesAsync();
    }

    public async Task RemoveSubscriberFromUser(Guid myId, Guid subsctiberId)
    {
        throw new NotImplementedException();
    }

    public List<Guid> GetAlSubscribers(Guid userId)
    {
        var subscribers = Context.Subscribes.Where(x => x.SubscriberId == userId).ToList();

        var listIds = new List<Guid>();

        foreach (var id in subscribers)
        {
            listIds.Add(id.UserId);
        }

        return listIds;
    }


}