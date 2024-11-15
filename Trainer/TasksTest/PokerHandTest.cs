using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tasks;

namespace PokerHandTest
{
    class Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void TestPokerHandCreate()
        {
            Assert.AreEqual(8, new PokerHand("4H 5H 6H 7H 8H").Rank, "Straight flush");
            Assert.AreEqual(7, new PokerHand("9H 8D 9D 9S 9C").Rank, "Four of a kind");
            Assert.AreEqual(6, new PokerHand("2S AH 2H AS AC").Rank, "Full house");
            Assert.AreEqual(5, new PokerHand("4H 5H JH 7H 8H").Rank, "Flush");
            Assert.AreEqual(4, new PokerHand("4S 5H 3D 2H 6H").Rank, "Straight");
            Assert.AreEqual(4, new PokerHand("2D AC 3H 4H 5S").Rank, "Straight with low ace");
            Assert.AreEqual(3, new PokerHand("KH KD 6S KS AC").Rank, "Three of a kind");
            Assert.AreEqual(2, new PokerHand("2H TD 2S TC AC").Rank, "Two pairs");
            Assert.AreEqual(1, new PokerHand("2H QD 2S TC AC").Rank, "Pair");
            Assert.AreEqual(0, new PokerHand("2H QD 5S TC AC").Rank, "High card");
        }


        [TestCase("Highest straight then straight with low ace", Result.Win, "4S 5H 3D 2H 6H", "2D AC 3H 4H 5S")]

        [TestCase("Highest straight flush wins", Result.Loss, "2H 3H 4H 5H 6H", "KS AS TS QS JS")]
        [TestCase("Straight flush wins of 4 of a kind", Result.Win, "2H 3H 4H 5H 6H", "AS AD AC AH JD")]
        [TestCase("Highest 4 of a kind wins", Result.Win, "AS AH 2H AD AC", "JS JD JC JH 3D")]
        [TestCase("4 Of a kind wins of full house", Result.Loss, "2S AH 2H AS AC", "JS JD JC JH AD")]
        [TestCase("Full house wins of flush", Result.Win, "2S AH 2H AS AC", "2H 3H 5H 6H 7H")]
        [TestCase("Highest flush wins", Result.Win, "AS 3S 4S 8S 2S", "2H 3H 5H 6H 7H")]
        [TestCase("Flush wins of straight", Result.Win, "2H 3H 5H 6H 7H", "2S 3H 4H 5S 6C")]
        [TestCase("Equal straight is tie", Result.Tie, "2S 3H 4H 5S 6C", "3D 4C 5H 6H 2S")]
        [TestCase("Straight wins of three of a kind", Result.Win, "2S 3H 4H 5S 6C", "AH AC 5H 6H AS")]
        [TestCase("3 Of a kind wins of two pair", Result.Loss, "2S 2H 4H 5S 4C", "AH AC 5H 6H AS")]
        [TestCase("2 Pair wins of pair", Result.Win, "2S 2H 4H 5S 4C", "AH AC 5H 6H 7S")]
        [TestCase("Highest pair wins", Result.Loss, "6S AD 7H 4S AS", "AH AC 5H 6H 7S")]
        [TestCase("Pair wins of nothing", Result.Loss, "2S AH 4H 5S KC", "AH AC 5H 6H 7S")]
        [TestCase("Highest card loses", Result.Loss, "2S 3H 6H 7S 9C", "7H 3C TH 6H 9S")]
        [TestCase("Highest card wins", Result.Win, "4S 5H 6H TS AC", "3S 5H 6H TS AC")]
        [TestCase("Equal cards is tie", Result.Tie, "2S AH 4H 5S 6C", "AD 4C 5H 6H 2C")]
        public static void TestPokerHand(string description, Result expected, string hand, string opponentHand)
        {
            Assert.AreEqual(expected, new PokerHand(hand).CompareWith(new PokerHand(opponentHand)), description);
        }


        [Test]
        public void PokerHandSortTest()
        {
            // Arrange
            var expected = new List<PokerHand> {
                new PokerHand("KS AS TS QS JS"),
                new PokerHand("2H 3H 4H 5H 6H"),
                new PokerHand("AS AD AC AH JD"),
                new PokerHand("JS JD JC JH 3D"),
                new PokerHand("2S AH 2H AS AC"),
                new PokerHand("AS 3S 4S 8S 2S"),
                new PokerHand("2H 3H 5H 6H 7H"),
                new PokerHand("2S 3H 4H 5S 6C"),
                new PokerHand("2D AC 3H 4H 5S"),
                new PokerHand("AH AC 5H 6H AS"),
                new PokerHand("2S 2H 4H 5S 4C"),
                new PokerHand("AH AC 5H 6H 7S"),
                new PokerHand("AH AC 4H 6H 7S"),
                new PokerHand("2S AH 4H 5S KC"),
                new PokerHand("2S 3H 6H 7S 9C") };

            var random = new Random((int)DateTime.Now.Ticks);
            var actual = expected.OrderBy(x => random.Next()).ToList();
            // Act
            actual.Sort();
            // Assert
            for (var i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i], "Unexpected sorting order at index {0}", i);
        }

        // expected: hand name                    cards                  input -> hole cards             community cards
        [TestCase("nothing",         new[] { "A", "K", "Q", "J", "9" }, new[] { "K♠", "A♦" }, new[] { "J♣", "Q♥", "9♥", "2♥", "3♦" })]
        [TestCase("pair",            new[] { "Q", "K", "J", "9" },      new[] { "K♠", "Q♦" }, new[] { "J♣", "Q♥", "9♥", "2♥", "3♦" })]
        [TestCase("pair",            new[] { "3", "K", "Q", "J" },      new[] { "K♠", "3♥" }, new[] { "J♣", "Q♥", "9♥", "2♥", "3♦" })]
        [TestCase("two pair",        new[] { "K", "J", "9" },           new[] { "K♠", "J♦" }, new[] { "J♣", "K♥", "9♥", "2♥", "3♦" })] // 2(K + J)
        [TestCase("two pair",        new[] { "K", "2", "J" },           new[] { "K♠", "2♦" }, new[] { "J♣", "K♥", "9♥", "2♥", "3♦" })] // 2(K + 2)
        [TestCase("two pair",        new[] { "J", "2", "K" },           new[] { "J♠", "2♦" }, new[] { "J♣", "K♥", "9♥", "2♥", "3♦" })] // 2(J + 2)
        [TestCase("two pair",        new[] { "K", "J", "9" },           new[] { "J♠", "2♦" }, new[] { "J♣", "K♥", "9♥", "2♥", "K♦" })] // 2(K + J + 2)
        [TestCase("three-of-a-kind", new[] { "Q", "J", "9" },           new[] { "8♠", "7♦" }, new[] { "J♣", "Q♥", "Q♠", "9♥", "Q♦" })]
        [TestCase("straight",        new[] { "K", "Q", "J", "10", "9" }, new[] { "Q♠", "2♦" }, new[] { "J♣", "10♥", "9♥", "K♥", "3♦" })]
        [TestCase("straight",        new[] { "Q", "J", "10", "9", "8" }, new[] { "Q♠", "7♦" }, new[] { "J♣", "10♥", "9♥", "A♥", "8♦" })]
        [TestCase("straight",        new[] { "J", "10", "9", "8", "7" }, new[] { "7♠", "K♦" }, new[] { "J♣", "10♥", "9♥", "A♥", "8♦" })]
        [TestCase("flush",          new[] { "8", "7", "6", "5", "3" },  new[] { "8♠", "6♠" }, new[] { "7♠", "5♠", "4♦", "3♠", "2♠" })] // not straight-flush
        [TestCase("flush",          new[] { "Q", "J", "10", "5", "3" }, new[] { "A♠", "K♦" }, new[] { "J♥", "5♥", "10♥", "Q♥", "3♥" })]
        [TestCase("flush",          new[] { "K", "Q", "J", "10", "5" }, new[] { "A♠", "K♥" }, new[] { "J♥", "5♥", "10♥", "Q♥", "3♥" })]
        [TestCase("flush",          new[] { "7", "6", "5", "3", "2" },  new[] { "8♦", "6♠" }, new[] { "7♠", "5♠", "9♦", "3♠", "2♠" })]
        [TestCase("full house",     new[] { "A", "K" }, new[] { "A♠", "A♦" }, new[] { "K♣", "K♥", "A♥", "Q♥", "3♦" })]
        [TestCase("full house",     new[] { "A", "K" }, new[] { "A♠", "A♦" }, new[] { "K♣", "K♥", "A♥", "3♥", "3♦" })]
        [TestCase("full house",     new[] { "A", "K" }, new[] { "A♠", "A♦" }, new[] { "K♣", "K♥", "A♥", "3♥", "K♦" })]
        [TestCase("four-of-a-kind", new[] { "3", "2" }, new[] { "2♠", "3♦" }, new[] { "2♣", "2♥", "3♠", "3♥", "3♦" })]
        [TestCase("four-of-a-kind", new[] { "2", "3" }, new[] { "2♠", "3♦" }, new[] { "2♣", "2♥", "3♠", "3♥", "2♦" })]
        [TestCase("four-of-a-kind", new[] { "3", "A" }, new[] { "A♠", "3♦" }, new[] { "2♣", "2♥", "3♠", "3♥", "3♦" })]
        [TestCase("straight-flush", new[] { "J", "10", "9", "8", "7" }, new[] { "8♠", "6♠" }, new[] { "7♠", "5♠", "9♠", "J♠", "10♠" })]
        [TestCase("straight-flush", new[] { "9", "8", "7", "6", "5" },  new[] { "8♠", "6♠" }, new[] { "7♠", "5♠", "9♠", "J♠", "10♦" })]

        public static void TestHand(string type, string[] ranks, string[] holeCards, string[] communityCards)
        {
            var actual = PokerHand.Hand(holeCards, communityCards);
            Assert.AreEqual(type, actual.type);
            Assert.AreEqual(ranks, actual.ranks);
        }

    }
}
