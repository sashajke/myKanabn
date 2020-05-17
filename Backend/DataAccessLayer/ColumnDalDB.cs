using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class ColumnDalDB : IColumnDAL
    {
        public string Email { get; set ; }
        public int Limit { get; set; }
        public int OrderID { get; set ; }
        public string Name { get ; set ; }

        public ColumnDalDB(string email, int id, int lim,string name)
        {
            this.Email = email;
            this.OrderID = id;
            this.Limit = lim;
            this.Name = name;
        }

        public bool Load(string email, int columnID)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
