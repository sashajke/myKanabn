using IntroSE.Kanban.Backend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Interfaces
{
    public interface ITaskDAL
    {
        void Save();
        bool Load(string email, int columnID, int id);
        string Creationtime { get; set; }
        string DueDate { get; set; }
        int Id { get; set; }
        int ColumnID { get; set; }
        string Email { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        void Remove();
    }
}
