using AsyncAwait.ServiceContracts;
using AsyncAwait.Utilities;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.DemoComponents
{
    internal class PlainAsynImplementation : ITestService
    {
        public async Task<string> ProvideTestValueAsync()
        {
            Debug.WriteLine("Start of async method");
            WriteInfoToDebugOut();

            string text;
            var stream = File.Open("Test.txt", FileMode.Open);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                text = await reader.ReadLineAsync();
            }

            Debug.WriteLine("Continuation of async method");
            WriteInfoToDebugOut();

            return text;
        }

        private static void WriteInfoToDebugOut()
        {
            var thread = ThreadInfo.Current;
            Debug.WriteLine($"Running on {thread}");
        }
    }
}