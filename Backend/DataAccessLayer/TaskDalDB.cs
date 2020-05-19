using IntroSE.Kanban.Backend.Common;
using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDalDB : ITaskDAL
    {
            private const string _dbName = "kanban.db";
            private const string _tableName = "Tasks";


        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime DueDate { get; set; }
        public int ColumnID { get; set; }
         
        public TaskDalDB(int id,string title, string desc, string email,DateTime creation,DateTime due,int ColId)
        {
            this.Id = id;
            this.Title = title;
            this.Description = desc;
            this.Email = email;
            this.Creationtime = creation;
            this.DueDate = due;
            this.ColumnID = ColId;
        }
        public TaskDalDB()
        {

        }
        public bool Load(string email, int orderId, int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            int res = -1;
            if (!checkIfDBexists(_dbName))
                SQLiteConnection.CreateFile(_dbName);
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                if (!checkIfTableExists(_tableName))
                {
                    CreateTable(_tableName);
                }
                SQLiteCommand command = new SQLiteCommand(null, connection);
                if (!checkIfTaskExists(Id,Email)) // if no such Task create a new one
                {
                    command.CommandText = $"INSERT INTO {_tableName} (Email,ID,ColumnID,Title,Descritpion,CreationDate,DueDate) " +
                        $"VALUES (@emailVal,@IDVal,@colVal,@titleVal,@descVal,@createVal,@dueVal);";
                }
                else // update the appropriate row
                {
                    //string text = $"UPDATE {_tableName} SET Nickname='{Nickname}',Password='{Password}',IsLogged={IsLogged} WHERE Email='{Email}'";

                    string text2 = $"UPDATE {_tableName} SET ColumnID=@colVal,Title=@titleVal,Descritpion=@descVal,CreationDate=@createVal,DueDate=@dueVal WHERE Email=@emailVal AND ID=@IDVal";
                    command.CommandText = text2;
                    //command.CommandText = "UPDATE Users SET Nickname = 'sasha55',Password = 'ggg',IsLogged = 1 WHERE Email = 'gg@gmail.com'";
                }
                try
                {
                    connection.Open();

                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", this.Email);
                    SQLiteParameter idParam = new SQLiteParameter("@IDVal", this.Id);
                    SQLiteParameter colParam = new SQLiteParameter("@colVal", this.ColumnID);

                    SQLiteParameter titleParam = new SQLiteParameter("@titleVal", this.Title);
                    SQLiteParameter descParam = new SQLiteParameter("@descVal", this.Description);
                    SQLiteParameter createParam = new SQLiteParameter("@createVal", this.Creationtime);
                    SQLiteParameter dueParam = new SQLiteParameter("@dueVal", this.DueDate);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(colParam);

                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descParam);
                    command.Parameters.Add(createParam);
                    command.Parameters.Add(dueParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();

                }
                catch (Exception ee)
                {
                    //log
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
        }

        private bool checkIfTaskExists(int id,string email)
        {
            bool res = false;
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM {_tableName} WHERE ID={id} AND Email='{email}'"
                };
                try
                {
                    connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    int counter = 0;
                    while (dataReader.Read())
                        counter++;
                    res = counter > 0;
                }
                catch (Exception ee)
                {

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res;
        }

        private string GetConnectionString()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _dbName));
            return $"Data Source={path}; Version=3;";
        }

        private void CreateTable(string tableName)
        {
            string connectionString = GetConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // string sql2 = $"CREATE TABLE {tableName}(Email varchar(20) PRIMARY KEY,Nickname varchar(20),Password varchar(20),IsLogged INTEGER"; 

                string sql = $"create table Tasks (Email TEXT NOT NULL,ID INT NOT NULL,ColumnID INT NOT NULL,Title TEXT NOT NULL,Descritpion TEXT NOT NULL,CreationDate REAL NOT NULL,DueDate REAL NOT NULL,FOREIGN KEY(Email) REFERENCES Users(Email),PRIMARY KEY(ID, Email))";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                //string sql = $"create table {_tableName} (Email TEXT NOT NULL PRIMARY KEY,ID INT NOT NULL PRIMARY KEY,Title TEXT NOT NULL,Descritpion TEXT NOT NULL,CreationDate REAL NOT NULL,DueDate REAL NOT NULL)";

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ee)
                {

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        private bool checkIfTableExists(string tableName)
        {
            bool res = false;
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';"

                };
                try
                {
                    connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    int counter = 0;
                    while (dataReader.Read())
                        counter++;
                    res = counter > 0;
                }
                catch (Exception ee)
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res;
        }

        private bool checkIfDBexists(string dbName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), dbName));
            return File.Exists(path);
        }

        public void Remove()
        {
            int res = -1;
            if (!checkIfDBexists(_dbName))
                throw new Exception($"database {_dbName} non existent"); // add log
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                if (!checkIfTableExists(_tableName))
                    throw new Exception($"table {_tableName} non existent");
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE * FROM {_tableName} WHERE Email=@emailVal AND ID=@idVal"
                };
                try
                {
                    connection.Open();
                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", this.Email);
                    SQLiteParameter idParam = new SQLiteParameter("@idVal", this.Id);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(idParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ee)
                {

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
