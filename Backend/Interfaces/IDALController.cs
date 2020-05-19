using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Interfaces
{
    interface IDALController<T>
    {
        bool Load();
        void Save();
        void RemoveAll();
        List<T> Items { get; set; }
    }
}
