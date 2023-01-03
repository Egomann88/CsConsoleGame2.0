namespace CsConsoleGame
{
    internal class Marketplace
    {
        // class
        private const byte WEAKHEALERPRICE = 25;
        private const byte NORMALHEALERPRICE = 45;
        private const byte JOKERHEALERPRICE = 60;
        private const byte STRONGHEALERPRICE = 80;
        private const byte LVLFORHIGHUSES = 12;
        private const int SHORTTIMEOUT = 800;
        private const int TIMEOUT = 1200;
        private const int LONGTIMEOUT = 2000;
        private const ushort STRPRICE = 390;
        private const ushort INTPRICE = 360;
        private const ushort DEXPRICE = 410;
        private const ushort HELPRICE = 345;
        private const ushort CCHPRICE = 550;
        private const ushort CMLPRICE = 580;

        // mem

        // const
        public Marketplace(Player p) {
            Player = p;
        }

        // meth
        private Player Player { get; set; }

        /// <summary>
        /// Overview of the entire market -> mainspot<br />
        /// The player can go anywhere or leave
        /// </summary>
        /// <returns>current Character</returns>
        public Player OnMarket() {
            bool onMarket = true;

            while (onMarket) {
                Console.Clear();
                Console.WriteLine("Ihr befindet Euch auf dem Marktplatz.\nWohin wollt Ihr gehen?");
                Console.WriteLine("1) Heiler\n2) Glückspiel\n3) Arena\n4) Verstärkungsmagier\n9) Marktplatz verlassen");

                //input
                switch (Console.ReadKey(true).KeyChar) {
                    case '1': HealerOverView(); break;
                    case '2': GamblingOverView(); break;
                    case '3': ArenaOverView(); break;
                    case '4': StatPushOverView(); break;
                    case '9': onMarket = false; break;
                    default: continue;
                }
            }

            return Player;
        }

        private void HealerOverView() {
            Random r = new();
            string[] healers = {
                $"1) den Anfänger, sein Heilwert beträgt 25 % eures maximalen Lebens. (Preis: {WEAKHEALERPRICE})",
                $"2) den Erfaherenen, sein Heilwert beträgt 45 % eures maximalen Lebens. (Preis: {NORMALHEALERPRICE})",
                $"3) die Wilde, wie viel sie heilt, hängt von ihrer Stimmung ab. (Preis: {JOKERHEALERPRICE})",
                $"4) die Meisterin, sie kann euer gesamtes Leben und darüber hinaus heilen. (Preis: {STRONGHEALERPRICE})",
                $"9) Zurück zum Marktplatz",
                $"\nEurer Leben: {Player.Health[0]} / {Player.Health[1]}"
            };

            while (true) {
                Console.Clear();

                if (PlayerFullHp()) {
                    Console.WriteLine("Ihr habt bereits volles Leben.");
                    Thread.Sleep(SHORTTIMEOUT);
                    break;
                }

                Console.WriteLine("Es gibt folgende Heiler auf dem Markt:");
                foreach(string healer in healers) {
                    Console.WriteLine(healer);
                }

                // input
                switch (Console.ReadKey(true).KeyChar) {
                    case '1':
                        if (!PlayerGoldEnough(WEAKHEALERPRICE)) break;

                        HealerPerzent(0.25);
                        Player.Gold -= WEAKHEALERPRICE;
                        break;
                    case '2':
                        if (!PlayerGoldEnough(NORMALHEALERPRICE)) break;

                        HealerPerzent(0.45);
                        Player.Gold -= NORMALHEALERPRICE;
                        break;
                    case '3':
                        if (!PlayerGoldEnough(JOKERHEALERPRICE)) break;

                        HealerNumber((short)r.Next(5, Player.Health[1] - 15), true);
                        Player.Gold -= JOKERHEALERPRICE;
                        break;
                    case '4':
                        if (Player.Lvl < LVLFORHIGHUSES) {
                            Console.WriteLine("Die Heilerin lässt euch nicht hinein. Euer Level ist zu tief.");
                            Thread.Sleep(TIMEOUT);
                            continue;
                        } else if (!PlayerGoldEnough(STRONGHEALERPRICE)) break;

                        Player.FullHeal();
                        HealerNumber(5, true);  // overheal by 5 hp
                        Console.WriteLine("Komplettes Leben wurde wiederhergestellt.");
                        Player.Gold -= STRONGHEALERPRICE;
                        break;
                    case '9': return;
                    default: continue;
                }
                Thread.Sleep(SHORTTIMEOUT);
                return;
            }
        }

        /// <summary>
        /// checks if the player hp is full
        /// </summary>
        /// <returns>true if hp is full - false if not</returns>
        private bool PlayerFullHp() {
            if (Player.Health[0] >= Player.Health[1]) return true;
            return false;
        }

        /// <summary>
        /// heals the player by %
        /// </summary>
        /// <param name="healPerzent">% value the player will be healed by</param>
        private void HealerPerzent(double healPerzent) {
            double healValue = Math.Round(Player.Health[1] * healPerzent);
            Player.ChangeCurrentHealth((short)healValue);
            Console.WriteLine("{0} HP wurden wiederhergestellt.", healValue);
        }

        /// <summary>
        /// heals the player
        /// </summary>
        /// <param name="healValue">points of helaing</param>
        /// <param name="overheal">heal over map hp?</param>
        private void HealerNumber(short healValue, bool overheal) {
            Player.ChangeCurrentHealth(healValue, overheal);
            Console.WriteLine("{0} HP wurden wiederhergestellt.", healValue);
        }

        /// <summary>
        /// Shows the games one can play -> gives the choice to play one of them or to leave
        /// </summary>
        private void GamblingOverView() {
            while (true) {
                Console.Clear();
                Console.WriteLine("Ihr könnt \"Rot oder Schwarz\" spielen.");
                Console.WriteLine("Dort musst ihr euren Einsatz geben und auf euer Glück hoffen.");
                Console.WriteLine("1) Rot oder Schwarz spielen\n9) Zurück zum Marktplatz");

                // input
                switch (Console.ReadKey(true).KeyChar) {
                    case '1': RedBlack(); break;
                    case '9': return;
                    default: continue;
                }
            }
        }

        /// <summary>
        /// The player has to give his bet (the game does not allow the p. to give higher bets than he possesses)<br />
        /// The player has to choose between red and black<br />
        /// If the player is right, he gets his bet paid out - if not, he has to pay his bet
        /// </summary>
        private void RedBlack() {
            Random r = new();
            string[] moods = { "grimmige", "gelangweilte", "fröhliche" };
            string dealerMood = moods[r.Next(1, moods.Length)];
            string dealer = $"Der {dealerMood} Spielmeister ";
            bool gameIsRed = false, playerIsRed = false, ableToPlay = false;
            uint stake = 0; // players bet
            char input = '0';

            if (Player.Gold <= 0) { // player has no gold
                NotEnoughMoney();
                Thread.Sleep(TIMEOUT);
                return;
            }

            while(!ableToPlay) {
                Console.Clear();
                Console.WriteLine(dealer + "fragt nach eurem Einsatz. (Euer Gold: {0})", Player.Gold);
                Console.Write("Euer Einsatz: ");
                if (!uint.TryParse(Console.ReadLine(), out stake)) continue;    // must insert a (positive) number
                if (stake == 0) {
                    Console.WriteLine("Ihr müsst eine Wette abschliessen!");
                    Thread.Sleep(SHORTTIMEOUT);
                } else if (stake > Player.Gold) {
                    Console.WriteLine("Ihr dürft nicht mehr wetten, als Ihr eigentlich besitzt!");
                    Thread.Sleep(SHORTTIMEOUT);
                } else ableToPlay = true;
            }

            Console.WriteLine(dealer + "fragt, für das Ihr wettet.");
            while (true) {
                Console.WriteLine("\n1) Rot\n2) Schwarz");
                Console.Write("Eure Wahl: ");
                input = Console.ReadKey(false).KeyChar;

                if (input == '1') playerIsRed = true;
                else if (input == '2') playerIsRed = false;
                else continue;

                break;
            }

            Console.Write("\n\nDas Ergebnis ist");
            for (byte i = 0; i < 3; i++) {
                Console.Write(".");
                Thread.Sleep(SHORTTIMEOUT - 200); // 0.6s * 3 = 1.8s
            }

            if (r.Next(1, 3) == 1) {
                gameIsRed = true;
                Console.WriteLine("\nRot!");
            } else {
                gameIsRed = false;
                Console.WriteLine("\nSchwarz!");
            }

            if (playerIsRed == gameIsRed) {
                Console.WriteLine("Ihr habt gewonnen!");
                Player.Gold += Convert.ToInt32(stake);
            } else {
                Console.WriteLine("Ihr habt verloren...");
                Player.Gold -= Convert.ToInt32(stake);
            }

            Thread.Sleep(TIMEOUT);
        }

        /// <summary>
        /// Shows arena and rules -> gives choice to fight or leave
        /// </summary>
        private void ArenaOverView() {
            while (true) {
                Console.Clear();
                Console.WriteLine("In der Arena werdet ihr ausschliesslich starke Gegner treffen und wie der Zufall es will, " +
                    "habt ihr die Möglichkeit gegen besonders starke Gegner zu kämpfen, mit höheren Belohnungen natürlich\n" +
                    "In der Arena gelten nicht dieselben Regeln wie in der Wildnis. Hier könnt ihr nicht sterben.");
                Console.WriteLine("1) Normaler Arenakampf\n2) Kampf gegen starken Gegner\n9) Zurück zum Marktplatz.");

                // input
                switch (Console.ReadKey(true).KeyChar) {
                    case '1': Player = ArenaFight(false); break;
                    case '2': Player = ArenaFight(true); break;
                    case '9': return;
                    default: continue;
                }
            }
        }

        /// <summary>
        /// Evaluates and fights against enemy in arena
        /// </summary>
        /// <param name="isHard">Is Enemy strong?</param>
        private Player ArenaFight(bool isHard) {
            Enemy e = EvalEnemy(isHard);
            FightArena fa = new(Player, e);

            return fa.FightIn();
        }

        /// <summary>
        /// generates rnd enemy<br/>
        /// enemy can be extra strong
        /// </summary>
        /// <param name="isHard">Is Enemy strong?</param>
        /// <returns>new Enemy</returns>
        private EnemyArena EvalEnemy(bool isHard) {
            Random r = new();
            byte enemyId = Convert.ToByte(r.Next(1, 6));

            return new EnemyArena(Player.Lvl, enemyId, isHard);
        }

        /// <summary>
        /// Gives the opportunity to increase stats permanently.<br />
        /// The Player can't use these service is its level is too low.<br />
        /// If the player can't pay, he won't get it
        /// </summary>
        private void StatPushOverView() {
            ushort price = 0;

            if (Player.Lvl < LVLFORHIGHUSES) {
                Console.Clear();
                Console.WriteLine("Die Verstärkungsmagier lässt euch nicht hinein. Euer Level ist zu tief.");
                Thread.Sleep(TIMEOUT);
                return;
            }

            while (true) {
                Console.Clear();
                Console.WriteLine("Der Verstärkungsmagier kann euch, auf eine neue Ebene der Macht bringen, " +
                    "für einen kleinen Betrag natürlich.");
                Console.WriteLine("Euer Gold: {0}", Player.Gold);
                Console.WriteLine("1) +1 Stärke (Preis: {0} Gold)\n2) +1 Inteligents (Preis: {1} Gold)\n3) +1 Geschicklichkeit (Preis: {2} Gold)\n" +
                  "4) +5 Max Leben (Preis: {3} Gold)\n5) Krit. Chance + 2 % (Preis: {4} Gold)\n6) Krit. Schaden + 5 % (Preis: {5} Gold)\n" +
                  "9) Zurück zum Marktplatzs", STRPRICE, INTPRICE, DEXPRICE, HELPRICE, CCHPRICE, CMLPRICE);

                // input
                switch (Console.ReadKey().KeyChar) {
                    case '1':
                        price = STRPRICE;
                        if(!PlayerGoldEnough(price)) continue;
                        
                        Player.Strength++;
                        break;
                    case '2':
                        price = INTPRICE;
                        if (!PlayerGoldEnough(price)) continue;
                        
                        Player.Intelligents++;
                        break;
                    case '3':
                        price = DEXPRICE;
                        if (!PlayerGoldEnough(price)) continue;

                        Player.Dexterity++;
                        break;
                    case '4':
                        price = HELPRICE;
                        if (!PlayerGoldEnough(price)) continue;

                        Player.Health[1] += 5;
                        break;
                    case '5':
                        price = CCHPRICE;
                        if (!PlayerGoldEnough(price)) continue;

                        Player.CritChance += 2;
                        break;
                    case '6':
                        price = CMLPRICE;
                        if (!PlayerGoldEnough(price)) continue;

                        Player.CritMult += 0.05F;
                        break;
                    case '9': return;
                    default: continue;
                }
                Player.Gold -= price;
                Thread.Sleep(SHORTTIMEOUT);
            }
        }

        private bool PlayerGoldEnough(ushort price) {
            if (Player.Gold < price) {
                NotEnoughMoney();
                Thread.Sleep(SHORTTIMEOUT);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Will trigger if the player can't pay for a service 
        /// </summary>
        private static void NotEnoughMoney() {
            Console.WriteLine("Ihr habt nicht genügend Geld.");
        }
    }
}
