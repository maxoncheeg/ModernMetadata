using ModernMetadata.Library.Model.Metadata.MenuData;
using ModernMetadata.Library.Model.Metadata.Users;

namespace ModernMetadata.Library.Model.Metadata.Readers
{
    public interface IMenuConfigReader
    {
        /// <summary>
        /// Чтение данных о меню из файла меню.
        /// </summary>
         
        public IMenuData ReadMenuData(IUserMenuData userMenuData);
    }
}
