using CsConsoleGame;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace TestCsConsoleGame
{
    [TestFixture]
    public class TestWorkerServices
    {
        [TestCase(false, 4, 3333, ExpectedResult = 1)]
        [TestCase(false, 4, 2222, ExpectedResult = 0)]
        [TestCase(false, 3, 2356, ExpectedResult = 1)]
        [TestCase(false, 3, 1357, ExpectedResult = 0)]
        [TestCase(false, 2, 1577, ExpectedResult = 1)]
        [TestCase(false, 2, 800, ExpectedResult = 0)]
        [TestCase(false, 1, 1277, ExpectedResult = 1)]
        [TestCase(false, 1, 500, ExpectedResult = 0)]
        [TestCase(true, 4, 3333, ExpectedResult = 1)]
        [TestCase(true, 4, 2222, ExpectedResult = 0)]
        [TestCase(true, 5, 5000, ExpectedResult = -1)]
        [TestCase(true, 3, 2356, ExpectedResult = 1)]
        [TestCase(true, 3, 1357, ExpectedResult = 0)]
        [TestCase(true, 2, 1577, ExpectedResult = 1)]
        [TestCase(true, 2, 800, ExpectedResult = 0)]
        [TestCase(true, 1, 1277, ExpectedResult = 1)]
        [TestCase(true, 1, 500, ExpectedResult = 0)]
        /// <summary>
        /// -1 = Error: Max lvl - no more upgrades
        /// 0 = Error: not enough money
        /// 1 = OK
        /// </summary>
        public static int TestIncreaseService(bool isHealer, byte lvl, int money) {
            // arrange
            IWorkerServices Iws;
            if (isHealer) Iws = new Healer(string.Empty);
            else Iws = new Looter(string.Empty);

            Iws.Lvl = lvl;
            Iws.UpgradeCost = SetUpgradeCost(Iws);

            if (Iws.Lvl == 5) return -1;
            else if (Iws.Lvl != 5 && money >= Iws.UpgradeCost) return 1;
            return 0;
        }

        public static ushort SetUpgradeCost(IWorkerServices ws) {
            Random r = new Random();
            switch (ws.Lvl) {
                case 1:
                    ws.UpgradeCost = (ushort)(500 + r.Next(1, 777));
                    break;
                case 2:
                    ws.UpgradeCost = (ushort)(800 + r.Next(1, 777));
                    break;
                case 3:
                    ws.UpgradeCost = (ushort)(1357 + r.Next(1, 999));
                    break;
                case 4:
                    ws.UpgradeCost = (ushort)(2222 + r.Next(1, 1111));
                    break;
            }
            return ws.UpgradeCost;
        }
    }
}
