using System.Threading.Tasks;

namespace AsyncAwait.WpfApplication.ViewModel.Commands
{
    internal abstract class AsyncCommand<TParameter> : Command<TParameter>, IAsyncCommand
    {
        public abstract Task ExecuteAsync(TParameter parameter);

        public override async void Execute(TParameter parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            await ExecuteAsync((TParameter)parameter);
        }
    }

    internal abstract class AsyncCommand : Command, IAsyncCommand
    {
        public abstract Task ExecuteAsync();

        public override async void Execute()
        {
            await ExecuteAsync();
        }

        public async Task ExecuteAsync(object parameter)
        {
            await ExecuteAsync();
        }
    }
}
