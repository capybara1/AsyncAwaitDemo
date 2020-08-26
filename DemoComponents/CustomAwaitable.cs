using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncAwait.DemoComponents
{
    public class CustomAwaitable
    {
        private readonly Action<string> _writeLine;
        private readonly bool _isCompleted;

        public CustomAwaitable(Action<string> writeLine, bool isCompleted)
        {
            _writeLine = writeLine ?? throw new ArgumentNullException(nameof(writeLine));
            _isCompleted = isCompleted;
        }

        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter(_writeLine, _isCompleted);
        }
    }
}
