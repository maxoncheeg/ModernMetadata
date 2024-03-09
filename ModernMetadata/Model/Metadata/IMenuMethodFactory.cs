using System.Windows;

namespace ModernMetadata.Model.Metadata
{
    /// <summary>
    /// Создание метода на основе делегата по имени метода.
    /// </summary>
    public interface IMenuMethodFactory
    {
        public RoutedEventHandler GetMenuMethod(string name);
    }
}
