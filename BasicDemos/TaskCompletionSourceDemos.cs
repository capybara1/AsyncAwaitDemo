using AsyncAwait.DemoComponents;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AsyncAwaitDemo.BasicDemos
{
    public class TaskCompletionSourceDemos
    {
        [Fact(DisplayName = "Using wrapper for the Asynchronous Programming Model")]
        public async Task UseWrapperForTheAsynchronousProgrammingModel()
        {
            // See https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/asynchronous-programming-model-apm

            var wrapper = new LegacyApmServiceWrapper();

            var content = await wrapper.ReadFileContentAsync("Test.txt", Encoding.UTF8);

            Assert.Equal("Test", content);
        }

        [Fact(DisplayName = "Using wrapper for the Event-based Asynchronous Pattern")]
        public async Task UseWrapperForTheEventBasedAsynchronousPattern()
        {
            // See https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/event-based-asynchronous-pattern-eap

            var service = new LegacyEapService();
            var wrapper = new LegacyEapServiceWrapper(service);

            var content = await wrapper.SendAsync("Example");

            Assert.Equal("Test", content);
        }
    }
}
