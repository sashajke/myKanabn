using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Interfaces
{
    public interface IUserDAL
    {
        void Save();
        bool Load(string email);
        string Password { get; set; }
        string Nickname { get; set; }
        bool IsLogged { get; set; }
        string Email { get; set; }
    }
}
