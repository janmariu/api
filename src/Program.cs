using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Systemd;
using API.Dota;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("*", policyBuilder => policyBuilder.AllowAnyOrigin()));

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
     builder.Host.UseSystemd();
}

//Register our modules.
builder.Services.AddDotaModule();

var app = builder.Build();
app.UseCors("*");
app.UseSwagger();
app.UseSwaggerUI();
app.Urls.Add("http://*:5010");

//Add our API endpoints.
app.AddDotaEndpoints();

app.Run();

