using AsyncAwait.DemoComponents;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AsyncAwaitDemo.BasicDemos
{
    public class TaskCompletionSourceDemos
    {
        // See https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/asynchronous-programming-model-apm
        
        [Fact(DisplayName = "Using wrapper for the Asynchronous Programming Model")]
        public async Task UseWrapperForTheAsynchronousProgrammingModel()
        {
            var wrapper = new LegacyServiceWrapper();

            var content = await wrapper.ReadFileContentAsync("Test.txt", Encoding.UTF8);

            Assert.Equal("Test", content);
        }
    }
}
