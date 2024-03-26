namespace ModernMetadata.Library.Model.Metadata.Users
{
    /// <summary>
    /// Статусы отображения и видимости компонентов меню.
    /// </summary>

    public enum ItemStatus
    {
        Visible,
        NotEnabledVisible,
        NotVisible
    }

    /// <summary>
    /// Данные меню пользователя.
    /// </summary>

    public interface IUserMenuData
    {
        IReadOnlyDictionary<string, ItemStatus> ItemsConfig { get; }
    }
}
