using AsyncAwait.WpfApplication.ViewModel.Commands;
using AsyncAwait.WpfApplication.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwait.WpfApplication
{
    /// <summary>
    /// The Application.
    /// </summary>
    public partial class Application : System.Windows.Application
    {
        private readonly ViewModel.ApplicationContext _context;
        
        public Application(ViewModel.ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            Debug.WriteLine("After starting up the application, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            Debug.WriteLine("Task scheduler for current synchronization context:");
            Debug.WriteLine(TaskScheduler.FromCurrentSynchronizationContext()?.GetType().Name);

            MainWindow = new MainWindow
            {
                DataContext = _context,
            };
            MainWindow.Show();

            base.OnStartup(args);
        }

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId} (UI Thread)");

            Debug.WriteLine("At program start, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            var services = new ServiceCollection();
            var serviceProvider = ConfigureServices(services);
            var app = serviceProvider.GetRequiredService<Application>();

            Debug.WriteLine("After instantiation of a WPF class, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            app.Run();
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var module = new DemoComponents.Module();
            module.ConfigureServices(services);

            services.AddSingleton<Application>();

            services.AddTransient(sp => new ReadFileAsyncCommand());

            services.AddTransient(sp => new ViewModel.ApplicationContext
            {
                SimpleExample = sp.GetRequiredService<ReadFileAsyncCommand>(),
            });

            IServiceProvider result = services.BuildServiceProvider();
            return result;
        }
    }
}