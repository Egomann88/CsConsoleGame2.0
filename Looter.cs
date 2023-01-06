using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class Looter : Worker, IWorkerServices
    {
        //class
        const byte MAXLVL = 5;  // max level for looter

		//const
		public Looter(ushort price, string name, byte lvl = 1) : base(price, name) {
			Lvl = lvl;

			SetUpgradeCost();
		}

		//meth
		public byte Lvl { get; set; }

        public ushort UpgradeCost { get; set; }

        public void IncreaseService() {
            if(Lvl == MAXLVL) {
                Console.WriteLine("{0} ist auf der höchsten Stufe", Name);
                return;
            }

            Console.WriteLine("{0} ist Stufe {1}.\nEine Verbesserung kostet {2}.\n" +
                "Stufe aufsteigen lassen? [j/n]", Name, Lvl, UpgradeCost);
			if (Console.ReadKey(false).Key == ConsoleKey.J) {
                Lvl++;
				SetUpgradeCost();
            }
		}

        public ushort UseService() {
            Random r = new();
            byte roll = (byte)r.Next(1, 21);
            byte[] gainPerLevel = new byte[5] { 40, 60, 80, 140, 220 };
            ushort gain = 0;

			// convert to byte -> cannot be bigger than byte
			if (roll == 1) gain = (byte)(gainPerLevel[Lvl - 1] / 3);
            else if (roll < 11) gain = (byte)(gainPerLevel[Lvl - 1] / 1.5);
			else if (roll < 20) gain = gainPerLevel[Lvl - 1];
            else gain = (ushort)(gainPerLevel[Lvl - 1] * 2.5);

            return gain;
        }

        private void SetUpgradeCost() {
            Random r = new();
            switch (Lvl) {
                case 1:
                    UpgradeCost = (ushort)(500 + r.Next(1, 777));
                    break;
                case 2:
                    UpgradeCost = (ushort)(800 + r.Next(1, 777));
                    break;
                case 3:
                    UpgradeCost = (ushort)(1357 + r.Next(1, 999));
                    break;
                case 4:
                    UpgradeCost = (ushort)(2222 + r.Next(1, 1111));
                    break;
            }
        }
    }
}
