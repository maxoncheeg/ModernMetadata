using ModernMetadata.Model.Metadata.Users;
using System.IO;

namespace ModernMetadata.Model.Metadata.Readers
{
    public class UserConfigReader : IUserConfigReader
    {
        private string _fileName;
        public UserConfigReader(string fileName)
        {
            _fileName = fileName;
        }

        public IUserMenuData? ReadUserMenuData(string name, string password)
        {
            if (!File.Exists(_fileName)) throw new FileNotFoundException(_fileName);

            using StreamReader streamReader = new(_fileName);
            Dictionary<string, ItemStatus> itemsConfig = new();
            string? line = "";

            while ((line = streamReader.ReadLine()) != null)
                if (line[0] == '#')
                {
                    line = line.Substring(1);
                    string[] words = line.Split(' ');

                    if (words[0] == name && words[1] == password)
                    {
                        while ((line = streamReader.ReadLine()) != null && line[0] != '#')
                        {
                            string[] configs = line.Split(' ');
                            itemsConfig.Add(configs[0], (ItemStatus)int.Parse(configs[1]));
                        }
                        return new UserMenuData(itemsConfig);
                    }
                }

            return null;
        }
    }
}
