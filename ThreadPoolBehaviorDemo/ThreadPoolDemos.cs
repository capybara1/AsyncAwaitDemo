using AsyncAwait.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolBehaviorDemo
{
    [TestClass]
    public class ThreadPoolDemos
    {
        private static readonly TimeSpan _blockDuration = TimeSpan.FromSeconds(3);

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly object _lockHandle = new object();

        private int _minWorkerThreads;
        private int _maxWorkerThreads;
        private int _minIOCPThreads;
        private int _maxIOCPThreads;

        [TestMethod]
        public void DemoWithDefaultThreadPoolMinimum()
        {
            RunDemo();

            Thread.Sleep(TimeSpan.FromSeconds(10)); // Can be used to observe excess worker threads being destroyed

            if (Debugger.IsAttached) Debugger.Break();
        }

        [TestMethod]
        public void DemoWithIncreasedThreadPoolMinimum()
        {
            var newMinimum = 3*Environment.ProcessorCount;
            ThreadPool.SetMinThreads(newMinimum, newMinimum);

            RunDemo();

            if (Debugger.IsAttached) Debugger.Break();
        }

        [TestMethod]
        public async Task DemoWithDefaultThreadPoolMinimumAsync()
        {
            await RunDemoAsync();

            if (Debugger.IsAttached) Debugger.Break();
        }

        public void RunDemo(int numberOfTasks = 25)
        {
            WriteInitialnfoToDebug();

            var thread = ThreadInfo.Current;
            Debug.WriteLine($"Test runs on {thread}");

            var tasks = new Task[numberOfTasks];
            _stopwatch.Start();
            for (var index = 0; index < numberOfTasks; index++)
            {
                tasks[index] = Task.Run(() => DoWork(index));
            }

            Task.WaitAll(tasks); // Not awaitable
            _stopwatch.Stop();
        }

        public async Task RunDemoAsync(int numberOfTasks = 25)
        {
            WriteInitialnfoToDebug();

            Debug.WriteLine("At program start, the Synchronization Context is:");
            Debug.WriteLine(SynchronizationContext.Current?.GetType().Name ?? "null");

            var thread = ThreadInfo.Current;
            Debug.WriteLine($"Test runs on {thread}");

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

            Debug.WriteLine($"Task {taskId} Releasing {thread}");
        }

        private async Task DoWorkAsync(int taskId)
        {
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var thread = ThreadInfo.Current;

            WriteThreadSpecificInfoToDebug(taskId, elapsedMilliseconds, thread);

            Debug.WriteLine($"Task {taskId}: Releasing {thread}");

            await Task.Delay(_blockDuration); // Awaitable

            Debug.WriteLine($"Task {taskId}: Continuing on {thread}");
            Debug.WriteLine($"Task {taskId}: Releasing {thread}");
        }

        private void WriteInitialnfoToDebug()
        {
            var logicalProcessorCount = Environment.ProcessorCount;
            Debug.WriteLine($"Number of logical processors: {logicalProcessorCount}");

            ThreadPool.GetMinThreads(out _minWorkerThreads, out _minIOCPThreads);
            Debug.WriteLine($"Minimum number of worker threads in pool: {_minWorkerThreads}");
            Debug.WriteLine($"Minimum number of asyncronous IOCP (I/O Completion Port) threads in pool: {_minIOCPThreads}");

            ThreadPool.GetMaxThreads(out _maxWorkerThreads, out _maxIOCPThreads);
            Debug.WriteLine($"Maximum number of worker threads in pool: {_maxWorkerThreads}");
            Debug.WriteLine($"Maximum number of asyncronous IOCP threads in pool: {_maxIOCPThreads}");
        }

        private void WriteThreadSpecificInfoToDebug(int taskId, long elapsedMilliseconds, ThreadInfo thread)
        {
            ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableCompletionPortThread);
            var usedWorkerThreads = _maxWorkerThreads - availableWorkerThreads;
            var usedIOCPThreads = _maxIOCPThreads - availableCompletionPortThread;

            lock (_lockHandle) // Keep lines together
            {
                Debug.WriteLine($"Task {taskId}: Got {thread} after {elapsedMilliseconds} ms");
                if (usedWorkerThreads > 0)
                {
                    Debug.WriteLine($"Task {taskId}: Current number of used worker threads in pool: {usedWorkerThreads}");
                }
                if (usedIOCPThreads > 0)
                {
                    Debug.WriteLine($"Task {taskId}: Current number of used asyncronous IOCP threads in pool: {usedIOCPThreads}");
                }
            }
        }
    }
}
