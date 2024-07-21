using DFX.DUROCONNECT.COMM.DAL.Wrapper;
using DFX.DUROCONNECT.COMMUNICATION;
using Microsoft.Extensions.Configuration;
using RepoDb;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
GlobalConfiguration.Setup().UseSqlServer();



builder.Services.AddHttpClient();

// Optionally configure named HTTP clients
 
var DBConnection = configuration.GetConnectionString("DBConnection");

builder.Services.Add(new ServiceDescriptor(typeof(SPWrapper), new SPWrapper(DBConnection)));


 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Services.GetRequiredService<ILogger<SendCommunication>>();
app.Services.GetRequiredService<IHttpClientFactory>();
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
