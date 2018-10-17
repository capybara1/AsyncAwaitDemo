using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncAwait.WpfApplication.ViewModel.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
