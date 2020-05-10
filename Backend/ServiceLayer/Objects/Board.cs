
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly IReadOnlyCollection<string> ColumnsNames;
        internal Board(IReadOnlyCollection<string> columnsNames) 
        {
            this.ColumnsNames = columnsNames;
        }
    }
}
