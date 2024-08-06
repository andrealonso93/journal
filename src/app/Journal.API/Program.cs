using Journal.API.Filters;
using Journal.Database;
using Journal.Domain.Models;
using Journal.Notification;
using Journal.Repository.Implementations;
using Journal.Repository.Interfaces;
using Journal.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

// Add services to the container.

builder.Services.AddSingleton<NotificationContext>();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<NotificationFilter>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JournalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("journal")));

builder.Services.AddScoped<IQueryExecutor, SqlQueryExecutor>();
builder.Services.AddScoped<IRepository<Input>, InputRepository>();


builder.Services.AddScoped<IInputService, InputService>();



var app = builder.Build();
app.Logger.LogInformation("Application started");
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
