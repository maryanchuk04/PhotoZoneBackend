namespace PhotoZone.Core.IServices;

public interface ISubscribesService
{

    /// <summary>
    /// Підписався
    /// </summary>
    /// <param name="subscriberId"></param>
    Task AddSubscriberForUser(Guid myId, Guid subscriberId);

    /// <summary>
    /// Відписався
    /// </summary>
    /// <param name="subsctiberId"></param>
    Task RemoveSubscriberFromUser(Guid MyId, Guid subsctiberId);

    /// <summary>
    /// Отримати всіх моїх підписників
    /// </summary>
    /// <returns></returns>
    List<Guid> GetAlSubscribers(Guid userId);


}