using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using IntroSE.Kanban.Backend.Common;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskWrapper: DalObject<TaskWrapper>
    {

        public ColumnStatus Status { get; set; }
        public int Id { get; set ; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime DueDate { get; set; }
        public TaskWrapper() { }
        public TaskWrapper(int id, string title, string description, string email, DateTime creationtime, DateTime dueDate,ColumnStatus status)
        {
            Id = id;
            Title = title;
            Description = description;
            Email = email;
            Creationtime = creationtime;
            DueDate = dueDate;
            Status = status;
        }
        override
        public void Save()
        {
            ToJson(this, GetFileName(Email,Id));
        }
        public void Remove()
        {
            File.Delete(GetFileName(Email, Id));
        }
        public bool Load(string email,ColumnStatus status,int id)
        {
            TaskWrapper toLoad = null;
            try
            {
                toLoad = FromJson(GetFileName(email,id));

            }
            catch (Exception ee)
            {
                return false;
            }
            if (toLoad == null)
                return false;
            Id = toLoad.Id;
            Title = toLoad.Title;
            Description = toLoad.Description;
            Email = toLoad.Email;
            Creationtime = toLoad.Creationtime;
            DueDate = toLoad.DueDate;
            return true;
        }
        private string GetFileName(string email,int id)
        {
            string dir = Directory.GetCurrentDirectory();
            dir = Path.Combine(dir, "Tasks");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, email + "_"  + id.ToString() + ".json");
        }
    }
}
