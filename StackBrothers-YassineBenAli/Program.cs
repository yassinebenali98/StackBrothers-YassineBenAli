using StackBrothers_YassineBenAli.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "./address.db";
var connection = new SqliteConnection($"Data Source={dbPath}");

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connection));
    


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
