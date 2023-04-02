using InfoSystem.Data;

namespace InfoSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            DatabaseInitializer.Init(scope.ServiceProvider);
        }
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {    
                
                webBuilder.UseStartup<Startup>();
            });
    }
}