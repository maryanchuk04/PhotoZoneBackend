using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhotoZone.Core.IServices;
using PhotoZone.EF;
using PhotoZone.IServices;
using PhotoZone.Mapping;
using PhotoZone.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.v



#region ConfigureServices
builder.Services.AddAutoMapper(typeof(UserMapperProfile).GetTypeInfo().Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseSqlServer(
        "Server=127.0.0.1;Database=PhotoZone;user id = sa; password = Maks010104;MultipleActiveResultSets=false");
    options.EnableServiceProviderCaching(false);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IAuthServices,AuthServices>();
builder.Services.AddScoped<ISecurityContext, SecurityContext>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISubscribesService, SubscribesService>();
builder.Services.AddScoped<ISubscribtionService, SubscribtionService>();
#endregion


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.MapFallbackToFile("index.html"); ;

app.Run();
