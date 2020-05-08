namespace HackThePlanet.Host
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;


    public static class WebSocketExtensions
    {
        /// <summary>
        /// Finds and maps all WebsocketEndpoints as services.
        /// </summary>
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<WebSocketList>();

            IEnumerable<Type> exportedTypes = Assembly
                .GetEntryAssembly()?
                .ExportedTypes;
            if (exportedTypes != null)
            {
                foreach (Type type in exportedTypes)
                {
                    if (type.GetTypeInfo().BaseType == typeof(WebsocketEndpoint))
                        services.AddSingleton(type);
                }
            }

            return services;
        }


        /// <summary>
        /// Registers WebSocketMiddleware.
        /// </summary>
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app, 
                                                              PathString path,
                                                              WebsocketEndpoint handler)
        {
            return app.Map(
                pathMatch: path,
                configuration: _app => _app.UseMiddleware<WebSocketMiddleware>(handler));
        }
    }
}