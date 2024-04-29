using Microsoft.EntityFrameworkCore;
using SNGeneratorAPI.Data;
using SNGeneratorAPI.Model;
using SNGeneratorAPI.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Using builder to create a connection to SQL Server
var connectionString = builder.Configuration.GetConnectionString("SNConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<SNContext>(options => 
options.UseSqlServer());


// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options
.ResolveConflictingActions(apiDescription => apiDescription.First()));

builder.Services.AddScoped<OperacaoService>();
builder.Services.AddScoped<ComponenteService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
