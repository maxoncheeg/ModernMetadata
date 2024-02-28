namespace ModernMetadata.Model.Metadata.Users
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
}
