using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Common
{
    public enum ColumnStatus 
    {
        Backlog,
        InProgress,
        Done
    }
    public enum SavingSystem
    {
        File,
        DB
    }
}
