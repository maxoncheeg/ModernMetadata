using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernMetadata.Model.Metadata.Users
{
    public class UserMenuData : IUserMenuData
    {
        public IReadOnlyDictionary<string, ItemStatus> ItemsConfig => throw new NotImplementedException();
    }
}
