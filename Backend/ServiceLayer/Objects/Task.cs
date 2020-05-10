
using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Task
    {
        public readonly int Id;
        public readonly DateTime CreationTime;
        public readonly DateTime DueDate;
        public readonly string Title;
        public readonly string Description;
        internal Task(int id, DateTime creationTime, DateTime dueDate, string title, string description)
        {
            this.DueDate = dueDate;
            this.Id = id;
            this.CreationTime = creationTime;
            this.Title = title;
            this.Description = description;
        }
        internal Task(BusinessLayer.Task toCopy)
        {
            this.DueDate = toCopy.DueDate;
            this.Id = toCopy.Id;
            this.CreationTime = toCopy.Creationtime;
            this.Title = toCopy.Title;
            this.Description = toCopy.Description;
        }
    }
}
