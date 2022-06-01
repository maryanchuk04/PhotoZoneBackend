namespace PhotoZone.Services;

public interface ISecurityContext
{
    Guid GetCurrentUserId();
}