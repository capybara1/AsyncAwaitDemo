using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait.WindowsFormsApplication
{
    internal class Program
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Debug.WriteLine("After using static methods from Application, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            var mainWindow = new MainWindow();

            Debug.WriteLine("After creating a Windows Forms class, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            Debug.WriteLine("Task scheduler for current synchronization context:");
            Debug.WriteLine(TaskScheduler.FromCurrentSynchronizationContext()?.GetType().Name);

            Debug.WriteLine("Task current task scheduler:");
            Debug.WriteLine(TaskScheduler.Current?.GetType().Name);
            
            Application.Run(mainWindow);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId} (UI Thread)");

            Debug.WriteLine("At program start, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            var services = new ServiceCollection();
            var serviceProvider = ConfigureServices(services);
            var program = serviceProvider.GetRequiredService<Program>();
            program.Run();
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var module = new DemoComponents.Module();
            module.ConfigureServices(services);

            services.AddTransient(sp => new Program());
            
            IServiceProvider result = services.BuildServiceProvider();
            return result;
        }
    }
}
