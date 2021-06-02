using System;
using System.Windows.Input;

namespace wslib.WPF.Core
{
    /// <summary>
    /// Defines a command. Implementing ICommand.
    /// </summary>
    public class DelegateCommand : ICommand
    {

        public DelegateCommand(Action executeHandler, Predicate<bool> canExecuteHandler)
        {
            this._executeHandler = executeHandler;
            this._canExecuteHandler = canExecuteHandler;
            if (this._executeHandler == null)
                throw new ArgumentNullException(nameof(executeHandler), "ExecuteHandler can't be null. Please specify the Command");
        }
        #region Handlers


        public event EventHandler CanExecuteChanged;
        private readonly Action _executeHandler;
        private readonly Predicate<bool> _canExecuteHandler;
        #endregion
        #region Methods

        public bool CanExecute(object parameter) => this._canExecuteHandler == null || this._canExecuteHandler((bool)parameter) == true;

        public void Execute(object parameter) => this._executeHandler();

        public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion
    }

    /// <summary>
    /// Defines a command. Implementing ICommand.
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => throw new NotImplementedException();

        public void Execute(object parameter) => throw new NotImplementedException();
    }
}