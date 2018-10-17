using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.WpfApplication.ViewModel.Commands
{
    internal class ReadFileAsyncCommand : AsyncCommand
    {
        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            Debug.WriteLine("Start of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");

            var stream = File.Open("Test.txt", FileMode.Open);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var text = await reader.ReadLineAsync();
                Debug.WriteLine($"The read text is '{text}'");
            }
            
            Debug.WriteLine("Continuation of async method");
            Debug.WriteLine($"Thread id is {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
