using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{

    public class ColumnDalFile : DalObject<ColumnDalFile>,IColumnDAL
    {

        public ColumnStatus Status { get; set; }
        public string Name { get; set ; }
        public int Limit { get; set; }
        public string Email { get; set; }

        public ColumnDalFile()
        {
        }
        public ColumnDalFile(int limit,string email, ColumnStatus status)
        {
            Email = email;
            switch (status)
            {
                case ColumnStatus.Backlog:
                    this.Name = "backlog";
                    break;
                case ColumnStatus.InProgress:
                    this.Name = "in progress";
                    break;
                case ColumnStatus.Done:
                    this.Name = "done";

                    break;
                default:
                    break;
            }
            Limit = limit;
            Status = status;
        }

        override public void Save()
        {
            ToJson(this, GetFileName(Email,Status));
        }
        public bool Load(string email, ColumnStatus status)
        {
            ColumnDalFile toLoad = null;
            try
            {
                toLoad = FromJson<ColumnDalFile>(GetFileName(email,status));

            }
            catch (Exception ee)
            {
                return false;
            }
            if (toLoad == null)
                return false;
            Email = toLoad.Email;
            Name = toLoad.Name;
            Limit = toLoad.Limit;
            Status = toLoad.Status;
            return true;
        }
        private string GetFileName(string email,ColumnStatus status)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Columns");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, email +"_"+ status.ToString()+ ".json");
        }
    }
}
