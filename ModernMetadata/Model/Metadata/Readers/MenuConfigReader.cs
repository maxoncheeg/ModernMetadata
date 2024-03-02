using ModernMetadata.Model.Metadata.MenuData;
using ModernMetadata.Model.Metadata.Users;
using System.IO;

namespace ModernMetadata.Model.Metadata.Readers
{
    public class MenuConfigReader : IMenuConfigReader
    {
        private string _fileName;
        private IMenuMethodFactory _factory;

        public MenuConfigReader(IMenuMethodFactory factory, string fileName = "menuConfig.txt")
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            _fileName = fileName;
            _factory = factory;
        }

        public IMenuData ReadMenuData(IUserMenuData userMenuData)
        {
            string? line;
            using StreamReader reader = new(_fileName);

            List<IMenuItemData> temp = [];
            List<IMenuItemData> menu = [];
            int lastNotVisibleId = -1;

            while ((line = reader.ReadLine()) != null)
            {
                string[] strings = line.Split(' ');

                if (strings.Length < 0)
                    throw new IOException("cannot find information");
                if (!int.TryParse(strings[0], out var id))
                    throw new IOException("cannot find information");

                string name = strings[1];
                int zeroIndex = 2;

                for (int i = 2; strings[i][0] != '0'; ++i, zeroIndex = i)
                {
                    name += " ";
                    name += strings[i];
                }

                bool isStatusExist = userMenuData.ItemsConfig.TryGetValue(name, out var status);

                if (isStatusExist && status == ItemStatus.NotVisible)
                {
                    lastNotVisibleId = id;
                    continue;
                }
                if (lastNotVisibleId != -1 && lastNotVisibleId < id) continue;
                else lastNotVisibleId = -1;

                if (strings.Length == zeroIndex + 1)
                {
                    if (id != 0)
                    {
                        if (id <= temp.Count - 1)                    
                            temp = temp[..id];  
                        
                        temp.Add(new MenuItemData(name));
                        temp[id - 1].AddInnerMenu(temp[id]);
                    }
                    else
                        temp.Add(new MenuItemData(name));
                }
                else
                {
                    string methodName = strings[zeroIndex + 1];
                    var method = isStatusExist && status == ItemStatus.NotEnabledVisible ? null : _factory.GetMenuMethod(methodName);

                    if (temp.Count == 0)
                        menu.Add(new MenuItemData(name, method));
                    else if (id == 0)
                    {
                        menu.Add(temp[0]);
                        temp.Clear();
                        menu.Add(new MenuItemData(name, method));
                    }
                    else
                        temp[id - 1].AddInnerMenu(new MenuItemData(name, method));
                }
            }

            if (temp.Count > 0)
                menu.Add(temp[0]);

            return new MenuData.MenuData(menu);
        }
    }
}
