using System.Windows;

namespace ModernMetadata.Model.Metadata.MenuData
{
    public class MenuItemData : IMenuItemData
    {
        private string _name;
        private RoutedEventHandler? _method;
        private List<IMenuItemData>? _innerMenus;

        public string Name => _name;
        public RoutedEventHandler? Method => _method;
        public IReadOnlyCollection<IMenuItemData>? InnerMenus => _innerMenus;

        /// <summary>
        ///  Конструктор с методом и вложенными меню.
        /// </summary>
        
        public MenuItemData(string name, RoutedEventHandler method, List<IMenuItemData> innerMenus)
        {
            _name = name;
            _method = method;
            _innerMenus = innerMenus;
        }

        /// <summary>
        ///  Конструктор с вложенными меню.
        /// </summary>

        public MenuItemData(string name, List<IMenuItemData> innerMenus)
        {
            _name = name;
            _method = null;
            _innerMenus = innerMenus;
        }

        /// <summary>
        ///  Конструктор с методом.
        /// </summary>

        public MenuItemData(string name, RoutedEventHandler? method)
        {
            _name = name;
            _method = method;
            _innerMenus = null;
        }

        public MenuItemData(string name)
        {
            _name = name;
            _method = null;
            _innerMenus = null;
        }

        /// <summary>
        /// Добавление внутреннего меню.
        /// </summary>

        public void AddInnerMenu(IMenuItemData item)
        {
            if (_innerMenus == null)
                _innerMenus = new List<IMenuItemData>();
            _innerMenus.Add(item);
        }
    }
}
