namespace HackThePlanet.Host
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;


    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            app.UseStaticFiles(); 
            app.UseCors("AllowEverything");
            
            IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>(); 
            IServiceProvider serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            
            app.UseWebSockets(); 
            app.MapWebSocketManager("/game", serviceProvider.GetService<GameEndpoint>());
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConnections();
            services.AddCors(ConfigureCors);
            services.AddWebSocketManager();
            services.AddHostedService<GameService>();
        }


        private void ConfigureCors(CorsOptions corsOptions)
        {
            corsOptions.AddPolicy(
                "AllowEverything",
                corsPolicyBuilder =>
                    {
                        corsPolicyBuilder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
        }
    }
}