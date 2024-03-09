using ModernMetadata.Model.Metadata.MenuData;
using ModernMetadata.Model.Metadata.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernMetadata.Model.Metadata.Readers
{
    public interface IMenuConfigReader
    {
        /// <summary>
        /// Чтение данных о меню из файла меню.
        /// </summary>
         
        public IMenuData ReadMenuData(IUserMenuData userMenuData);
    }
}
