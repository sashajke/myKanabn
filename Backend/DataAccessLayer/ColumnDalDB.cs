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
    class ColumnDalDB : IColumnDAL
    {
        private const string _dbName = "kanban.db";
        private const string _tableName = "Columns";
        public string Email { get; set ; }
        public int Limit { get; set; }
        public int OrderID { get; set ; }
        public string Name { get ; set ; }

        public ColumnDalDB(string email, int id, int lim,string name)
        {
            this.Email = email;
            this.OrderID = id;
            this.Limit = lim;
            this.Name = name;
        }
        public ColumnDalDB()
        {

        }
        public bool Load(string email, int columnID)
        {
            throw new NotImplementedException();
        }

        public void Remove()
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
                if (!checkIfColumnExists(Email,OrderID)) // if no such user create a new one
                {
                    command.CommandText = $"INSERT INTO {_tableName} ( Email,ColumnID,Name,Lim) " +
                        $"VALUES (@emailVal,@colVal,@nameVal,@limVal);";
                }
                else // update the appropriate row
                {

                    string text2 = $"UPDATE {_tableName} SET Name=@nameVal,Lim=@limVal WHERE Email=@emailVal AND ColumnID=@colVal";
                    command.CommandText = text2;
                }
                try
                {
                    connection.Open();

                    SQLiteParameter emailParam = new SQLiteParameter("@emailVal", this.Email);
                    SQLiteParameter colParam = new SQLiteParameter("@colVal", this.OrderID);
                    SQLiteParameter nameParam = new SQLiteParameter("@nameVal", this.Name);
                    SQLiteParameter limParam = new SQLiteParameter("@limVal", this.Limit);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(colParam);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(limParam);
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
        public void CreateTable(string tableName)
        {
            string connectionString = GetConnectionString();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // string sql2 = $"CREATE TABLE {tableName}(Email varchar(20) PRIMARY KEY,Nickname varchar(20),Password varchar(20),IsLogged INTEGER"; 


                SQLiteCommand command = new SQLiteCommand(null, connection);
                string sql = $"create table {_tableName} (ColumnID INT NOT NULL,Name TEXT NOT NULL,Lim INT NOT NULL,Email TEXT NOT NULL,PRIMARY KEY(ColumnID,Email))";

                command.CommandText = sql;
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
        public bool checkIfColumnExists(string email,int id)
        {
            bool res = false;
            using (SQLiteConnection connection = new SQLiteConnection(GetConnectionString()))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM {_tableName} WHERE Email='{email}' AND ColumnID={id}"
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

    }
}
