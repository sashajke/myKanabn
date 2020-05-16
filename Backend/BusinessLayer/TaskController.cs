using System;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class TaskController
    {
        private UserController _userController;
        public TaskController(UserController userController)
        {
            _userController = userController;
        }
        private bool validUser(User check)
        {
            if (check == null)
                throw new Exception("there is no such user");
            if (!check.IsLogged)
                throw new Exception("user is offline");
            return true;
        }
        public bool UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime dueDate)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            if (columnOrdinal< check.Board.Columns.Count - 1) // can't update tasks in the done column
            {          
                return check.Board.GetColumn(columnOrdinal).GetTaskByID(taskID).updateTaskDueDate(dueDate);
            }
            throw new Exception("can't update tasks in the done column");
        }
        public bool UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            if (columnOrdinal < check.Board.Columns.Count - 1)
            {
                
                return check.Board.GetColumn(columnOrdinal).GetTaskByID(taskID).updateTaskTitle(title);
            }
            throw new Exception("can't update tasks in the done column");
        }
        public bool UpdateTaskDescription(string email, int columnOrdinal, int taskID, string description)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            if (columnOrdinal < check.Board.Columns.Count - 1)
            {
                
                return check.Board.GetColumn(columnOrdinal).GetTaskByID(taskID).updateTaskDescription(description);
            }
            throw new Exception("can't update tasks in the done column");
        }
        public void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            check.Board.advanceTask(columnOrdinal, taskId);
        }
        public Task AddTask(string email, string title, string description, DateTime dueDate)
        {
            User check = _userController.GetUser(email);
            validUser(check);
            Task newAdded = check.Board.CreateNewTask(title, description, dueDate);
            return newAdded;
        }
    }
}
