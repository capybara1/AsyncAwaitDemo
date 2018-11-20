using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.DemoComponents
{
    public class LegacyEapServiceWrapper
    {
        private readonly LegacyEapService _inner;

        public LegacyEapServiceWrapper(LegacyEapService inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));

            _inner.SendCompleted += HandleSendCompleted;
        }

        public Task<string> SendAsync(string message)
        {
            var tcs = new TaskCompletionSource<string>();

            _inner.SendAsync(message, tcs);

            var result = tcs.Task;

            return result;
        }

        private void HandleSendCompleted(object sender, SendCompletedEventArgs args)
        {
            var tsc = (TaskCompletionSource<string>)args.UserState;

            if (args.Error != null)
            {
                tsc.SetException(args.Error);
            }
            else if (args.Cancelled)
            {
                tsc.SetCanceled();
            }
            else
            {
                tsc.SetResult(args.Response);
            }
        }
    }
}
