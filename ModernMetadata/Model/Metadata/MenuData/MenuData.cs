namespace ModernMetadata.Model.Metadata.MenuData
{
    public class MenuData : IMenuData
    {
        public MenuData(List<IMenuItemData> menues)
        {
            Menues = menues;
        }

        public IReadOnlyCollection<IMenuItemData> Menues { get; }
    }
}
