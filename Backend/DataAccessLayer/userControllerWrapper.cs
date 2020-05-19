using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class userControllerWrapper : DalObject<userControllerWrapper>, IDALController<IUserDAL>
    {
        
        public List<IUserDAL> Items { get ; set ; }

        public userControllerWrapper()
        {
            Items = new List<IUserDAL>();
        }
        public void RemoveAll()
        {
            File.Delete(GetDirectory());
        }
        public void Save()
        {
            foreach (var item in Items)
            {
                item.Save();                
            }
        }
        public bool Load()
        {
            if (Items == null)
                Items = new List<IUserDAL>();
            Items.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    UserDalFile toLoad = FromJson<UserDalFile>(item);
                    Items.Add(toLoad);
                }
                catch(Exception ee)
                {

                }
            }
            return true;
        }
        private string GetFileName(string email)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Users");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, email + ".json");
        }
        private string GetDirectory()
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Users");
            return dir;
        }
    }
}
