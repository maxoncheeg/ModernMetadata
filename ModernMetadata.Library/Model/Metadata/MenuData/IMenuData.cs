namespace ModernMetadata.Library.Model.Metadata.MenuData
{
    /// <summary>
    /// Данные меню.
    /// </summary>
    
    public interface IMenuData
    {
        /// <summary>
        /// 
        /// </summary>
        
        public IReadOnlyCollection<IMenuItemData> Menus { get; }
    }
}
