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

        public string Name { get; set ; }
        public int Limit { get; set; }
        public string Email { get; set; }
        public int OrderID { get; set ; }

        public ColumnDalFile()
        {
        }
        public ColumnDalFile(int limit,string email, int orderID,string name)
        {
            Email = email;
            Name = name;
            Limit = limit;
            OrderID = orderID;
        }

        override public void Save()
        {
            ToJson(this, GetFileName(Email, OrderID));
        }


        public void Remove()
        {
            File.Delete(GetFileName(Email, OrderID));
        }

        public void RemoveAll()
        {

        }
        public bool Load(string email, int orderID)
        {
            ColumnDalFile toLoad = null;
            try
            {
                toLoad = FromJson<ColumnDalFile>(GetFileName(email,orderID));

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
            OrderID = toLoad.OrderID;
            return true;
        }
        private string GetFileName(string email,int orderID)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Columns");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, email +"_"+ orderID+ ".json");
        }
    }
}
