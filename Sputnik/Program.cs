using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sputnik.Services;

namespace Sputnik
{
    internal class Program
    {
        private static CancellationTokenSource _cts = new();
        public static GameSettings GameSettings { get; private set; }
        static async Task Main(string[] args)
        {
            ConfigureSettings();

            Console.SetBufferSize(120, 30);
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

        private static void ConfigureSettings()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            GameSettings = config.GetSection("GameSettings").Get<GameSettings>();
        }

        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _cts.Cancel();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IInputListener, InputListener>();
            services.AddSingleton<IShapeDrawer, ShapeDrawer>();
            services.AddSingleton<IConsoleService , ConsoleService>();
            services.AddSingleton<IPlayMechanicsService, PlayMechanicsService>();
            services.AddTransient<WordHunt>();
            services.AddTransient<NumberCrunch>();
            services.AddTransient<PatternMatch>();
            services.AddTransient<Game>();
        }
    }
}
