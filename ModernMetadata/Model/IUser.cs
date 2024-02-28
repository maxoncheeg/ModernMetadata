using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernMetadata.Model
{
    public enum ItemStatus
    {
        Visible,
        NotEnabledVisible,
        NotVisible
    }
    public interface IUserMenuData
    {
        IReadOnlyDictionary<string, ItemStatus> ItemsConfig { get; }
    }

    public interface IMenuData
    {
        IReadOnlyDictionary<string, RoutedEventHandler> ItemsConfig { get; }
    }

    public class MenuData : IMenuData
    {
        private Dictionary<string, RoutedEventHandler> _dictionary;

        public MenuData(Dictionary<string, RoutedEventHandler> dictionary)
        {
            _dictionary = dictionary;
        }

        public IReadOnlyDictionary<string, RoutedEventHandler> ItemsConfig => _dictionary;
    }

    public interface IItemMethodsFactory
    {
        public RoutedEventHandler GetMethod(string name);
    }
}
