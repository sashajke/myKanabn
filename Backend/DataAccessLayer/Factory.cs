using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Factory
    {
        public static SavingSystem CurrentConfig { get; set; }

        public static IUserDAL CreateUserDalImpl(string email, string password, string nickname, bool isLogged)
        {
            if (CurrentConfig == SavingSystem.File)
                return new UserDalFile(email,password,nickname,isLogged);
            return new UserDalDB(email, password, nickname, isLogged);
        }
        public static ITaskDAL CreateTaskDalImpl(int id, string title, string description, string email, DateTime creationtime, DateTime dueDate, int columnID)
        {
            if (CurrentConfig == SavingSystem.File)
                return new TaskDalFile(id,title,description,email,creationtime,dueDate, columnID);
            return new TaskDalDB(id, title, description, email, creationtime, dueDate, columnID);
        }
        public static IColumnDAL CreateColumnDalImpl(int limit, string email, int columnID,string name)
        {
            if (CurrentConfig == SavingSystem.File)
                return new ColumnDalFile(limit,email, columnID,name);
            return new ColumnDalDB(email, columnID, limit, name);
        }
        //public static IBoardDAL CreateBoardDalImpl()
        //{
        //    if (CurrentConfig == SavingSystem.File)
        //        return null;
        //    return new BoardDalDB();
        //}

    }
}
