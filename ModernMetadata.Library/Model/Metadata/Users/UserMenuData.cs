namespace ModernMetadata.Library.Model.Metadata.Users
{
    public class UserMenuData : IUserMenuData
    {
        public IReadOnlyDictionary<string, ItemStatus> ItemsConfig {  get; }

        public UserMenuData(Dictionary<string, ItemStatus> itemsConfig)
        {
            ItemsConfig = itemsConfig;
        }
    }
}
