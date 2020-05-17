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
         
        public TaskDalDB(int id,string title, string desc, string email,DateTime creation,DateTime due,int ColId)
        {
            this.Id = id;
            this.Title = title;
            this.Description = desc;
            this.Email = email;
            this.Creationtime = creation;
            this.DueDate = due;
        }
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
