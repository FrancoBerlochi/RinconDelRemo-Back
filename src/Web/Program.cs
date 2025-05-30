using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Console.WriteLine($"Ambiente actual: {builder.Environment.EnvironmentName}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database Configuration
var connectionString = builder.Configuration.GetConnectionString("RinconDelRemoDB");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#endregion

#region Swagger config
//builder.Services.AddSwaggerGen();
string instance = builder.Configuration["AzureAd:Instance"]!;
string tenantId = builder.Configuration["AzureAd:TenantId"]!;
string clientid = builder.Configuration["AzureAd:ClientId"]!;
string ApplicationIdURI = builder.Configuration["AzureAd:ApplicationIdURI"]!;
string scope = "default";
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configure OAuth2
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{instance}{tenantId}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{instance}{tenantId}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { $"{ApplicationIdURI}/{scope}", "Access API" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { $"{ApplicationIdURI}/{scope}" }
        }
    });
});

#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://rincondelremo.ciamlogin.com/404f8a9c-8bed-4081-8425-fb67edb49460/v2.0";
        options.Audience = "8eed8731-13b7-4e6a-9481-416825ac461d";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true
        };
    });

builder.Services.AddAuthorization();
#endregion

#region Services
builder.Services.AddScoped<IKayakService, KayakService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();

#endregion

#region Repositories
builder.Services.AddScoped<IKayakRepository, KayakRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
#endregion


var app = builder.Build();

app.UseSwaggerUI(c =>
{
    c.OAuthUsePkce();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
