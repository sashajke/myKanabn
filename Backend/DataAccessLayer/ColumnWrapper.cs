using IntroSE.Kanban.Backend.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{

    public class ColumnWrapper:DalObject<ColumnWrapper>
    {

        public ColumnStatus Status { get; set; }
        public string Name { get; set ; }
        public int Limit { get; set; }
        public string Email { get; set; }

        public ColumnWrapper()
        {
        }
        public ColumnWrapper(int limit,string email, ColumnStatus status)
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
            ColumnWrapper toLoad = null;
            try
            {
                toLoad = FromJson<ColumnWrapper>(GetFileName(email,status));

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
