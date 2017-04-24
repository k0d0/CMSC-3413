using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;

namespace Project5.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private TodoContext _context;

        public SettingRepository(TodoContext context)
        {
            _context = context;
        }

        public void Create(Setting setting)
        {
            _context.Settings.Add(setting);
            _context.SaveChanges();
        }

        public IEnumerable<Setting> GetWindow(string userName)
        {
            return _context.Settings.Where(s => s.UserName == userName);
        }

        public void Update(Setting setting, string userName)
        {
            var settingToUpdate = _context.Settings.First(t => t.UserName == userName);
            settingToUpdate.warningWindow = setting.warningWindow;
            _context.SaveChanges();
        }
    }
}
