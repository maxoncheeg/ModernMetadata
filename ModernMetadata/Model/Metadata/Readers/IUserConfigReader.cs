using ModernMetadata.Model.Metadata.Users;


namespace ModernMetadata.Model.Metadata.Readers
{
    public interface IUserConfigReader
    {
        public IUserMenuData? ReadUserMenuData(string name, string password);
    }
}
