using BloodDonationCenters.Api.Endpoints;
using BloodDonationCenters.Application.Interfaces;
using BloodDonationCenters.Application.Services;
using BloodDonationCenters.Domain.Entities;
using BloodDonationCenters.Infraestructure.Data;
using BloodDonationCenters.Infraestructure.Interfaces;
using BloodDonationCenters.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BloodDonationDB"));

builder.Services.AddScoped<IRepository<BloodDonationCenter>, Repository<BloodDonationCenter>>();
builder.Services.AddScoped<IBloodDonationService, BloodDonationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        DbInitializer.Seed(context);
    }
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.AddBloodDonationCentersEndpoints();

app.Run();
