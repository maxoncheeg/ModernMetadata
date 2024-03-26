using ModernMetadata.Model.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ModernMetadata.View
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private IMenuMethodFactory _factory;

        public MenuView(object data)
        {
            InitializeComponent();

            _factory = new MenuMethodFactory();
            Assembly assembly =
                Assembly.LoadFrom(
                    @"C:\Users\maksg\Desktop\CODING\methodsandtech\ModernMetadata\ModernMetadata.Library\bin\Debug\net8.0\ModernMetadata.Library.dll");
            Type type = assembly.GetTypes().First(type => type.Name == "MenuConfigReader");

            object reader = Activator.CreateInstance(type, ["menuConfig.txt"]) ?? throw new DllNotFoundException();
            
            object? res = type.GetMethod("ReadMenuData")?.Invoke(reader, [data]);

            if (res == null) return;
            foreach (var item in (res.GetType().GetProperty("Menus")?.GetValue(res) as IReadOnlyCollection<object>)!)
            {
                if ((item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>) == null
                    || ((item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>)!)
                    .Count == 0)
                {
                    var newMenu = new MenuItem()
                        { Header = (item.GetType().GetProperty("Name")?.GetValue(item)?.ToString()) };
                    if (item.GetType().GetProperty("Method")?.GetValue(item) != null)
                        newMenu.Click +=
                            _factory.GetMenuMethod(item.GetType().GetProperty("Method")?.GetValue(item)?.ToString());

                    menu.Items.Add(newMenu);
                }
                else
                    menu.Items.Add(GetMenues(item.GetType().GetProperty("Name")?.GetValue(item)?.ToString(),
                        (item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>)));
            }
        }

        /// <summary>
        /// Создание компонентов меню.
        /// </summary>
        private MenuItem GetMenues(string? name, IReadOnlyCollection<object>? data)
        {
            MenuItem menu1 = new() { Header = name };
            foreach (var item in data)
            {
                if ((item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>) == null
                    || ((item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>)!).Count == 0)
                {
                    var newMenu = new MenuItem()
                        { Header = (item.GetType().GetProperty("Name")?.GetValue(item)?.ToString()) };
                    if (item.GetType().GetProperty("Method")?.GetValue(item) != null)
                        newMenu.Click +=
                            _factory.GetMenuMethod(item.GetType().GetProperty("Method")?.GetValue(item)?.ToString());

                    menu1.Items.Add(newMenu);
                }
                else
                    menu1.Items.Add(GetMenues(item.GetType().GetProperty("Name")?.GetValue(item)?.ToString(),
                        (item.GetType().GetProperty("InnerMenus")?.GetValue(item) as IReadOnlyCollection<object>)));
            }

            return menu1;
        }
    }
}