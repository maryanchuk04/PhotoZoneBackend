namespace PhotoZone.Core.IServices;

public interface ISubscribtionService
{
    Task AddNewSubscribtion(Guid myId,Guid subscribtioId);

    Task RemoveSubscribtion(Guid myId, Guid subscribtioId);

    List<Guid> GetAllSubscribtion(Guid userId);
}