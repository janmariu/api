namespace API.Dota;

public static class DotaModule
{
    public static IServiceCollection AddDotaModule(this IServiceCollection services)
    {
        services.AddTransient<SteamService>();
        services.AddTransient<DotaService>();
        services.AddTransient<DotaPlayerRepository>();
        services.Configure<SteamServiceOptions>(o => o.Key = Environment.GetEnvironmentVariable("STEAM_KEY"));

        return services;
    }

    public static IEndpointRouteBuilder AddDotaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/dota", async (DotaService teamService) => await teamService.GetTeamStatus());
        
        return endpoints;
    }
}