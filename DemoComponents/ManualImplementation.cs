using AsyncAwait.ServiceContracts;
using AsyncAwait.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.DemoComponents
{
    // Note: the generated code by the c# compiler is a state machine
    // The code below serves solely for demonstration

    internal class ManualImplementation : ITestService
    {
        public Task<string> ProvideTestValueAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<string>();

            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Debug.WriteLine("Start of method");
                        WriteInfoToDebugOut();

                        var stream = File.Open("Test.txt", FileMode.Open);
                        var reader = new StreamReader(stream, Encoding.UTF8);

                        var readLineTask = reader.ReadLineAsync();
                        readLineTask.ContinueWith(t =>
                            {
                                try
                                {
                                    reader.Dispose();

                                    if (t.IsFaulted)
                                    {
                                        taskCompletionSource.SetException(t.Exception);
                                        return;
                                    }
                            
                                    string text = t.Result;
                                    Debug.WriteLine("Continuation of method");
                                    WriteInfoToDebugOut();

                                    taskCompletionSource.SetResult(text);
                                }
                                catch (Exception exception)
                                {
                                    taskCompletionSource.SetException(exception);
                                }
                            });
                    }
                    catch (Exception exception)
                    {
                        taskCompletionSource.SetException(exception);
                    }
                },
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                TaskScheduler.Default);

            return taskCompletionSource.Task;
        }

        private static void WriteInfoToDebugOut()
        {
            var thread = ThreadInfo.Current;
            Debug.WriteLine($"Running on {thread}");
        }
    }
}