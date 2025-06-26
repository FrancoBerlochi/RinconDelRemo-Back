using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static Infrastructure.Services.AuthenticationService;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Application.UsesCases;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;




var builder = WebApplication.CreateBuilder(args);

// Console.WriteLine($"Ambiente actual: {builder.Environment.EnvironmentName}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region Swagger Authentication Config
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("RinconDelRemoApiBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "RinconDelRemoApiBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definici�n
                }, new List<string>() }
    });

});
#endregion

#region Database Configuration
var connectionString = builder.Configuration.GetConnectionString("RinconDelRemoDB");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .LogTo(Console.WriteLine, LogLevel.Debug)
           .EnableSensitiveDataLogging()
);


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

#endregion(

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVercel", builder =>
    {
        builder.WithOrigins("https://rincondelremo.vercel.app", "http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
#endregion

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cliente", policy =>
        policy.RequireClaim("Tipo de usuario", "Cliente"));

    options.AddPolicy("DuenioKayak", policy =>
        policy.RequireClaim("Tipo de usuario", "DuenioKayak"));

    options.AddPolicy("ClienteODuenio", policy =>
        policy.RequireClaim("Tipo de usuario", "Cliente", "DuenioKayak"));
});
//.AddJwtBearer("LocalJwt", options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["AuthenticationService:Issuer"],

//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["AuthenticationService:Audience"],

//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationService:SecretForKey"])),

//        NameClaimType = "NameIdentifier",
//        RoleClaimType = "Role"
//    };
//})
//.AddJwtBearer("AzureB2C", options =>
//{
//    options.Authority = "https://rincondelremo.ciamlogin.com/404f8a9c-8bed-4081-8425-fb67edb49460/v2.0";
//    options.Audience = "8eed8731-13b7-4e6a-9481-416825ac461d";
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,

//    };

//});

#endregion

#region Services
builder.Services.AddScoped<IKayakService, KayakService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IAttendantService, AttendantService>();
builder.Services.AddScoped<IKayakReservationService, KayakReservationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.Configure<AuthenticacionServiceOptions>(
    builder.Configuration.GetSection(AuthenticacionServiceOptions.AuthenticacionService));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IKayakReservationService, KayakReservationService>();
builder.Services.AddHttpClient<IWeatherService, OpenWeatherService>();
#endregion

#region Repositories
builder.Services.AddScoped<IKayakRepository, KayakRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IAttendantRepository, AttendantRepository>();
builder.Services.AddScoped<IKayakReservationRepository, KayakReservationRepository>();

#endregion

#region MercadoPago
builder.Services.AddSingleton<IPaymentService>(provider =>
    new MercadoPagoService("APP_USR-8203332625940662-061915-c4e2ad5f8f8f38c243ffa8ca0e22935c-2503974515"));
builder.Services.AddTransient<CreatePaymentUseCase>();

#endregion


var app = builder.Build();

// Middleware global de manejo de excepciones
//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//var exception = exceptionHandlerPathFeature?.Error;

//Console.WriteLine($"Error global: {exception?.Message}");
//Console.WriteLine(exception?.StackTrace);

//context.Response.StatusCode = 500;
//await context.Response.WriteAsync("Error interno del servidor");
//    });
//});

app.UseSwaggerUI(c =>
{
    c.OAuthUsePkce();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVercel");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
