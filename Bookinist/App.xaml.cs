using Bookinist.Data;
using Bookinist.Services.Registration;
using Bookinist.ViewModels.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;

namespace Bookinist
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w =>
            w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w =>
            w.IsFocused);

        public static Window CurrentWindow => FocusedWindow ?? ActiveWindow;

        private static IHost _host;

        public static IHost Host => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs())
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) => Microsoft.Extensions.Hosting
            .Host
                .CreateDefaultBuilder(args)
                .ConfigureServices(
                    (hostContext, services) => services
                        .AddDatabase(hostContext.Configuration.GetSection("Database"))
                        .AddServices()
                        .AddViewModels()
            );

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IHost host = Host;
            IsDesignMode = false;

            using (IServiceScope scope = Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DatabaseInitializer>().InitializeAsync()
                    .Wait();
            }

            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using IHost host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
