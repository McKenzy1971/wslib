using System;
using System.Windows.Input;

namespace wslib.WPF.Core
{
    /// <summary>
    /// Represents a command using the <see cref="ICommand"/> interface.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executeHandler">Encapsulates the method that is executed when <see cref="Execute(object)"/>is called.</param>
        /// <param name="canExecuteHandler">Encapsulates the method that is executed when <see cref="CanExecute(object)"/>is called.</param>
        public DelegateCommand(Action executeHandler, Predicate<bool> canExecuteHandler)
        {
            this._executeHandler = executeHandler;
            this._canExecuteHandler = canExecuteHandler;
            if (this._executeHandler == null)
                throw new ArgumentNullException(nameof(executeHandler), "ExecuteHandler can't be null. Please specify the Command");
        }

        #region Handlers
        /// <summary>
        /// Represents the event that occurs, when <see cref="OnCanExecuteChanged"/> is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        private readonly Action _executeHandler;
        private readonly Predicate<bool> _canExecuteHandler;
        #endregion

        #region Methods
        public bool CanExecute(object parameter) => this._canExecuteHandler == null || this._canExecuteHandler((bool)parameter) == true;
        public void Execute(object parameter) => this._executeHandler();
        /// <summary>
        /// Invokes the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        #endregion
    }

    /// <summary>
    /// Represents a command using  the <see cref="ICommand"/> interface.
    /// </summary>
    /// <typeparam name="T">The parameter type of the delegates.</typeparam>
    public class DelegateCommand<T> : ICommand
    {
        public DelegateCommand(Action<T> executeHdl, Predicate<T> canExecuteHdl)
        {
            this._executeHandler = executeHdl;
            this._canExecuteHandler = canExecuteHdl;
            if (this._executeHandler == null)
                throw new ArgumentNullException(nameof(executeHdl) + " Can't be null. Please specify the command.");
        }

        #region Handlers
        /// <summary>
        /// Represents the event that occurs, when <see cref="OnCanExecuteChanged"/> is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        private readonly Action<T> _executeHandler;
        private readonly Predicate<T> _canExecuteHandler;
        #endregion

        public bool CanExecute(object parameter) => this._canExecuteHandler == null || this._canExecuteHandler((T)parameter) == true;

        public void Execute(object parameter) => this._executeHandler((T)parameter);
        /// <summary>
        /// Invokes the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}