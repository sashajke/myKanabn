using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDalDB : IUserDAL
    {
        private const string _dbName = "kanban.db";
        private const string _tableName = "Users";
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool IsLogged { get; set; }

        public UserDalDB(string email,string password,string nickname,bool isLogged)
        {
            this.Email = email;
            this.Password = password;
            this.Nickname = nickname;
            this.IsLogged = isLogged;
        }
        public UserDalDB()
        {

        }
        public bool Load(string email)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            int res = -1;
            if(!checkIfDBexists(_dbName))
                SQLiteConnection.CreateFile(_dbName);
            using (var connection = new SQLiteConnection(GetConnectionString()))
            {
                if (!checkIfTableExists(_tableName))
                {
                    CreateTable(_tableName);
                }
                SQLiteCommand command = new SQLiteCommand(null, connection);
                if (!checkIfUserExists(Email)) // if no such user create a new one
                {
                    command.CommandText = $"INSERT INTO {_tableName} ( Email,Nickname,Password,IsLogged) " +
                        $"VALUES (@emailVal,@nickVal,@passVal,@isLogVal);";
                }
                else // update the appropriate row
                {
                     string text = $"UPDATE {_tableName} SET Nickname='{Nickname}',Password='{Password}',IsLogged={IsLogged} WHERE Email='{Email}'";

                    string text2 = $"UPDATE {_tableName} SET Nickname=@nickVal,Password=@passVal,IsLogged=@isLogVal WHERE Email=@emailVal";
                    command.CommandText = text2;
                    //command.CommandText = "UPDATE Users SET Nickname = 'sasha55',Password = 'ggg',IsLogged = 1 WHERE Email = 'gg@gmail.com'";
                }           
                try
                {
                    connection.Open();

                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", this.Email);
                    SQLiteParameter nickParam = new SQLiteParameter("@nickVal", "sasha");
                    SQLiteParameter passParam = new SQLiteParameter("@passVal", this.Password);
                    SQLiteParameter isLogParam = new SQLiteParameter("@isLogVal", this.IsLogged);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(nickParam);
                    command.Parameters.Add(passParam);
                    command.Parameters.Add(isLogParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    
                }
                catch(Exception ee)
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
                    CommandText = $"DELETE * FROM {_tableName} WHERE Email=@emailVal"
                };
                try
                {
                    connection.Open();
                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", this.Email);
                    command.Parameters.Add(emailParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception ee)
                {

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
        private string GetConnectionString()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _dbName));
            return $"Data Source={path}; Version=3;";        
        }
        private bool checkIfDBexists(string dbName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), dbName));
            return File.Exists(path);
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
                    SQLiteDataReader dataReader =  command.ExecuteReader();
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
        public void CreateTable(string tableName)
        {
            string connectionString = GetConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
               // string sql2 = $"CREATE TABLE {tableName}(Email varchar(20) PRIMARY KEY,Nickname varchar(20),Password varchar(20),IsLogged INTEGER"; 


                SQLiteCommand command = new SQLiteCommand(null, connection);
                string sql = $"create table {_tableName} (Email TEXT NOT NULL PRIMARY KEY,Nickname TEXT NOT NULL,Password  TEXT NOT NULL,IsLogged int NOT NULL)";

                command.CommandText = sql;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception ee)
                {

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }           
            }
        }
        public bool checkIfUserExists(string email)
        {
            bool res = false;
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM {_tableName} WHERE Email='{email}'"
                };
                try
                {
                    connection.Open();
                    SQLiteDataReader dataReader =command.ExecuteReader();
                    int counter = 0;
                    while (dataReader.Read())
                        counter++;
                    res = counter > 0;
                }
                catch(Exception ee)
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

    }
}
