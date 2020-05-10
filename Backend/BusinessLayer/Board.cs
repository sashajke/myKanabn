using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.DataAccessLayer;
namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Board
    {
        private const int maxLenghDescription = 300;
        private const int maxLenghTitle = 50;
        private List<Column> columns;
        private string email;
        private List<string> columnNames;
        public List<Column> Columns { get => columns; set => columns = value; }
        public List<string> ColumnNames { get => columnNames; set => columnNames = value; }
        internal Board(string email)
        {
            this.columnNames = new List<string>();
            this.columns = new List<Column>();
            for (int i = 0; i < 3; i++)
            {
                Column col = new Column((ColumnStatus)i, email);
                this.columns.Add(col);
                this.columnNames.Add((col.Name));
            }
            this.email = email;
        }
        public Board() { }
        public Board(string email,List<Column> columns,List<string>columnNames)
        {
            this.email = email;
            this.columns = columns;
            this.columnNames = columnNames;
        }
        public Column GetColumn(int ColumnOrdinal)
        {
            if ((ColumnOrdinal < 0 || ColumnOrdinal > 2))
                throw new Exception("Illeagal ColumnOrdinal");
            return columns[ColumnOrdinal];
        }
        public Column GetColumn(string ColumnName)
        {
            if (string.IsNullOrEmpty(ColumnName))
                return null;
            Column returnColumn = columns.Find(column => column.Name == ColumnName);
            return returnColumn;
        }
        public Task CreateNewTask(string title, string des, DateTime dueDate)
        {
            if(string.IsNullOrEmpty(title))
                throw new Exception("title can't be null");
            if (title.Length > maxLenghTitle || title.Length < 1)
                throw new Exception("title must be between 1 to 50 chars");
            //if(des == null)
            //    throw new Exception("description is null");
            if(des !=null && des.Length > maxLenghDescription)
                throw new Exception("description is too long");
            if(dueDate == null)
                throw new Exception("due date is null");
            if (dueDate <= DateTime.Now)
                throw new Exception("due date not valid");
            int ID = getNextID();
            if (!this.GetColumn(0).addTask(new Task(ID, title, des, this.email, dueDate)))
                throw new Exception("leftmost culumn has reached its maximum amount of tasks allowed limit");
            return this.columns[0].GetTaskByID(ID);
        }
        private int getNextID()
        {
            int counter = 0;
            foreach (var item in Columns)
            {
                counter += item.TaskByID.Count;
            }
            return counter;
        }
        public void advanceTask(int columnOrdinal, int taskID)
        {
            if(columnOrdinal > 2 || columnOrdinal < 0)
                throw new Exception("illegal column id ");

            if (columnOrdinal == 2)
                throw new Exception("can't advance tasks that under 'done' column ");
            if (columns[columnOrdinal + 1].Limit == columns[columnOrdinal + 1].TaskByID.Count)
                throw new Exception("the next column have reached to the limit");
            Task taskToRemove = columns[columnOrdinal].removeTask(taskID);
            columns[columnOrdinal + 1].addTask(taskToRemove);
        }
    }
}
