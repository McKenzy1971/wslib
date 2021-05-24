using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace wslib.WPF.Core
{
    /// <summary>
    /// Base class of ViewModels. Implementing INotifyPropertyChanged.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that occors when a Property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes PropertyChanged event when it isn't null.
        /// </summary>
        /// <param name="propertyName">Name of Callmember</param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}