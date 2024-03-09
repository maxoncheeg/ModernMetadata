using ModernMetadata.Model.Metadata.Users;


namespace ModernMetadata.Model.Metadata.Readers
{
    public interface IUserConfigReader
    {
        /// <summary>
        /// Чтение данных о меню из файла пользователей.
        /// </summary>
        
        public IUserMenuData? ReadUserMenuData(string name, string password);
    }
}
