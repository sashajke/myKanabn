using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {   
        private TaskController taskController;
        public TaskService(UserController taskManager)
        {
            taskController = new TaskController(taskManager);
        }

        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            Response<Task> toReturn;
            try
            {
                BusinessLayer.Task taskBusiness = taskController.AddTask(email,title,description,dueDate);
                Task taskService = new Task(taskBusiness);
                toReturn = new Response<Task>(taskService);
            }
            catch (Exception ee)
            {
                toReturn = new Response<Task>(ee.Message);
            }
            return toReturn;
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response toReturn;
            try
            {
                taskController.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            Response toReturn;
            try
            {
                taskController.UpdateTaskTitle(email, columnOrdinal, taskId, title);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            Response toReturn;
            try
            {
                taskController.UpdateTaskDescription(email, columnOrdinal, taskId, description);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            Response toReturn;
            try
            {
                taskController.AdvanceTask(email, columnOrdinal, taskId);
                toReturn = new Response();
            }
            catch (Exception ee)
            {
                toReturn = new Response(ee.Message);
            }
            return toReturn;
        }

    }
}
