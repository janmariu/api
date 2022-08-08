using System.Runtime.InteropServices;
using API.Dota;
    
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    builder = builder.UseSystemd();
}

//Register our modules.
builder.Services.AddDotaModule();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.Urls.Add("http://*:5010");

//Add our API endpoints.
app.AddDotaEndpoints();

app.Run();

