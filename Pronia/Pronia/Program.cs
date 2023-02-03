using Pronia.Infastructure.Extentions;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup
            var builder = WebApplication.CreateBuilder(args);

            //Register services (IoC container)
            builder.Services.ConfigureServices(builder.Configuration);

            //setup
            var app = builder.Build();

            //Configuration of middleware pdipeline
            app.ConfigureMiddlewarePipeline();

            //setup
            app.Run();
        }
    }
}