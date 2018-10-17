using AsyncAwait.WpfApplication.ViewModel.Commands;

namespace AsyncAwait.WpfApplication.ViewModel
{
    public class ApplicationContext
    {
        public IAsyncCommand SimpleExample { get; set; }
        public IAsyncCommand ContinueOnCapturedContext { get; set; }
        public IAsyncCommand DontContinueOnCapturedContext { get; set; }
    }
}
