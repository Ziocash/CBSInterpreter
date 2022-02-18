using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Windows;

namespace CBSInterpreter.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddLogging(opt =>
            opt.AddSerilog(new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(@$"./log{DateTime.Now:yyyymmdd}.log").CreateLogger()));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _serviceProvider.DisposeAsync();

            base.OnExit(e);
        }
    }
}
