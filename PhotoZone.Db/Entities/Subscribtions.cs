namespace PhotoZone.Entities;

//ті на кого підписуюсь я
public class Subscribtions
{
    public Guid Id { get; set; }

    public Guid SubscribtionId { get; set; }

    public Guid? UserId { get; set; }

}
