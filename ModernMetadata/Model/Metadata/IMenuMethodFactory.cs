﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernMetadata.Model.Metadata
{
    /// <summary>
    /// Создание метода на основе делегата по имени метода.
    /// </summary>

    public interface IMenuMethodFactory
    {
        public RoutedEventHandler GetMenuMethod(string name);
    }
}
