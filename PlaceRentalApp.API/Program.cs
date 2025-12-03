using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.API.Middlewares;
using PlaceRentalApp.API.Persistance;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PlaceRental");

//builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseInMemoryDatabase("PlaceRentalDb"));
builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();