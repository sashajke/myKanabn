using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDalDB : ITaskDAL
    {
        public bool Load(string email, ColumnStatus status, int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
