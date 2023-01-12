using CsConsoleGame;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Threading;

namespace TestCsConsoleGame
{
    [TestFixture]
    internal class TestMarketPlace
    {
        [TestCase(0.20, new short[] { 10, 25 }, ExpectedResult = 15)]
        [TestCase(0.20, new short[] { 8, 40 }, ExpectedResult = 16)]
        [TestCase(0.33, new short[] { 10, 25 }, ExpectedResult = 18)]
        [TestCase(0.60, new short[] { 10, 25 }, ExpectedResult = 25)]
        [TestCase(1.11, new short[] { 2, 25 }, ExpectedResult = 25)]
        [TestCase(-0.40, new short[] { 12, 25 }, ExpectedResult = 2)]
        public short HealerPerzent(double healPerzent, short[] health) {
            double healValue = Math.Round(health[1] * healPerzent);
            health[0] += (short)healValue;
            if (health[0] > health[1]) health[0] = health[1];

            return health[0];
        }

        [TestCase(0, 80, true, ExpectedResult = -2)]
        [TestCase(100, 200, true, ExpectedResult = -1)]
        //[TestCase(100, 80, true, ExpectedResult = 180)] // can be wrong
        //[TestCase(100, 100, true, ExpectedResult = 200)] // can be wrong
        public int RedBlack(int playerGold, int stake, bool playerIsRed) {
            Random r = new Random();
            bool gameIsRed = false;

            if (playerGold <= 0) return -2;
            if(playerGold < stake) return -1;

            if (r.Next(1, 3) == 1) gameIsRed = true;
            else gameIsRed = false;

            if (playerIsRed == gameIsRed) return playerGold + Convert.ToInt32(stake);
            else return playerGold - Convert.ToInt32(stake);
        }

        [TestCase(0, 1, 3, ExpectedResult = -1)]
        [TestCase(200, 1, 20, ExpectedResult = -2)]
        [TestCase(233, 2, 20, ExpectedResult = -2)]
        [TestCase(199, 3, 20, ExpectedResult = -2)]
        [TestCase(300, 1, 20, ExpectedResult = 0)]
        [TestCase(333, 1, 20, ExpectedResult = 33)]
        [TestCase(400, 2, 20, ExpectedResult = 150)]
        [TestCase(250, 2, 20, ExpectedResult = 0)]
        [TestCase(222, 3, 20, ExpectedResult = 22)]
        [TestCase(208, 3, 20, ExpectedResult = 8)]
        public int StatPushOverView(int playerGold, byte input, byte lvl) {
            ushort price = 0;

            if (lvl < 9) return -1;

                switch (input) {
                    case 1:
                        price = 300;
                        if (playerGold < price) return -2;
                        break;
                    case 2:
                        price = 250;
                        if (playerGold < price) return -2;
                        break;
                    case 3:
                        price = 200;
                        if (playerGold < price) return -2;
                        break;
                }
                return playerGold - price;
        }
    }
}
