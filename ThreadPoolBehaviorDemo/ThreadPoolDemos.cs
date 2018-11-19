using AsyncAwait.Utilities;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AsyncAwaitDemo.ThreadPoolBehaviorDemo
{
    public class ThreadPoolDemos
    {
        private static readonly TimeSpan _blockDuration = TimeSpan.FromSeconds(3);

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly object _lockHandle = new object();

        private int _minWorkerThreads;
        private int _maxWorkerThreads;
        private int _minIOCPThreads;
        private int _maxIOCPThreads;

        private readonly ITestOutputHelper _testOutputHelper;

        public ThreadPoolDemos(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Demo of blocking tasks on thread pool with default minimum")]
        public void DemoOfBlockingTasksOnThreadPoolWithDefaultMinimum()
        {
            RunDemo();

            Thread.Sleep(TimeSpan.FromSeconds(10)); // Can be used to observe excess worker threads being destroyed

            if (Debugger.IsAttached) Debugger.Break();
        }

        [Fact(DisplayName = "Demo of blocking tasks on thread pool with increased minimum")]
        public void DemoOfBlockingTasksOnThreadPoolWithIncreasedMinimum()
        {
            var newMinimum = 3*Environment.ProcessorCount;
            ThreadPool.SetMinThreads(newMinimum, newMinimum);

            RunDemo();

            if (Debugger.IsAttached) Debugger.Break();
        }

        [Fact(DisplayName = "Demo of async tasks on thread pool with default minimum")]
        public async Task DemoOfAsyncTasksOnThreadPoolWithDefaultMinimum()
        {
            await RunDemoAsync();

            if (Debugger.IsAttached) Debugger.Break();
        }

        public void RunDemo(int numberOfTasks = 25)
        {
            WriteInitialnfoToDebug();

            var thread = ThreadInfo.Current;
            WriteInformation($"Test runs on {thread}");

            var tasks = new Task[numberOfTasks];
            _stopwatch.Start();
            for (var index = 0; index < numberOfTasks; index++)
            {
                var i = index;
                tasks[index] = Task.Run(() => DoWork(i));
            }

            Task.WaitAll(tasks); // Not awaitable
            _stopwatch.Stop();
        }

        public async Task RunDemoAsync(int numberOfTasks = 25)
        {
            WriteInitialnfoToDebug();

            WriteInformation("At program start, the Synchronization Context is:");
            WriteInformation(SynchronizationContext.Current?.GetType().Name ?? "null");

            var thread = ThreadInfo.Current;
            WriteInformation($"Test runs on {thread}");

            var tasks = new Task[numberOfTasks];
            _stopwatch.Start();
            for (var index = 0; index < numberOfTasks; index++)
            {
                tasks[index] = DoWorkAsync(index);
            }

            await Task.WhenAll(tasks); // Awaitable
            _stopwatch.Stop();
        }

        private void DoWork(int taskId)
        {
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var thread = ThreadInfo.Current;

            WriteThreadSpecificInfoToDebug(taskId, elapsedMilliseconds, thread);

            Thread.Sleep(_blockDuration); // Blocks worker threads, not awaitable

            WriteInformation($"Task {taskId} Releasing {thread}");
        }

        private async Task DoWorkAsync(int taskId)
        {
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var thread = ThreadInfo.Current;

            WriteThreadSpecificInfoToDebug(taskId, elapsedMilliseconds, thread);

            WriteInformation($"Task {taskId}: Releasing {thread}");

            await Task.Delay(_blockDuration); // Awaitable

            WriteInformation($"Task {taskId}: Continuing on {thread}");
            WriteInformation($"Task {taskId}: Releasing {thread}");
        }

        private void WriteInitialnfoToDebug()
        {
            var logicalProcessorCount = Environment.ProcessorCount;
            WriteInformation($"Number of logical processors: {logicalProcessorCount}");

            ThreadPool.GetMinThreads(out _minWorkerThreads, out _minIOCPThreads);
            WriteInformation($"Minimum number of worker threads in pool: {_minWorkerThreads}");
            WriteInformation($"Minimum number of asyncronous IOCP (I/O Completion Port) threads in pool: {_minIOCPThreads}");

            ThreadPool.GetMaxThreads(out _maxWorkerThreads, out _maxIOCPThreads);
            WriteInformation($"Maximum number of worker threads in pool: {_maxWorkerThreads}");
            WriteInformation($"Maximum number of asyncronous IOCP threads in pool: {_maxIOCPThreads}");
        }

        private void WriteThreadSpecificInfoToDebug(int taskId, long elapsedMilliseconds, ThreadInfo thread)
        {
            ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableCompletionPortThread);
            var usedWorkerThreads = _maxWorkerThreads - availableWorkerThreads;
            var usedIOCPThreads = _maxIOCPThreads - availableCompletionPortThread;

            lock (_lockHandle) // Keep lines together
            {
                WriteInformation($"Task {taskId}: Got {thread} after {elapsedMilliseconds} ms");
                if (usedWorkerThreads > 0)
                {
                    WriteInformation($"Task {taskId}: Current number of used worker threads in pool: {usedWorkerThreads}");
                }
                if (usedIOCPThreads > 0)
                {
                    WriteInformation($"Task {taskId}: Current number of used asyncronous IOCP threads in pool: {usedIOCPThreads}");
                }
            }
        }

        private void WriteInformation(string message)
        {
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var elapsedTimeSpan = TimeSpan.FromMilliseconds(elapsedMilliseconds);

            var output = $@"{elapsedTimeSpan:h\:mm\:ss\.fff} {message}";

            _testOutputHelper.WriteLine(output);
            Debug.WriteLine(output);
        }
    }
}
