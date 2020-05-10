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

        public static IUserDAL CreateUserDalImpl()
        {
            if (CurrentConfig == SavingSystem.File)
                return new UserDalFile();
            return new UserDalDB();
        }
        public static ITaskDAL CreateTaskDalImpl()
        {
            if (CurrentConfig == SavingSystem.File)
                return new TaskDalFile();
            return new TaskDalDB();
        }
        public static IColumnDAL CreateColumnDalImpl()
        {
            if (CurrentConfig == SavingSystem.File)
                return new ColumnDalFile();
            return new ColumnDalDB();
        }
        public static IBoardDAL CreateBoardDalImpl()
        {
            //if (CurrentConfig == SavingSystem.File)
            //    return new BoardDalFile();
            return new BoardDalDB();
        }

    }
}
