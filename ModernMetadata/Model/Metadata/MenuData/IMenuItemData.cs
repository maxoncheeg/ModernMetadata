using System.Windows;

namespace ModernMetadata.Model.Metadata.MenuData
{
    /// <summary>
    /// Данные компонентов меню.
    /// </summary>
    
    public interface IMenuItemData
    {
        public string Name { get; }
        public RoutedEventHandler? Method { get; }
        public IReadOnlyCollection<IMenuItemData>? InnerMenues { get; }

        /// <summary>
        /// Добавление компонентов подменю.
        /// </summary>
        
        public void AddInnerMenu(IMenuItemData item);
    }
}
