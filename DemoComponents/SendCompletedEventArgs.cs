using System;
using System.ComponentModel;

namespace AsyncAwait.DemoComponents
{
    public class SendCompletedEventArgs : AsyncCompletedEventArgs
    {
        public SendCompletedEventArgs(
            Exception error, 
            bool cancelled,
            object userState)
            : base(error, cancelled, userState)
        { }

        public string Response { get; set; }
    }
}
