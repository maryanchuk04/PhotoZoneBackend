namespace PhotoZone.PhotoZoneExtention;

public static class CookiesExtension
{
    public static void SetTokenCookie(this HttpContext context, string Token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(7),
            Secure = true,
        };

        context.Response.Cookies.Delete("Token");
        context.Response.Cookies.Append("Token", Token, cookieOptions);
    }
}