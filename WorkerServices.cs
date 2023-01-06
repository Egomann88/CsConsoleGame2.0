using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CsConsoleGame
{
    internal class WorkerServices
    {
        // class
        private const byte MAXLVL = 5;

        // const
        public WorkerServices(IWorkerServices ws) {
            Ws = ws;
        }

        // meth
        public IWorkerServices Ws { get; set; }

        public void IncreaseService() {
            if (Ws.Lvl == MAXLVL) {
                Console.WriteLine("{0} ist auf der höchsten Stufe", Ws.Name);
                return;
            }

            Console.WriteLine("{0} ist Stufe {1}.\nEine Verbesserung kostet {2}.\n" +
                "Stufe aufsteigen lassen? [j/n]", Ws.Name, Ws.Lvl, Ws.UpgradeCost);
            if (Console.ReadKey(false).Key == ConsoleKey.J) {
                Ws.Lvl++;
                SetUpgradeCost();
            }
        }

        public void SetUpgradeCost() {
            Random r = new();
            switch (Ws.Lvl) {
                case 1:
                    Ws.UpgradeCost = (ushort)(500 + r.Next(1, 777));
                    break;
                case 2:
                    Ws.UpgradeCost = (ushort)(800 + r.Next(1, 777));
                    break;
                case 3:
                    Ws.UpgradeCost = (ushort)(1357 + r.Next(1, 999));
                    break;
                case 4:
                    Ws.UpgradeCost = (ushort)(2222 + r.Next(1, 1111));
                    break;
            }
        }
    }
}
