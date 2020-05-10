using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskControllerWrapper : DalObject<userControllerWrapper>
    {


        public List<TaskWrapper> Tasks { get ; set ; }

        public TaskControllerWrapper()
        {
            Tasks = new List<TaskWrapper>();
        }
        public override void Save()
        {
            
        }
        public void SaveAsSeperateFiles()
        {
            foreach (var item in Tasks)
            {
                item.Save();                
            }
        }
        public bool LoadAsSeperateFiles()
        {
            if (Tasks == null)
                Tasks = new List<TaskWrapper>();
            Tasks.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    TaskWrapper toLoad = FromJson<TaskWrapper>(item);
                    Tasks.Add(toLoad);
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
