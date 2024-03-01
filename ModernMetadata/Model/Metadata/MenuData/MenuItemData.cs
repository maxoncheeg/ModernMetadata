using System.Windows;

namespace ModernMetadata.Model.Metadata.MenuData
{
    public class MenuItemData : IMenuItemData
    {
        private string _name;
        private RoutedEventHandler? _method;
        private List<IMenuItemData>? _innerMenues;

        public string Name => _name;
        public RoutedEventHandler? Method => _method;
        public IReadOnlyCollection<IMenuItemData>? InnerMenues => _innerMenues;

        public MenuItemData(string name, RoutedEventHandler method, List<IMenuItemData> innerMenues)
        {
            _name = name;
            _method = method;
            _innerMenues = innerMenues;
        }

        public MenuItemData(string name, List<IMenuItemData> innerMenues)
        {
            _name = name;
            _method = null;
            _innerMenues = innerMenues;
        }

        public MenuItemData(string name, RoutedEventHandler? method)
        {
            _name = name;
            _method = method;
            _innerMenues = null;
        }

        public MenuItemData(string name)
        {
            _name = name;
            _method = null;
            _innerMenues = null;
        }

        public void AddInnerMenu(IMenuItemData item)
        {
            if(_innerMenues == null)
                _innerMenues = new List<IMenuItemData>();
            _innerMenues.Add(item);
        }
    }
}
