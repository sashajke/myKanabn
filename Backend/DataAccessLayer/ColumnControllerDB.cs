using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IntroSE.Kanban.Backend.Interfaces;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class ColumnControllerDB : IDALController<IColumnDAL>
    {
        private const string _dbName = "kanban.db";
        private const string _tableName = "Columns";
        public List<IColumnDAL> Items { get; set; }

        public bool Load()
        {
            bool res = false;
            if (Items == null)
                Items = new List<IColumnDAL>();
            Items.Clear();
            using (IDbConnection cnn = new SQLiteConnection(GetConnectionString()))
            {
                Items = cnn.Query<IColumnDAL>($"select * from {_tableName}", new DynamicParameters()).ToList();
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
