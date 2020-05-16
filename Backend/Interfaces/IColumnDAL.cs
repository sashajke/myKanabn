using IntroSE.Kanban.Backend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Interfaces
{
    public interface IColumnDAL
    {
        void Save();
        bool Load(string email, int id);    
        string Email { get; set; }
        string Name { get; set; }
        int Limit { get; set; }
        int OrderID { get; set; }
        void Remove();          
    }
}
