using DigiPayTest.DbContexts;
using DigiPayTest.Interfaces.Repositories;
using DigiPayTest.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      builder =>
//                      {
//                          //builder.WithOrigins("http://localhost:3000/").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
//                          builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
//                      });
//});
builder.Services.AddControllers();

//builder.Services.AddControllers(options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//})
//    .AddNewtonsoftJson(options => {
//        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    })
//    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WeatherDbContext>(option =>
{
    option.UseMySql(
        builder.Configuration["ConnectionStrings:SqlConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:SqlConnection"])
        );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IWeather, WeatherRepository>();

var app = builder.Build();

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

