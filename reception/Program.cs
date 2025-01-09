using System.Security.Cryptography;
using DataAccessLayer.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using reception.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
Console.WriteLine($"Connection string is: {builder.Configuration.GetConnectionString("Psql")}");

builder.Services.AddDbContextFactory<HospitalContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("Psql"),
            x => x.MigrationsAssembly("DAL.Psql.Migrations"))
        //.UseLazyLoadingProxies()
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
    );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
    {
        var rsaKey = RSA.Create();
        rsaKey.ImportFromPem(File.ReadAllText(builder.Configuration.GetSection("Jwt:PublicKeyPath").Value));
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,   
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(rsaKey)
        }; 
    });
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Insert(0, new GlobalRoutePrefixConvention("api/v1/reception-service"));
});
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IEmergencyVisitService, EmergencyVisitService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();   

app.Run();


// Define the GlobalRoutePrefixConvention class
public class GlobalRoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public GlobalRoutePrefixConvention(string prefix)
    {
        _routePrefix = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(prefix));
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel != null)
                {
                    // Combine the existing route with the global prefix
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                        _routePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    // Add the global prefix if no route is defined
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}
