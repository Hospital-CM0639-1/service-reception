using System.Security.Cryptography;
using DataAccessLayer.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using reception.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


Console.WriteLine($"Connection string is: {builder.Configuration.GetConnectionString("Psql")}");

builder.Services.AddDbContextFactory<HospitalContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("Psql"),
            x => x.MigrationsAssembly("DAL.Psql.Migrations"))
        //.UseLazyLoadingProxies()
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
    );

builder.Services.AddControllers();
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

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IManagerService, ManagerService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle    
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

app.MapControllers();

app.Run();
