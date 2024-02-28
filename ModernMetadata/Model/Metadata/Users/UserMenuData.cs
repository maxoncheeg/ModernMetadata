using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernMetadata.Model.Metadata.Users
{
    public class UserMenuData : IUserMenuData
    {
        public UserMenuData(Dictionary<string,ItemStatus> itemsConfig) 
        {
            ItemsConfig = itemsConfig;
        }
        public IReadOnlyDictionary<string, ItemStatus> ItemsConfig {  get; }
    }
}
