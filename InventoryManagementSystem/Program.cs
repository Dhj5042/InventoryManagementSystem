using Microsoft.Extensions.Configuration;
using System.Reflection;
using InventoryManagementSystem.Api.Utility;
using InventoryManagementSystem.Api.Services.Attributes;
using InventoryManagementSystem.Api.Database;
using FluentValidation;
using InventoryManagementSystem.API.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Database            
// Configure MongoDB
builder.Services.Configure<DBContext>(
    builder.Configuration.GetSection("DatabaseSettings"));
#endregion

#region Services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
#endregion

#region FluentValidation
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(ProductValidator).Assembly }, ServiceLifetime.Singleton);
#endregion

#region Dependency Injection            
builder.Services.RegisterRepository(Assembly.Load("InventoryManagementSystem.API.Repository"));
builder.Services.RegisterServices(Assembly.Load("InventoryManagementSystem.API.Services"), serviceType =>
{
    ServiceLifetimeAttribute attribute = serviceType.GetCustomAttribute<ServiceLifetimeAttribute>();
    ServiceLifetime lifetime = attribute == null ? ServiceLifetime.Scoped : attribute.LifeTime;
    return lifetime;
});
#endregion


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
