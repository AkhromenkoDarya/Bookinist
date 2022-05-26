using Bookinist.Services.Registration;
using Bookinist.ViewModels.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;

namespace Bookinist
{
    public partial class App
    {
        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w =>
            w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w =>
            w.IsFocused);

        private static IHost _host;

        public static IHost Host => _host ??= Microsoft.Extensions.Hosting.Host
           .CreateDefaultBuilder(Environment.GetCommandLineArgs())
           .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", true, true))
           .ConfigureServices((host, services) => services
               .AddViewModels()
               .AddServices())
           .Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IHost host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using IHost host = Host;
            await host.StopAsync();
        }
    }
}
