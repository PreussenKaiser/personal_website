using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PKaiser.Web.ViewModels;

/// <summary>
/// Represents the a base view model.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Handles component changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Notifies the view that a property has changed.
    /// </summary>
    /// <param name="propertyName">The property which changed.</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Sets a property and notifies that it has changed.
    /// </summary>
    /// <typeparam name="T">The property's type.</typeparam>
    /// <param name="backingStore">The property's backing field.</param>
    /// <param name="value">The value to set the backing field to.</param>
    /// <param name="propertyName">The property which changed.</param>
    protected virtual void SetProperty<T>(
        ref T backingStore, T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return;

        backingStore = value;

        this.OnPropertyChanged(propertyName);
    }
}
