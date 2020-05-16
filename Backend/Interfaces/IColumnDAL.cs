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
        bool Load(string email, ColumnStatus status);    
        string Email { get; set; }
        ColumnStatus Status { get; set; }
        int Limit { get; set; }
        int OrderID { get; set; }
    }
}
