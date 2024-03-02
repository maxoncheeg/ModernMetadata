using ModernMetadata.Model.Metadata.Users;
using System.IO;

namespace ModernMetadata.Model.Metadata.Readers
{
    public class UserConfigReader : IUserConfigReader
    {
        private string _fileName;

        public UserConfigReader(string fileName = "userConfig.txt")
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);
            _fileName = fileName;
        }

        public IUserMenuData? ReadUserMenuData(string name, string password)
        {
            using StreamReader streamReader = new(_fileName);
            Dictionary<string, ItemStatus> itemsConfig = [];
            string? line;

            while ((line = streamReader.ReadLine()) != null)
                if (line[0] == '#')
                {
                    line = line[1..];
                    string[] words = line.Split(' ');

                    if (words[0] == name && words[1] == password)
                    {
                        while ((line = streamReader.ReadLine()) != null && line[0] != '#')
                            itemsConfig.Add(line[..(line.Length - 2)], (ItemStatus)int.Parse(line[^1..]));

                        return new UserMenuData(itemsConfig);
                    }
                }

            return null;
        }
    }
}
