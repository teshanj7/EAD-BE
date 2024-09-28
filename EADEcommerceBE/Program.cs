using MongoDB.Driver;
using EADEcommerceBE.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read configuration from appsettings.json
IConfiguration configuration = builder.Configuration;

// Register IMongoClient as a Singleton
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(configuration.GetConnectionString("MongoDb")));

// Register the repository as Transient
builder.Services.AddTransient<IProductRepository, ProductRepository>();

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