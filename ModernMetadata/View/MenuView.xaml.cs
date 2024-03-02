using ModernMetadata.Model.Metadata.Readers;
using ModernMetadata.Model.Metadata.Users;
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
using ModernMetadata.Model.Metadata.MenuData;

namespace ModernMetadata.View
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        public MenuView(IUserMenuData data)
        {
            InitializeComponent();

            IMenuMethodFactory factory = new MenuMethodFactory();
            IMenuConfigReader reader = new MenuConfigReader(factory);
            var res = reader.ReadMenuData(data);

            foreach ( var item in res.Menues)
            {
                if (item.InnerMenues == null || item.InnerMenues.Count == 0) {
                    var newMenu = new MenuItem() { Header = item.Name };
                    if(item.Method != null)
                        newMenu.Click += item.Method;
 
                    menu.Items.Add(newMenu);
                }
                else
                    menu.Items.Add(GetMenues(item.Name, item.InnerMenues));
            }
        }

        private MenuItem GetMenues(string name, IReadOnlyCollection<IMenuItemData> data)
        {
            MenuItem item = new() { Header = name };
            foreach (IMenuItemData menu in data)
            {
                if (menu.InnerMenues == null || menu.InnerMenues.Count == 0)
                {
                    var newMenu = new MenuItem() { Header = menu.Name };
                    if (menu.Method != null)
                        newMenu.Click += menu.Method;

                    item.Items.Add(newMenu);
                }
                else
                    item.Items.Add(GetMenues(menu.Name, menu.InnerMenues));
            }
            return item;
        }
    }
}
