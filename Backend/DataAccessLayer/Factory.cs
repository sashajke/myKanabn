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
            return new UserDalDB();
        }
        public static ITaskDAL CreateTaskDalImpl(int id, string title, string description, string email, DateTime creationtime, DateTime dueDate, ColumnStatus status)
        {
            if (CurrentConfig == SavingSystem.File)
                return new TaskDalFile(id,title,description,email,creationtime,dueDate,status);
            return new TaskDalDB();
        }
        public static IColumnDAL CreateColumnDalImpl(int limit, string email, ColumnStatus status)
        {
            if (CurrentConfig == SavingSystem.File)
                return new ColumnDalFile(limit,email,status);
            return new ColumnDalDB();
        }
        public static IBoardDAL CreateBoardDalImpl()
        {
            if (CurrentConfig == SavingSystem.File)
                return null;
            return new BoardDalDB();
        }

    }
}
