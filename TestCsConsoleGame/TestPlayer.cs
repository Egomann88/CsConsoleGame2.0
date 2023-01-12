using CsConsoleGame;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestCsConsoleGame
{
    [TestFixture]
    internal class TestPlayer
    {
        [TestCase(20, new uint[] { 20, 200 }, ExpectedResult = 20)]
        [TestCase(20, new uint[] { 20, 200 }, ExpectedResult = 20)]
        [TestCase(22, new uint[] { 200, 200 }, ExpectedResult = 23)]
        [TestCase(20, new uint[] { 5903, 200 }, ExpectedResult = 21)]
        [TestCase(100, new uint[] { 810, 200 }, ExpectedResult = 100)]
        public byte IncreaseLvl(byte lvl, uint[] exp) {
            // if lvl 100 is reached, no more leveling
            if (lvl >= 100) {
                lvl = 100;
                exp = new uint[2] { 0, 0 };
                return lvl;
            } else if (exp[0] >= exp[1]) { // allows multiple lvl ups
                //Console.WriteLine("\n{0} ist ein Level aufgestiegen.\n{0} ist nun Level {1}.", Name, ++lvl);
                lvl++;
                exp[0] -= exp[1];
                exp[1] += (byte)(5 + lvl);

                return lvl;
            }
            return lvl;
        }
    }
}
