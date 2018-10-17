using ServiceContracts;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitAspNetCoreWebApi.Controllers
{
    internal class TestService : ITestService
    {
        public async Task<string> ReadFileAsync()
        {
            Debug.WriteLine("Start of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");

            string text;
            var stream = File.Open("Test.txt", FileMode.Open);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                text = await reader.ReadLineAsync();
            }

            Debug.WriteLine("Continuation of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");

            return text;
        }
    }
}