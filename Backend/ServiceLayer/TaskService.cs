using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                log.Debug($"Added task number: {taskBusiness.Id} to the board of {email} succesfully");

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
                log.Debug($"Updated task number: {taskId} duedate to be {dueDate} in the board of {email} succesfully");

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
                log.Debug($"Updated task number: {taskId} title to be {title} in the board of {email} succesfully");

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
                log.Debug($"Updated task number: {taskId} description to be {description} in the board of {email} succesfully");

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
                log.Debug($"Advanced task number: {taskId} in the board of {email} succesfully");

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
