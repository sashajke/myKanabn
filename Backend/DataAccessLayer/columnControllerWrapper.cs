using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnControllerWrapper : DalObject<userControllerWrapper>
    {


        public List<ColumnDalFile> Columns { get ; set ; }

        public ColumnControllerWrapper()
        {
            Columns = new List<ColumnDalFile>();
        }
        public void SaveAsSeperateFiles()
        {
            foreach (var item in Columns)
            {
                item.Save();                
            }
        }
        public void RemoveAll()
        {
            File.Delete(GetDirectory());
        }
        public bool LoadAsSeperateFiles()
        {
            if (Columns == null)
                Columns = new List<ColumnDalFile>();
            Columns.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    ColumnDalFile toLoad = FromJson<ColumnDalFile>(item);
                    Columns.Add(toLoad);
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

        public override void Save()
        {
            //throw new NotImplementedException();
        }
    }
}
