using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;

namespace Project5.Repositories
{
    public interface ISettingRepository
    {
        void Create(Setting setting);

        void Update(Setting setting, string userName);

        IEnumerable<Setting> GetWindow(string userName);
    }
}
