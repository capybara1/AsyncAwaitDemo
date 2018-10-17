using System;
using System.Windows.Input;

namespace AsyncAwait.WpfApplication.ViewModel.Commands
{
    internal abstract class Command<TParameter> : ICommand
    {
        public abstract bool CanExecute(TParameter parameter);

        public abstract void Execute(TParameter parameter);

        public bool CanExecute(object parameter)
        {
            if (!(parameter is TParameter typedParameter)) return false;
            return CanExecute(typedParameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            Execute((TParameter)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    internal abstract class Command : ICommand
    {
        public abstract bool CanExecute();

        public abstract void Execute();

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute(object parameter)
        {
            Execute();
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
