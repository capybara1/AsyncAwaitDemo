using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AsyncAwait.ThreadPoolBehaviorDemo
{
    public class TaskDemos
    {
        // See also https://blogs.msdn.microsoft.com/pfxteam/2011/10/24/task-run-vs-task-factory-startnew/
        
        private readonly ITestOutputHelper _testOutputHelper;

        public TaskDemos(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }
        
        [Fact(DisplayName = "Default behavior of Task.Factory.StartNew")]
        public async Task DefaultBehaviorOfTaskFactory()
        {
            WriteInformation("=== Test thread");
            WriteThreadInformation(Thread.CurrentThread);

            WriteInformation("=== Task thread");
            var task = Task.Factory.StartNew(() =>
            {
                WriteThreadInformation(Thread.CurrentThread);
                WriteSchedulerInformation(TaskScheduler.Current);
            });
            await task;

            WriteInformation("=== Task");
            WriteTaskInformation(task);
        }

        [Fact(DisplayName = "Default behavior of Task.Run")]
        public async Task DefaultBehaviorOfTaskRun()
        {
            WriteInformation("=== Test thread");
            WriteThreadInformation(Thread.CurrentThread);

            WriteInformation("=== Task thread");
            var task = Task.Run(() =>
            {
                WriteThreadInformation(Thread.CurrentThread);
                WriteSchedulerInformation(TaskScheduler.Current);
            });
            await task;

            WriteInformation("=== Task");
            WriteTaskInformation(task);
        }

        private void WriteThreadInformation(Thread thread)
        {
            WriteInformation($"Id: {thread.ManagedThreadId}");
            WriteInformation($"Is pool thread: {thread.IsThreadPoolThread}");
        }

        private void WriteSchedulerInformation(TaskScheduler scheduler)
        {
            WriteInformation($"Is default scheduler: {scheduler == TaskScheduler.Default}");
        }

        private void WriteTaskInformation(Task task)
        {
            WriteInformation($"Creation options: {task.CreationOptions}");
        }

        private void WriteInformation(string message)
        {
            _testOutputHelper.WriteLine(message);
            Debug.WriteLine(message);
        }
    }
}
