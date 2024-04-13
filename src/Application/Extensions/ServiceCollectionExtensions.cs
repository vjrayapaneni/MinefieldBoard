// Copyright (c) 2024, <Company>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinefieldBoard.Application.Models;
using MinefieldBoard.Application.Services;
using MinefieldBoard.Domain.Interfaces;

namespace MinefieldBoard.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }


            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }


            services.AddOptions<BoardOptions>().Bind(configuration.GetSection(BoardOptions.Board));
            services.AddLogging();
            services.AddSingleton<IPlayer, Player>();
            services.AddSingleton<IGameBoard, MinefieldGameBoard>();
            services.AddTransient<ITile, Tile>();
            services.AddSingleton<IConsoleWriterService, ConsoleWriterService>();

            return services;
        }
    }
}