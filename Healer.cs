using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
	internal class Healer : Worker, IWorkerServices
	{
		//class
		const byte MAXLVL = 5;  // max level for looter

		//const
		public Healer(string name, byte lvl = 1) : base(name) {
			Lvl = lvl;

			SetUpgradeCost();

            /*
			 WorkerServices w = new(this);

			 w.SetUpgradeCost();
			 */
        }

        //meth
        public byte Lvl { get; set; }

		public ushort UpgradeCost { get; set; }

		public void IncreaseService() {
			if (Lvl == MAXLVL) {
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

		public short UseService() {
			Random r = new();
			byte roll = (byte)r.Next(1, 21);
			byte[] healPerLevel = new byte[5] { 5, 10, 15, 20, 30 };
			byte heal = 0;

			// convert to byte -> cannot be bigger than byte
			if (roll == 1) heal = (byte)(healPerLevel[Lvl - 1] * 0.4);	// 40 %
			else if (roll < 11) heal = (byte)(healPerLevel[Lvl - 1] * 0.7);	// 70 %
			else if (roll < 20) heal = healPerLevel[Lvl - 1];	// 100 %
			else heal = (byte)(healPerLevel[Lvl - 1] * 1.3);	// 130 %

			return heal;
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
