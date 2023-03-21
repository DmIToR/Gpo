using InfoSystem.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// -----------------
// Adding swagger
// -----------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------
// Register database
// -----------------

builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

// -----------------
// App build
// -----------------

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