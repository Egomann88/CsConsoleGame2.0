using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    public interface IWorkerServices
    {
        string Name { get; set; }
        byte Lvl { get; set; }
        ushort UpgradeCost { get; set; }
    }
}
