namespace ModernMetadata.Model.Metadata.MenuData
{
    public class MenuData : IMenuData
    {
        public MenuData(List<IMenuItemData> menus)
        {
            Menus = menus;
        }

        public IReadOnlyCollection<IMenuItemData> Menus { get; }
    }
}
