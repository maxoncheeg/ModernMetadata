using System.Windows;

namespace ModernMetadata.Model.Metadata.MenuData
{
    public interface IMenuItemData
    {
        public string Name { get; }
        public RoutedEventHandler? Method { get; }
        public IReadOnlyCollection<IMenuItemData>? InnerMenues { get; }

        public void AddInnerMenu(IMenuItemData item);
    }
}
