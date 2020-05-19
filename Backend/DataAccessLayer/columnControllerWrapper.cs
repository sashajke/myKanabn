using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnControllerWrapper : DalObject<userControllerWrapper>,IDALController<IColumnDAL>
    {


        public List<IColumnDAL> Items { get ; set ; }

        public ColumnControllerWrapper()
        {
            Items = new List<IColumnDAL>();
        }
        public void Save()
        {
            foreach (var item in Items)
            {
                item.Save();                
            }
        }
        public void RemoveAll()
        {
            File.Delete(GetDirectory());
        }
        public bool Load()
        {
            if (Items == null)
                Items = new List<IColumnDAL>();
            Items.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    ColumnDalFile toLoad = FromJson<ColumnDalFile>(item);
                    Items.Add(toLoad);
                }
                catch(Exception ee)
                {

                }
            }
            return true;
        }
        private string GetDirectory()
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Columns");
            return dir;
        }
    }
}
