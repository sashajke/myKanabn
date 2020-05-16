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

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime DueDate { get; set; }
        public int ColumnID { get; set; }
         

        public bool Load(string email, int orderId, int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        public void Remove()
        {

        }
    }
}
