using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class Player : Character {
        // class
        const byte MAXLVL = 100;

        // member

        // const
        /// <summary>
        /// creates an new Player
        /// </summary>
        /// <param name="name">playername</param>
        /// <param name="cl">class of player</param>
        public Player(string name, byte cl) {
            if (InValidSign(name)) {
                Console.WriteLine("\nUnerlaubtes Zeichen im Namen!");
                return;
            }

            if(!(cl is 1 or 2 or 3)) {
                Console.WriteLine("\nEingegebene Klasse gibt es nicht!");
                return;
            }

            Name = name;
            Class = cl;
            Exp = new uint[2] { 0, 30 };
            Lvl = 1;

            switch (cl) {
                case 1: // warrior
                    Strength = 4;
                    Intelligents = 2;
                    Dexterity = 2;
                    CritChance = 2.4F;
                    CritMult = 1.25F;
                    Health = new short[2] { 30, 30 };
                    Gold = 29;
                    break;
                case 2: // mage
                    Strength = 2;
                    Intelligents = 4;
                    Dexterity = 2;
                    CritChance = 4.8F;
                    CritMult = 1.5F;
                    Health = new short[2] { 23, 23 };
                    Gold = 21;
                    break;
                case 3: // thief
                    Strength = 2;
                    Intelligents = 2;
                    Dexterity = 4;
                    CritChance = 3.2F;
                    CritMult = 1.75F;
                    Health = new short[2] { 25, 25 };
                    Gold = 36;
                    break;
                default:  break;
            }
        }

        // meth
        /// <summary>
        /// 1 = warrior
        /// 2 = mage
        /// 3 = thief
        /// </summary>
        public byte Class { get; set; }

        /// <summary>
        /// Creates an new Player with Name and Class.<br />
        /// </summary>
        /// <returns>Player</returns>
        public static Player CreatePlayer() {
            string name = "";
            byte cl = 0;

            name = ChangeName();

            do {
                Console.Clear();
                Console.WriteLine("Was ist die Klasse ihres Charakters?\n1) Krieger\n2) Magier\n3) Schurke");
                cl = Convert.ToByte(Console.ReadKey(false).KeyChar - 48);
            } while (cl < 1 || cl > 4);

            return new Player(name, cl);
        }

        /// <summary>
        /// overrides the old character name
        /// </summary>
        /// <returns>(new) name</returns>
        public static string ChangeName() {
            string name = "";

            while (name == "") {
                Console.Clear();
                Console.WriteLine("Geben Sie den Namen ihres Charakters ein:");
                name = Console.ReadLine();

                if (InValidSign(name)) {
                    Console.WriteLine("\nIm Namen ist ein unerlaubtes Zeichen enthalten!");
                    Thread.Sleep(500);
                    continue;
                } else if (name == "" || name == " ") {
                    Console.WriteLine("\nDer Name darf nicht leer sein!");
                    Thread.Sleep(500);
                    continue;
                }

                // convert to char array of the string
                char[] letters = name.ToCharArray();
                // upper case the first char
                letters[0] = char.ToUpper(letters[0]);
                // put array back together
                name = new string(letters);
            }

            return name;
        }

        /// <summary>
        /// Returns Classname
        /// </summary>
        /// <returns>classname</returns>
        public string GetClassName() {
            string cl = "";
            if (Class == 1) cl = "Krieger";
            else if (Class == 2) cl = "Magier";
            else cl = "Schurke";

            return cl;
        }

        /// <summary>
        /// infoscreen for player
        /// </summary>
        public void ShowPlayer() {
            string cl = GetClassName();
            string[] stats = {
                $"Name:\t\t\t{Name}",
                $"Klasse:\t\t\t{cl}",
                $"Level:\t\t\t{Lvl}",
                $"Exp:\t\t\t{Exp[0]} / {Exp[1]}",
                $"Leben:\t\t\t{Health[0]} / {Health[1]}",
                $"Gold:\t\t\t{Gold}",
                $"Stärke:\t\t\t{Strength}",
                $"Inteligents:\t\t{Intelligents}",
                $"Geschwindigkeit:\t{Dexterity}",
                $"Krit. Chance:\t\t{CritChance} %",
                $"Krit. Schaden:\t\t{(CritMult - 1.0F) * 100} %",
                "\nDrücken Sie <Enter> um zurückzukehren..."
            };

            do {
                Console.Clear();
                foreach (string stat in stats) {
                    Console.WriteLine(stat);
                }
            } while (Console.ReadKey(false).Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Increases Level and Exp, which is needed for next lvl<br />
        /// Reduces current Exp with the Exp need for this level
        /// </summary>
        public void IncreaseLvl() {
            // if lvl 100 is reached, no more leveling
            if (Lvl >= MAXLVL) {
                Lvl = MAXLVL;
                return;
            }

            while(Exp[0] >= Exp[1]) { // allows multiple lvl ups
                Console.WriteLine("\n{0} ist ein Level aufgestiegen.\n{0} ist nun Level {1}.", Name, ++Lvl);
                Console.ReadKey(true);
                Exp[0] -= Exp[1];
                Exp[1] += (byte)(20 + Lvl);

                if (Lvl % 10 == 0) Exp[1] += 50;    // increases exp need every 10 lvls a bit more

                IncreaseStats();
            }
        }

        /// <summary>
        /// Increases all stats by one (exept for class stat - increased by 2)<br />
        /// Heal the Character to max HP
        /// </summary>
        private void IncreaseStats() {
            Strength++;
            Intelligents++;
            Dexterity++;
            ChangeMaximumHealth(2);
            FullHeal();
            if (Lvl % 10 == 0) {
                CritChance += 0.3F;
                CritMult += 0.10F;
            }

            if (Lvl % 3 == 0) {
                switch (Class) {
                    case 1: Strength++; break;
                    case 2: Intelligents++; break;
                    case 3: Dexterity++; break;
                }
            }
        }
    }
}
