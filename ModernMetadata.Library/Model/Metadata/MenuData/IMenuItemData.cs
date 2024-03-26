using System.Windows;

namespace ModernMetadata.Library.Model.Metadata.MenuData
{
    /// <summary>
    /// Данные компонентов меню.
    /// </summary>
    
    public interface IMenuItemData
    {
        public string Name { get; }
        public string? Method { get; }
        public IReadOnlyCollection<IMenuItemData>? InnerMenus { get; }

        /// <summary>
        /// Добавление компонентов подменю.
        /// </summary>
        
        public void AddInnerMenu(IMenuItemData item);
    }
}
