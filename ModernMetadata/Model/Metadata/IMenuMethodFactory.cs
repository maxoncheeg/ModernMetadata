using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernMetadata.Model.Metadata
{
    public interface IMenuMethodFactory
    {
        public RoutedEventHandler GetMenuMethod(string name);
    }
}
