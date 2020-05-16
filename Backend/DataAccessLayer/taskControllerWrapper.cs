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


        public List<TaskDalFile> Tasks { get ; set ; }

        public TaskControllerWrapper()
        {
            Tasks = new List<TaskDalFile>();
        }
        public override void Save()
        {
            
        }

        public void RemoveAll()
        {
            File.Delete(GetDirectory());
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
                Tasks = new List<TaskDalFile>();
            Tasks.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    TaskDalFile toLoad = FromJson<TaskDalFile>(item);
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
