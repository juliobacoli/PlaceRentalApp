using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.API.Middlewares;
using PlaceRentalApp.API.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlaceRentalDbContext>(o => o.UseInMemoryDatabase("PlaceRentalDb"));
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
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