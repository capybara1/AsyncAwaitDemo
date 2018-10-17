using System;
using System.Threading.Tasks;

namespace AsyncAwait.WpfApplication.ViewModel.Commands
{
    internal class InvokeLibraryAsyncCommand : AsyncCommand
    {
        public InvokeLibraryAsyncCommand(bool continueOnCapturedContext)
        {
            ContinueOnCapturedContext = continueOnCapturedContext;
        }

        public bool ContinueOnCapturedContext { get; }

        public override bool CanExecute()
        {
            return true;
        }

        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
