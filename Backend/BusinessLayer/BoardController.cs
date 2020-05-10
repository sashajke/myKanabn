using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class BoardController
    {
        private int taskCount;
        private UserController _userController;
        private List<Board> AllBoards;

        public BoardController(UserController userController)
        {
            this.taskCount = 0;
            this._userController = userController;
        }

        private bool validUser(User check)
        {
            if (check == null)
                throw new Exception("there is no such user");
            if (!check.IsLogged)
                throw new Exception("user is offline");
            return true;
        }
        public void LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            if (columnOrdinal == 0 || columnOrdinal == 2)
                throw new Exception("can't update backlog or done column limit");
            if (!(check.Board.GetColumn(columnOrdinal).updateLimit(limit)))
                throw new Exception("limit must be positive");
        }
        public Column GetColumn(string email, string name)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            Column toReturn = check.Board.GetColumn(name);
            if (toReturn == null)
                throw new Exception("column name is incorrect");
            return toReturn;
        }
        public Column GetColumn(string email, int columnOrdinal)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            return check.Board.GetColumn(columnOrdinal);
        }
        public IReadOnlyCollection<string> GetBoard(string email)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            var readOnlyNames = new ReadOnlyCollection<string>(check.Board.ColumnNames);
            return readOnlyNames;
        }
    }
}
