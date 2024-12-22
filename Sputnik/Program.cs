﻿using Microsoft.Extensions.DependencyInjection;
using Sputnik.Services;

namespace Sputnik
{
    internal class Program
    {
        private static CancellationTokenSource _cts = new();

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;

            var services = new ServiceCollection();
            ConfigureServices(services);
            var servicesProvider = services.BuildServiceProvider();

            try
            {
                var game = servicesProvider.GetRequiredService<Game>();
                await game.Play(_cts.Token);
            }
            catch (OperationCanceledException)
            {
                var position = Console.CursorTop;
                Console.SetCursorPosition(0, position + 1);
                Console.WriteLine("Cancelled");
            }
            finally
            {
                Console.WriteLine("Exiting");
            }
            
        }

        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _cts.Cancel();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IInputListener, InputListener>();
            services.AddTransient<WordHunt>();
            services.AddTransient<NumberCrunch>();
            services.AddTransient<PatternMatch>();
            services.AddTransient<Game>();
        }
    }
}
