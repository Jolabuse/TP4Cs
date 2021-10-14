using System;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using PokerHandsRanker;
using PokerHandsRanker.Interfaces;
using System.Collections.Generic;

namespace PokerHandsRankerTests
{
    public class HandRankerServiceTests
    {
        private IHandRankerService _handRankerService;
        private IRankService _rankService;

        [SetUp]
        public void SetUp()
        {
            _rankService = Substitute.For<IRankService>();
            _handRankerService = new HandRankerService(_rankService);
        }

        [Test]
        public void Should_Call_IRankService_When_Ranking_Hands()
        {
            // TODO
            var hand = new List<List<string>>(2);
            hand.Add(new List<string>());
            hand.Add(new List<string>());
            _handRankerService.RankHands(hand);
            _rankService.Received().GetRankFromHand(hand[0]);
        }

        [Test]
        public void Should_Have_Player1_Win_If_His_Hand_Is_Better()
        {
            // TODO
            var hand = new List<List<string>>(2);
            hand.Add(new List<string>() { "TD", "3C", "9C", "TC", "9D" });
            hand.Add(new List<string>() { "AS", "AD", "5C", "JS", "3H" });
            Assert.That(_handRankerService.RankHands(hand), Is.EqualTo(1));
        }


        [Test]
        public void Should_Have_Player2_Win_If_His_Hand_Is_Better()
        {
            // TODO
            var hand = new List<List<string>>(2);
            hand.Add(new List<string>() { "AC", "2D", "QS", "QH", "9C" });
            hand.Add(new List<string>() { "8D", "6H", "8H", "5S", "SD" });
            Assert.That(_handRankerService.RankHands(hand), Is.EqualTo(2));
            
            //it worked when you test on the normal running but here it magically doesn't work
            //see the word file
        }


        [Test]
        public void Should_Have_A_Tie_If_Hands_Are_Equal()
        {
            // TODO
            var hand = new List<List<string>>(2);
            hand.Add(new List<string>() { "AS", "AD", "5C", "JS", "3H" });
            hand.Add(new List<string>() { "AS", "AD", "5C", "JS", "3H" });
            Assert.That(_handRankerService.RankHands(hand), Is.EqualTo(1));
        }
    }
}
