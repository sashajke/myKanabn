using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController
    {
        private List<User> users;
        private User currentUser;

        public UserController()
        {
            Factory.CurrentConfig = SavingSystem.File;
            this.users = new List<User>();
            this.currentUser = null;
        }
        static UserController()
        {

        }
        public User GetUser(string email)
        {
            return users.Find(user => user.Email == email);
        }
        public User Login(string email, string password)
        {
            if (this.currentUser == null)
            {
                User userToLogin = users.Find(user => user.Email.ToLower() == email.ToLower() && user.Password == password);
                if (userToLogin == null)
                    throw new Exception("email or password is incorrect");
                userToLogin.IsLogged = true;
                currentUser = userToLogin;
                //userToLogin.save();
                return userToLogin;
            }
            else
            {
                if (currentUser.Email == email)
                {
                    throw new Exception("the user is already online");
                    //log.error
                }
                else
                    throw new Exception("there is already online user");
            }
        }
        public bool Logout(string email)
        {
            User userToLogout = users.Find(user => user.Email.ToLower() == email.ToLower());
            if (userToLogout == null)
            {
                throw new Exception("User not found");
            }
            if (userToLogout != currentUser)
                throw new Exception("not a current user tried to logout");
            userToLogout.IsLogged = false;
            userToLogout.save();
            currentUser = null;
            return true;
        }
        public bool Register(string email, string password, string nickname)
        {
            if(!IsValidEmail(email))
                throw new Exception("You must enter a valid email adress");

            bool checkIfUserExists = users.Find(user => user.Email == email) != null;
            if (checkIfUserExists)
                throw new Exception("User already exists in the system");

            if (string.IsNullOrEmpty(nickname))
                throw new Exception("nickname cant be null or empty");
            if (nickname.Contains(' '))
                throw new Exception("nickname can't contain spaces");
            //checkIfUserExists = users.Find(user => user.Nickname == nickname) != null;
            //if (checkIfUserExists)
            //    throw new Exception("User already exists in the system");
            if (string.IsNullOrEmpty(password))
                throw new Exception("password cant be null or empty");
            if (password.Contains(' '))
                throw new Exception("password can't contain spaces");
            if (password.Length < 5 || password.Length > 25)
                throw new Exception("Password must be between 5 and 25 characters long");
            bool PasswordValidation = (password.Where(Char.IsUpper).Count() == 0) || (password.Where(Char.IsLower).Count() == 0) || (password.Where(Char.IsDigit).Count() == 0);
            if (PasswordValidation)
                throw new Exception("Password must contain at least one lower case letter , one upper case letter and one number");
            
            


            User newUser = new User(email, password, nickname);
            users.Add(newUser);
            newUser.save();
            return true;
        }
        private bool isValidEmail(string email)
        {
            try
            {
                var check = new System.Net.Mail.MailAddress(email);
                return check.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
            public void LoadData()
            {
            try
            {
                LoadAll();
            }
            catch(Exception ee)
            {
                users = new List<User>();
            }          
        }
        //public void save()
        //{
        //    DataAccessLayer.userControllerWrapper wrapper = new userControllerWrapper();
        //    List<DataAccessLayer.UserDalFile> usersWrapper = new List<UserDalFile>();
        //    foreach (var item in users)
        //    {
        //        usersWrapper.Add(item.ToDalObject());
        //    }
        //    wrapper.Users = usersWrapper;
        //    wrapper.Save();
        //}

        //public void saveAll()
        //{
        //    DataAccessLayer.TaskControllerWrapper taskWrapper = new TaskControllerWrapper();
        //    DataAccessLayer.ColumnControllerWrapper columnWrapper = new ColumnControllerWrapper();
        //    DataAccessLayer.userControllerWrapper userWrapepr = new userControllerWrapper();
        //    foreach (var user in users)
        //    {
        //        userWrapepr.Users.Add(user.ToDalObject());
        //        foreach (var column in user.Board.Columns)
        //        {
        //            columnWrapper.Columns.Add(column.ToDalObject());
        //            foreach (var task in column.TaskByID)
        //            {
        //                taskWrapper.Tasks.Add(task.ToDalObject());
        //            }
        //        }
        //    }
        //    taskWrapper.SaveAsSeperateFiles();
        //    columnWrapper.SaveAsSeperateFiles();
        //    userWrapepr.SaveAsSeperateFiles();
        //}
        public void DeleteData()
        {

            //DataAccessLayer.TaskControllerWrapper taskWrapper = new TaskControllerWrapper();
            //DataAccessLayer.ColumnControllerWrapper columnWrapper = new ColumnControllerWrapper();
            //DataAccessLayer.userControllerWrapper userWrapepr = new userControllerWrapper();
            //taskWrapper.RemoveAll();
            //columnWrapper.RemoveAll();
            //userWrapepr.RemoveAll();
            foreach (var user in users) // remove running data
            {
                Board userBoard = user.Board;
                foreach (var Column in userBoard.Columns)
                {
                    foreach (var task in Column.TaskByID)
                    {
                        task.ToDalObject().Remove();
                    }
                    Column.TaskByID.Clear();
                    Column.ToDalObject().Remove();
                }
                userBoard.ColumnNames.Clear();
                userBoard.Columns.Clear();
                user.ToDalObject().Remove();
            }
            users.Clear();
        }
        public void LoadAll()
        {
            if (users == null)
                users = new List<User>();
            users.Clear();      
            DataAccessLayer.TaskControllerWrapper taskWrapper = new TaskControllerWrapper();
            DataAccessLayer.ColumnControllerWrapper columnWrapper = new ColumnControllerWrapper();
            DataAccessLayer.userControllerWrapper userWrapepr = new userControllerWrapper();
            taskWrapper.LoadAsSeperateFiles();
            columnWrapper.LoadAsSeperateFiles();
            userWrapepr.LoadAsSeperateFiles();
            foreach (var user in userWrapepr.Users)
            {
                List<string> boardNames = new List<string>();

                List<string> columnNames = new List<string>();
                List<Column> bussinesColumns = new List<Column>();
                List<ColumnDalFile> columsToBeAdded = columnWrapper.Columns.FindAll(column => column.Email == user.Email);

                columsToBeAdded.Sort((a, b) => (a.OrderID.CompareTo(b.OrderID)));

                foreach (var column in columsToBeAdded)
                {
                    boardNames.Add(column.Name);
                    columnNames.Add(column.Name);

                    List<TaskDalFile> tasksToBeAdded = taskWrapper.Tasks.FindAll(task => task.Email == column.Email && task.ColumnID == column.OrderID);
                    List<Task> bussinesTasks = new List<Task>();
                    foreach (var task in tasksToBeAdded)
                    {
                        bussinesTasks.Add(new Task(task));
                    }
                    bussinesColumns.Add(new Column(column, bussinesTasks));
                }
                Board bussinesBoard = new Board(user.Email, bussinesColumns, boardNames);
                User bussinesUser = new User(user, bussinesBoard, columnNames);
                bussinesUser.IsLogged = false;                 
                users.Add(bussinesUser);
            }
        }
    }
}