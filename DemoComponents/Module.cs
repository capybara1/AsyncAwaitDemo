using AsyncAwait.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncAwait.DemoComponents
{
    public class Module
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITestService, PlainAsyncImplementation>();
            //services.AddTransient<ITestService, ManualImplementation>();
        }
    }
}
