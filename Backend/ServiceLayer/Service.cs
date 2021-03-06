﻿using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Configuration;
using log4net.Config;
using System.IO;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// The service for using the Kanban board.
    /// It allows executing all of the required behaviors by the Kanban board.
    /// You are not allowed (and can't due to the interfance) to change the signatures
    /// Do not add public methods\members! Your client expects to use specifically these functions.
    /// You may add private, non static fields (if needed).
    /// You are expected to implement all of the methods.
    /// Good luck.
    /// </summary>
    public class Service : IService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private UserService _UserService;
        private TaskService _TaskService;
        private BoardService _BoardService;
        /// <summary>
        /// Simple public constructor.
        /// </summary>
        public Service()
        {
            log.Debug("initalized service class");
        }

        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>
        public Response LoadData()
        {
            BusinessLayer.UserController us = new UserController();
            _UserService = new UserService(us);
            _TaskService = new TaskService(us);
            _BoardService = new BoardService(us);
            log.Debug("loaded data");
            return _UserService.loadData();
        }


        ///<summary>Remove all persistent data.</summary>
        public Response DeleteData()
        {
            try
            {
                
                return _UserService.DeleteData();
                
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }


        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="email">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <param name="nickname">The nickname of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string email, string password, string nickname)
        {
            try
            {
                return _UserService.Register(email, password, nickname);
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }

        }


        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                return _UserService.Login(email, password);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<User>("didn't execute LoadData function");
            }
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                return _UserService.Logout(email);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Returns the board of a user. The user must be logged in
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<Board> GetBoard(string email)
        {
            try
            {
                return _BoardService.GetBoard(email);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Board>("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            try
            {
                return _BoardService.LimitColumnTasks(email, columnOrdinal, limit);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            try
            {
                return _TaskService.AddTask(email, title, description, dueDate);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Task>("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                return _TaskService.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            try
            {
                return _TaskService.UpdateTaskTitle(email, columnOrdinal, taskId, title);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            try
            {
                return _TaskService.UpdateTaskDescription(email, columnOrdinal, taskId, description);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                return _TaskService.AdvanceTask(email, columnOrdinal, taskId);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }


        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnName">Column name</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                return _BoardService.GetColumn(email, columnName);

            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Column>("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Returns a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try
            {
                return _BoardService.GetColumn(email, columnOrdinal);
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Column>("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Removes a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string email, int columnOrdinal)
        {
            try
            {
                return _BoardService.RemoveColumn(email, columnOrdinal);
            }
            catch(Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response("didn't execute LoadData function");
            }
        }

        /// <summary>
        /// Adds a new column, given it's name and a location to place it.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Location to place to column</param>
        /// <param name="Name">new Column name</param>
        /// <returns>A response object with a value set to the new Column, the response should contain a error message in case of an error</returns>
        public Response<Column> AddColumn(string email, int columnOrdinal, string Name)
        {
            try
            {
                return _BoardService.AddColumn(email, columnOrdinal, Name);
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Column>("didn't execute LoadData function");
            }

        }

        /// <summary>
        /// Moves a column to the right, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            try
            {
                return _BoardService.MoveColumnRight(email, columnOrdinal);
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Column>("didn't execute LoadData function");
            }

        }

        /// <summary>
        /// Moves a column to the left, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            try
            {
                return _BoardService.MoveColumnLeft(email, columnOrdinal);
            }
            catch (Exception ee)
            {
                log.Warn("didn't execute load data function");
                return new Response<Column>("didn't execute LoadData function");
            }
        }

    }
}
