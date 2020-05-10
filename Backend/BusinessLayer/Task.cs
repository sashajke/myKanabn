using IntroSE.Kanban.Backend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Task
    {
        private const int maxLenghDescription = 300;
        private const int maxLenghTitle = 50;
        private int id;
        private string title;
        private string description;
        private ColumnStatus status = ColumnStatus.Backlog; // need to change
        private string email;
        private DateTime creationtime;
        private DateTime dueDate;

        public Task(int id, string title, string description, string email, DateTime dueDate)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.email = email;
            this.creationtime = DateTime.Now;
            this.dueDate = dueDate;
            save(); 
        }
        public Task() { }
        public Task(DataAccessLayer.TaskWrapper toCopy)
        {
            if (toCopy == null)
                return;
            Load(toCopy);
        }
        public int Id { get => id; }
        public DateTime Creationtime { get => creationtime; }
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                if(!dueDate.Equals(value))
                {
                    dueDate = value;
                    save();
                }
                
            }
        }
        public string Description { get => description;}
        public string Title { get => title;}
        public ColumnStatus Status
        {
            get => status;
            set
            {
                if(!status.Equals(value))
                {
                    status = value;
                    save();
                }
                
            }
        }

        public bool updateTaskDueDate(DateTime dueDate)
        {
            if (this.creationtime > dueDate)
                throw new Exception("wrong date");
            this.dueDate = dueDate;
            save();
            return true;
        }
        public bool updateTaskTitle(string title)
        {
            if (title.Length > maxLenghDescription || title.Length < 1)
                throw new Exception("title must be between 1 to 50 chars");
            this.title = title;
            save();
            return true;
        }
        public bool updateTaskDescription(string description)
        {
            if (description.Length > maxLenghDescription)
                throw new Exception("description is too long");
            this.description = description;
            save();
            return true;
        }
        public DataAccessLayer.TaskWrapper ToDalObject()
        {
            return new DataAccessLayer.TaskWrapper(this.id, this.title, this.description, this.email, this.creationtime, this.dueDate, this.status);
        }
        public void save()
        {
            this.ToDalObject().Save();
        }
        public void Load(DataAccessLayer.TaskWrapper taskWrap)
        {
            this.creationtime = taskWrap.Creationtime;
            this.description = taskWrap.Description;
            this.dueDate = taskWrap.DueDate;
            this.id = taskWrap.Id;
            this.email = taskWrap.Email;
            this.title = taskWrap.Title;
        }
        public override string ToString()
        {
            return $"Creation={this.creationtime} ID ={this.id} Status={this.status.ToString()}";
        }
    }
}
