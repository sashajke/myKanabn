using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskControllerWrapper : DalObject<userControllerWrapper>, IDALController<ITaskDAL>
    {


        public List<ITaskDAL> Items { get ; set ; }

        public TaskControllerWrapper()
        {
            Items = new List<ITaskDAL>();
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
                Items = new List<ITaskDAL>();
            Items.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    TaskDalFile toLoad = FromJson<TaskDalFile>(item);
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
            dir = Path.Combine(dir, "Tasks");
            return dir;
        }
    }
}
