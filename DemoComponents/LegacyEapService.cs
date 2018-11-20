using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.DemoComponents
{
    public class LegacyEapService
    {
        public void SendAsync(string message)
        {
            SendAsync(message, null);
        }

        public void SendAsync(string message, object userState)
        {
            Task.Factory.StartNew(
                () => 
                {
                    Thread.Sleep(1000);
                    SendCompleted?.Invoke(
                        this,
                        new SendCompletedEventArgs(null, false, userState)
                        {
                            Response = "Test",
                        });
                },
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        public event SendCompletedEventHandler SendCompleted;
    }
}
