using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AsyncAwait.BasicDemos
{
    public class TaskDemos
    {
        // See also https://blogs.msdn.microsoft.com/pfxteam/2011/10/24/task-run-vs-task-factory-startnew/
        // https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler#Default

        private readonly ITestOutputHelper _testOutputHelper;

        public TaskDemos(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Default behavior of Task.Factory.StartNew")]
        public async Task DefaultBehaviorOfTaskFactory()
        {
            WriteInformation("=== Test");
            WriteThreadInformation(Thread.CurrentThread);

            WriteInformation("=== Task");
            var task = Task.Factory.StartNew(() =>
            {
                WriteThreadInformation(Thread.CurrentThread);
                WriteSchedulerInformation(TaskScheduler.Current);
            });
            await task;

            WriteTaskInformation(task);
        }

        [Fact(DisplayName = "Default behavior of Task.Run")]
        public async Task DefaultBehaviorOfTaskRun()
        {
            WriteInformation("=== Test");
            WriteThreadInformation(Thread.CurrentThread);

            WriteInformation("=== Task");
            var task = Task.Run(() =>
            {
                WriteThreadInformation(Thread.CurrentThread);
                WriteSchedulerInformation(TaskScheduler.Current);
            });
            await task;

            WriteTaskInformation(task);
        }

        [Fact(DisplayName = "Long running task with Task.Factory.StartNew")]
        public async Task LongRunningTaskWithTaskFactory()
        {
            WriteInformation("=== Test");
            WriteThreadInformation(Thread.CurrentThread);

            WriteInformation("=== Task");
            var task = Task.Factory.StartNew(() =>
            {
                WriteThreadInformation(Thread.CurrentThread);
                WriteSchedulerInformation(TaskScheduler.Current);
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            await task;

            WriteTaskInformation(task);
        }

        private void WriteThreadInformation(Thread thread)
        {
            WriteInformation($"Id: {thread.ManagedThreadId}");
            WriteInformation($"Is pool thread: {thread.IsThreadPoolThread}");
        }

        private void WriteSchedulerInformation(TaskScheduler scheduler)
        {
            WriteInformation($"Task scheduler: {scheduler?.GetType().Name}");
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
