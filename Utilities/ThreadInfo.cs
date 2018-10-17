using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace AsyncAwait.Utilities
{
    public class ThreadInfo
    {
        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();
        
        public static ThreadInfo Current => CreateThreadInfo();

        private static ThreadInfo CreateThreadInfo()
        {
            Thread.BeginThreadAffinity();
            var threadId = GetCurrentThreadId();
            Thread.EndThreadAffinity();

            var process = Process.GetCurrentProcess();
            var processTread = process.Threads
                .Cast<ProcessThread>()
                .FirstOrDefault(t => t.Id == threadId);

            var managedThread = Thread.CurrentThread;
            var threadType = managedThread.IsThreadPoolThread
                ? "pool"
                : "regular";
                
            return new ThreadInfo
            {
                Id = threadId,
                Type = threadType,
                AffinityMask = "",
            };
        }

        public uint Id { get; private set; }
        
        public string Type { get; private set; }

        public string AffinityMask { get; private set; }

        public override string ToString()
        {
            return $"{Type} thread with id {Id} (0x{Id:x})";
        }
    }
}
