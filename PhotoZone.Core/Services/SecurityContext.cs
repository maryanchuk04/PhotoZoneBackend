using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;

namespace PhotoZone.Services;

public class SecurityContext : ISecurityContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SecurityContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        Claim guidClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);

        if (guidClaim == null || !Guid.TryParse(guidClaim.Value, out Guid result))
        {
            throw new PhotoZoneException("User not found");
        }

        return result;
    }
}