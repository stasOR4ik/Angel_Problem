using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Angel_Problem
{
    enum CellCondition
    {
        EMPTY,
        ANGEL,
        DEVIL
    }

    class Cell
    {
        public Polygon Square { get; set; }
        public CellCondition CellCondition { get; set; }

        public Cell()
        {
            Square = new Polygon();
            CellCondition = CellCondition.EMPTY;
        }

        public Cell(Polygon angelSquare)
        {
            Square = angelSquare;
            CellCondition = CellCondition.ANGEL;
        }
    }
}
