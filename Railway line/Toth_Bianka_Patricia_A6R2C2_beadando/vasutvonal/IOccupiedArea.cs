using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vasutvonal
{
    interface IOccupiedArea
    {
        string Name { get; }                   
        int Width { get; }
        int Height { get; }
    }
}
