using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public class PokerHand : IComparable
    {
        private readonly char[] suits = { 'S', 'H', 'D', 'C'}; // ♠ Spades - пики (лопата), ♥ Hearts, ♦ Diamonds, ♣ Clubs - трефы (крести)
        private readonly char[] cardValues = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'}; // Jack, Queen, King, Ace
        private readonly string[] values1 = { "Nothing", "Pair", "Two pairs", "Three of a kind",
                                             "Straight", "Flush", "Full house", "Four of a kind", "Straight flush" };

        private string _hand;
        private bool isFlush;
        private bool isStraight;
        public int Rank { get; private set; }
        public Dictionary<int, int> CardsByRank { get; private set; }

        public PokerHand(string hand)
        {
            _hand = hand;
            string values = "_23456789TJQKA";
            string valuesLowAce = "A23456789TJQK";

            var cards = hand.Split(' ')
                            .Select(x => new { Rank = values.IndexOf(x[0]),
                                               Suit = x[1] });

            var cardsLowAce = hand.Split(' ')
                                  .Select(x => new { Rank = valuesLowAce.IndexOf(x[0]),
                                                     Suit = x[1] });

            // straight without low ace: "5C 4D 3C 2S AS" is not straight
            isStraight = cards.Select(x => x.Rank)
                              .OrderBy(x => x)
                              .SequenceEqual(Enumerable.Range(cards.Min(x => x.Rank), 5));


            // straight with low ace: "5C 4D 3C 2S AS" is straight
            bool isStraightLowAce = cardsLowAce.Select(x => x.Rank)
                                               .OrderBy(x => x)
                                               .SequenceEqual(Enumerable.Range(cardsLowAce.Min(x => x.Rank), 5));

            if (!isStraight && isStraightLowAce)
            {
                isStraight = true;
                cards = cardsLowAce;
            }

            isFlush = cards.All(x => x.Suit == cards.First().Suit);

            CardsByRank = cards.GroupBy(x => x.Rank)
                               .OrderByDescending(x => x.Count())
                               .ThenByDescending(x => x.Key)
                               .ToDictionary(x => x.Key, x => x.Count());

            Rank = GetRank();
        }

        private int GetRank()
        {
            if (isFlush && isStraight)
            {
                return 8;                   // Straight flush
            }
            else if (CardsByRank.Any(x => x.Value == 4))
            {
                return 7;                   // Four of a kind
            }
            else if (CardsByRank.All(x => x.Value == 2 || x.Value == 3))
            {
                return 6;                   // Full house
            }
            else if (isFlush)
            {
                return 5;                   // Flush
            }
            else if (isStraight)
            {
                return 4;                   // Straight
            }
            else if (CardsByRank.Any(x => x.Value == 3))
            {
                return 3;                   // Three of a kind
            }
            else if (CardsByRank.Count(x => x.Value == 2) == 2)
            {
                return 2;                   // Two pairs
            }
            else if (CardsByRank.Any(x => x.Value == 2))
            {
                return 1;                   // Pair
            }

            return 0;
        }

        public Result CompareWith(PokerHand hand)
        {
            int compare = CompareTo(hand);

            if (compare == 0) return Result.Tie;

            return compare > 0 ? Result.Loss : Result.Win;
        }

        public int CompareTo(object obj)
        {
            PokerHand hand = obj as PokerHand;
            if (Rank > hand.Rank)
            {
                return -1;
            }

            if (Rank < hand.Rank)
            {
                return 1;
            }

            var ranks1 = CardsByRank.Keys.ToArray();
            var ranks2 = hand.CardsByRank.Keys.ToArray();

            for (int i = 0; i < ranks1.Count(); i++)
            {
                if (ranks1[i] != ranks2[i])
                {
                    return ranks1[i] > ranks2[i] ? -1 : 1;
                }
            }

            return 0;
        }

        public override string ToString()
        {
            return _hand;
        }

        //----------------Десятка обозначается как 10, а не T(en), как в прошлом задании ----------
        //---Конечный результат: сначала карты из комбинации (если она есть), а затем остальные. И все это в порядке убывания---
        //---Для full house - сначала номинал 3- карт, затем номинал 2-х карт ---
        public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
        {
            string[] values = "2 3 4 5 6 7 8 9 10 J Q K A".Split(' ');

            var cards = holeCards.Concat(communityCards)
                                    .Select(x => new
                                    {
                                        Rank = Array.IndexOf(values, x.Remove(x.Length - 1)),
                                        Suit = x[^1]
                                    });

            var cardsByRank = cards.GroupBy(x => x.Rank)
                                    .OrderByDescending(x => x.Count())
                                    .ThenByDescending(x => x.Key)
                                    .ToDictionary(x => x.Key, x => x.Count());

            var flush = cards.GroupBy(x => x.Suit)
                                .Select(x => x.ToList())
                                .Where(x => x.Count() >= 5);

            var keys = cardsByRank.Keys.OrderByDescending(x => x).ToArray();
            var straight = Enumerable.Range(0, 5 <= keys.Length ? keys.Length - 4 : 0)
                                        .Select(t => keys.Skip(t)
                                                        .TakeWhile((x, i) => 0 <= i && i < keys.Length - t && x == keys[t] - i)
                                                        .ToArray())
                                        .Where(x => x.Count() >= 5);

            if (flush.Any() && straight.Any())
            {
                for (int i = 0; i <= straight.First().Count() - 5; i++)
                {
                    var straightFlush = straight.First()
                                                .Skip(i)
                                                .Take(5)
                                                .Intersect(flush.First().Select(x => x.Rank));
                    if (straightFlush.Count() == 5)
                    {
                        return ("straight-flush", straightFlush.Select(x => values[x]).ToArray());
                    }
                }
            }

            string type = string.Empty;
            var ranks = Enumerable.Empty<string>();
            if (cardsByRank.Any(x => x.Value == 4))
            {
                type = "four-of-a-kind";
                ranks = cardsByRank.Take(1)
                                    .Append(cardsByRank.OrderByDescending(x => x.Key).First(x => x.Value != 4))
                                    .Select(x => values[x.Key]);
            }
            else if (cardsByRank.Count(x => x.Value == 3) == 2 || // full house maybe 3 + 3 + 1, 3 + 2 + 2 and 3 + 2 + 1 + 1
                    (cardsByRank.Count(x => x.Value == 3) == 1 && cardsByRank.Count(x => x.Value == 2) >= 1))
            {
                type = "full house";
                ranks = cardsByRank.Take(2)
                                    .Select(x => values[x.Key]);
            }
            else if (flush.Any())
            {
                type = "flush";
                ranks = flush.First()
                                .OrderByDescending(x => x.Rank)
                                .Take(5)
                                .Select(x => values[x.Rank]);
            }
            else if (straight.Any())
            {
                type = "straight";
                ranks = straight.First()
                                .Take(5)
                                .Select(x => values[x]);
            }
            else if (cardsByRank.Count(x => x.Value == 3) == 1)
            {
                type = "three-of-a-kind";
                ranks = cardsByRank.Take(3)
                                    .Select(x => values[x.Key]);
            }
            else if (cardsByRank.Count(x => x.Value == 2) == 3) // maybe 3 pairs
            {
                type = "two pair";
                var pairs = cardsByRank.Take(2);
                ranks = pairs.Append(cardsByRank.OrderByDescending(x => x.Key).First(x => !pairs.Contains(x)))
                                .Select(x => values[x.Key]);
            }
            else if (cardsByRank.Count(x => x.Value == 2) == 2)
            {
                type = "two pair";
                ranks = cardsByRank.Take(3)
                                    .Select(x => values[x.Key]);
            }
            else if (cardsByRank.Count(x => x.Value == 2) == 1)
            {
                type = "pair";
                ranks = cardsByRank.Take(4)
                                    .Select(x => values[x.Key]);
            }
            else if (cardsByRank.All(x => x.Value == 1))
            {
                type = "nothing";
                ranks = cardsByRank.Take(5)
                                    .Select(x => values[x.Key]);
            }

            return (type, ranks.ToArray());
        }

    }
}
