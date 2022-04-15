using feistel;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace feistel.Tests
{
    [TestClass()]
    public class MainFeistelClassTest
    {
        private static Feistel FeistelTestVar = new Feistel(4);

        [TestMethod()]
        public void FillerTest()
        {
            Assert.AreEqual("000000101010", Feistel.Filler("101010", 12));
            Assert.AreEqual("101010", Feistel.Filler("101010", 3));
            Assert.AreEqual("000000", Feistel.Filler("", 6));
        }

        [TestMethod()]
        public void RotateLeftTest()
        {
            Assert.AreEqual(1, Feistel.RotateLeft(256, 8));
            Assert.AreEqual(226, Feistel.RotateLeft(113, 8));
        }

        [TestMethod()]
        public void ExponentiationByModulusTest()
        {
            Assert.AreEqual(945, Feistel.ExponentiationByModulus(663, 920, 948));
        }

        [TestMethod()]
        public void FeistelTest()
        {
            Assert.AreEqual("0110011111000000", FeistelTestVar.Encode("0001111110011101", "10011010"));
            Assert.AreEqual("0001101111101100", FeistelTestVar.Encode("1110000111010001", "10001110"));
        }
    }
}
