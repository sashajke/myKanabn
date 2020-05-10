using IntroSE.Kanban.Backend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Interfaces
{
    interface IColumnDAL
    {
        void Save();
        bool Load(string email, ColumnStatus status);      
    }
}
