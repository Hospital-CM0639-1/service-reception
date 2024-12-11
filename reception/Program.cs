using DataAccessLayer.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IPatientService, PatientService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
