using IntroSE.Kanban.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserControllerDB : IDALController<IUserDAL>
    {
        private const string _dbName = "kanban.db";
        private const string _tableName = "Users";
        public List<IUserDAL> Items { get; set; }
        public UserControllerDB()
        {
            Items = new List<IUserDAL>();
        }
        public bool Load()
        {
            bool res = false;
            if (Items == null)
                Items = new List<IUserDAL>();
            Items.Clear();
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString()))
            {
                Items = cnn.Query<IUserDAL>($"select * from {_tableName}",new DynamicParameters()).ToList();

            }
            return true;
        }

        private string GetConnectionString()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _dbName));
            return $"Data Source={path}; Version=3;";
        }

        public void RemoveAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString()))
            {
                cnn.Execute($"DROP TABLE {_tableName}");
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
