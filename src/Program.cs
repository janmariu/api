using API.Dota;
    
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register our modules.
builder.Services.AddDotaModule();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//Add our API endpoints.
app.AddDotaEndpoints();

app.Run();

