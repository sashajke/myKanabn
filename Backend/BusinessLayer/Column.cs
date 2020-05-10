using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Column
    {
        private string name;
        private int limit;
        private List<Task> taskByID;
        private ColumnStatus status;
        private string email;

        public Column(ColumnStatus status,string email)
        {
            this.email = email;
            this.limit = -1;
            this.taskByID = new List<Task>();
            switch (status)
            {
                case ColumnStatus.Backlog:
                    this.name = "backlog";
                    break;
                case ColumnStatus.InProgress:
                    this.name = "in progress";
                    break;
                case ColumnStatus.Done:
                    this.name = "done";

                    break;
                default:
                    break;
            }
            this.status = status;
            save();
        }
        public Column()
        {

        }
        public Column(DataAccessLayer.ColumnDalFile toCopy,List<Task> tasks)
        {
            if (toCopy == null)
                return;
            this.email = toCopy.Email;
            switch (toCopy.Status)
            {
                case ColumnStatus.Backlog:
                    this.name = "backlog";
                    break;
                case ColumnStatus.InProgress:
                    this.name = "in progress";
                    break;
                case ColumnStatus.Done:
                    this.name = "done";

                    break;
                default:
                    break;
            }
            this.limit = toCopy.Limit;
            this.status = toCopy.Status;
            this.taskByID = tasks;
        }
        public int Limit {
            get => limit;
            set
            {
                if(limit!=value)
                {
                    limit = value;
                    save();
                }
                
            }
        }
        internal List<Task> TaskByID { get => taskByID; }
        public string Name { get => name; }

        //updates the limit
        public bool updateLimit(int lim)
        {
            if (lim != Limit)
            {
                if (lim < taskByID.Count && lim != -1)
                    throw new Exception("the column already contain more  than" + lim + " tasks");
                if (lim < 0 && lim != -1)
                    throw new Exception("limit value is illegal");
                this.limit = lim;
                save();
                return true;
            }
            return false;
        }
        //add a new task to the curr Column
        public bool addTask(Task newTask)
        {
            if (this.limit != -1)
            {
                if (this.limit == this.taskByID.Count)
                    throw new Exception("column has reached its max tasks");
            }
            newTask.Status = status;
            taskByID.Add(newTask);
            return true;
        }
        //Remove task from the column
        public Task removeTask(int taskID)
        {
            Task deleted = GetTaskByID(taskID);
            taskByID.Remove(GetTaskByID(taskID));
            return deleted;
        }
        public Task GetTaskByID(int ID)
        {
            foreach (Task val in taskByID)
            {
                if (val.Id == ID)
                    return val;
            }   
            throw new Exception("There is no Task");
        }
        //override
        public IColumnDAL ToDalObject()
        {
            return Factory.CreateColumnDalImpl(this.limit, this.email, this.status);
            //return new DataAccessLayer.ColumnDalFile(this.limit,this.email, this.status);
        }
        public void save()
        {
            this.ToDalObject().Save();
        }
    }
}
