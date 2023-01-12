using CsConsoleGame;
using System;
using NUnit;
using NUnit.Framework;


namespace TestCsConsoleGame
{
    internal class TestEnemy
    {
        [TestCase(1, 1, false, ExpectedResult = 2)]
        [TestCase(55, 2, false, ExpectedResult = 4)]
        [TestCase(10, 2, true, ExpectedResult = 4)]
        [TestCase(66, 3, false, ExpectedResult = 3)]
        [TestCase(9, 3, true, ExpectedResult = 2)]
        [TestCase(90, 4, false, ExpectedResult = 8)]
        [TestCase(10, 4, true, ExpectedResult = 6)]
        [TestCase(80, 5, false, ExpectedResult = 9)]
        [TestCase(10, 5, true, ExpectedResult = 6)]
        [TestCase(20, 6, false, ExpectedResult = 3)]
        [TestCase(10, 6, true, ExpectedResult = 4)]
        [TestCase(45, 7, false, ExpectedResult = 11)]
        [TestCase(10, 7, true, ExpectedResult = 12)]
        [TestCase(64, 8, false, ExpectedResult = 18)]
        [TestCase(10, 8, true, ExpectedResult = 16)]
        [TestCase(85, 9, false, ExpectedResult = 8)]
        [TestCase(10, 9, true, ExpectedResult = 6)]
        [TestCase(43, 10, false, ExpectedResult = 19)]
        [TestCase(10, 10, true, ExpectedResult = 20)]   // charLvl 50 equals lvl 10 hard enemy
        [TestCase(34, 11, false, ExpectedResult = 19)]
        [TestCase(10, 11, true, ExpectedResult = 23)]
        [TestCase(22, 12, false, ExpectedResult = 16)]
        [TestCase(10, 12, true, ExpectedResult = 22)]
        [TestCase(30, 13, false, ExpectedResult = 19)]
        [TestCase(10, 13, true, ExpectedResult = 23)]
        public ushort SetEnemyStats(byte pLvl, byte eId, bool isHard) {
            ushort strength = 0;
            float multiplier = 1.0F;

            // increase difficulty by increasing stat multiplier
            if (isHard) multiplier += 0.75f;
            while (pLvl - 10 >= 0) {
                multiplier += 0.2F;
                pLvl -= 10;
            }

            // create Enemy with Id
            switch (eId) {
                case 1: // goblin
                    strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    break;
                 case 2: // assasin
                    strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    break;
                case 3: // witch
                    strength = Convert.ToUInt16(Math.Round(1.3 * multiplier));
                    break;
                case 4: // bandit
                    strength = Convert.ToUInt16(Math.Round(3 * multiplier));
                    break;
                case 5: // mercenary
                    strength = Convert.ToUInt16(Math.Round(3.3 * multiplier));
                    break;
                case 6: // paladin
                    strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    break;
                case 7: // plantara
                    strength = Convert.ToUInt16(Math.Round(6 * multiplier));
                    break;
                case 8: // beserk
                    strength = Convert.ToUInt16(Math.Round(8.4 * multiplier));
                    break;
                case 9: // Archmage
                    strength = Convert.ToUInt16(Math.Round(3 * multiplier));
                    break;
                case 10: // dragon
                    strength = Convert.ToUInt16(Math.Round(10.4 * multiplier));
                    break;
                case 11: // demon
                    strength = Convert.ToUInt16(Math.Round(11.6 * multiplier));
                    break; 
                case 12: // grifin
                    strength = Convert.ToUInt16(Math.Round(11.4 * multiplier));
                    break;
                case 13: // ashura
                    strength = Convert.ToUInt16(Math.Round(12 * multiplier));
                    break;
            }
            return strength;
        }
    }
}
