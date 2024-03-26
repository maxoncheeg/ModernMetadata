
using ModernMetadata.Model.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ModernMetadata.Library.Model.Metadata.MenuData;
using ModernMetadata.Library.Model.Metadata.Readers;
using ModernMetadata.Library.Model.Metadata.Users;

namespace ModernMetadata.View
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private IMenuMethodFactory _factory;
        public MenuView(IUserMenuData data)
        {
            InitializeComponent();

            _factory = new MenuMethodFactory();
            IMenuConfigReader reader = new MenuConfigReader();
            var res = reader.ReadMenuData(data);

            foreach ( var item in res.Menus)
            {
                if (item.InnerMenus == null || item.InnerMenus.Count == 0) 
                {
                    var newMenu = new MenuItem() { Header = item.Name };
                    if(item.Method != null)
                        newMenu.Click += _factory.GetMenuMethod(item.Method);
 
                    menu.Items.Add(newMenu);
                }
                else
                    menu.Items.Add(GetMenues(item.Name, item.InnerMenus));
            }
        }

        /// <summary>
        /// Создание компонентов меню.
        /// </summary>

        private MenuItem GetMenues(string name, IReadOnlyCollection<IMenuItemData> data)
        {
            MenuItem item = new() { Header = name };
            foreach (IMenuItemData menu in data)
            {
                if (menu.InnerMenus == null || menu.InnerMenus.Count == 0)
                {
                    var newMenu = new MenuItem() { Header = menu.Name };
                    if (menu.Method != null)
                        newMenu.Click += _factory.GetMenuMethod(menu.Method);

                    item.Items.Add(newMenu);
                }
                else
                    item.Items.Add(GetMenues(menu.Name, menu.InnerMenus));
            }
            return item;
        }
    }
}
