using ModernMetadata.Model.Metadata.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Dictionary<string, ItemStatus> itemsConfig = new();
            if (File.Exists(_fileName))
            {
                using StreamReader streamReader = new(_fileName);

                string? line = "";

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line[0] == '#')
                    {
                        line = line.Substring(1);
                        string[] words = line.Split(' ');

                        if (words[0] == name)
                        {
                            if (words[1] == password)
                            {
                                while ((line = streamReader.ReadLine()) != null && line[0] != '#')
                                {
                                    string[] configs = line.Split(' ');
                                    itemsConfig.Add(configs[0], (ItemStatus)int.Parse(configs[1]));
                                }
                                return new UserMenuData(itemsConfig);
                            }
                            else return null;
                        }
                        else return null;
                    }
                    else return null;
                }
                return null;
            }
            else return null;
        }
    }
}
