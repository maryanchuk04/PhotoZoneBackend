namespace PhotoZone.Entities;

//ti yaki pidpus na mene
public class Subscribers
{
    /// <summary>
    /// CurrentUserId
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// SubscribesID user na kkogo ya pidp
    /// </summary>
    public Guid SubscriberId { get; set; }

    public Guid UserId { get; set; }
}