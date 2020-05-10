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


        public List<ColumnWrapper> Columns { get ; set ; }

        public ColumnControllerWrapper()
        {
            Columns = new List<ColumnWrapper>();
        }
        public void SaveAsSeperateFiles()
        {
            foreach (var item in Columns)
            {
                item.Save();                
            }
        }
        public bool LoadAsSeperateFiles()
        {
            if (Columns == null)
                Columns = new List<ColumnWrapper>();
            Columns.Clear();
            foreach (var item in Directory.GetFiles(GetDirectory()))
            {
                try
                {
                    ColumnWrapper toLoad = FromJson<ColumnWrapper>(item);
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
