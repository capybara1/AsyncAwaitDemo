using System;
using System.Runtime.CompilerServices;

namespace AsyncAwait.DemoComponents
{
    public class CustomAwaiter : INotifyCompletion
    {
        private readonly Action<string> _writeLine;
        private readonly bool _isCompleted;

        public CustomAwaiter(Action<string> writeLine, bool isCompleted)
        {
            _writeLine = writeLine ?? throw new ArgumentNullException(nameof(writeLine));
            _isCompleted = isCompleted;
        }

        public bool IsCompleted {
            get
            {
                _writeLine("IsCompleted invoked; returning " + _isCompleted);
                return _isCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            _writeLine("OnCompleted invoked");
            continuation();
        }

        public void GetResult()
        {
            _writeLine("GetResult invoked");
        }
    }
}