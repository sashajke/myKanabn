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
        private string email;
        private int orderID;

        public Column(string email,int id=-1)
        {
            this.email = email;
            this.limit = -1;
            this.taskByID = new List<Task>();
            this.orderID = id;
            switch (orderID)
            {
                case 0:
                    this.name = "backlog";
                    break;
                case 1:
                    this.name = "in progress";
                    break;
                case 2:
                    this.name = "done";

                    break;
                default:
                    this.name = "unknown";
                    break;
            }
            save();
        }
        
        public Column()
        {
            this.email = "";
            this.limit = -1;
            this.taskByID = new List<Task>();
            this.orderID = -1;
            this.name = "";
        }
        public Column(IColumnDAL toCopy,List<Task> tasks)
        {
            if (toCopy == null)
                return;
            this.email = toCopy.Email;
           
            this.orderID = toCopy.OrderID;
            this.limit = toCopy.Limit;
            this.taskByID = tasks;
            this.name = toCopy.Name;
        }
        public int OrderID
        {
            get => orderID;
            set
            {
                if(orderID != value)
                {
                    orderID = value;
                    save();
                }
            }
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
        public string Name
        {
            get => name;
            set
            {
                if (name != value && !string.IsNullOrEmpty(value))
                {
                    name = value;
                    save();
                }
            }
        }

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
            newTask.ColumnID = orderID;
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
            return Factory.CreateColumnDalImpl(this.limit, this.email, this.OrderID,this.Name);
            //return new DataAccessLayer.ColumnDalFile(this.limit,this.email, this.status);
        }
        public void save()
        {
            this.ToDalObject().Save();
        }
    }
}
