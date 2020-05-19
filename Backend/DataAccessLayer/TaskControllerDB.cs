using Dapper;
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
    class TaskControllerDB : IDALController<ITaskDAL>
    {
        private const string _dbName = "kanban.db";
        private const string _tableName = "Tasks";
        public List<ITaskDAL> Items { get; set; }

        public bool Load()
        {
            bool res = false;
            if (Items == null)
                Items = new List<ITaskDAL>();
            Items.Clear();
            using (SQLiteConnection cnn = new SQLiteConnection(GetConnectionString()))
            {
                //var output = cnn.Query<TaskDalDB>($"select * from {_tableName}", new DynamicParameters());
                //Items = output.ToList<ITaskDAL>();
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = cnn,
                    CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{_tableName}';"

                };
                try
                {
                    cnn.Open();
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
                    cnn.Close();
                }
            }
            return true;
        }

        public void RemoveAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString()))
            {
                cnn.Execute($"DROP TABLE {_tableName}");
            }
        }

        private string GetConnectionString()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _dbName));
            return $"Data Source={path}; Version=3;";
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
