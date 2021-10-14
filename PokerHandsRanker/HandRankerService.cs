using System.Collections.Generic;
using PokerHandsRanker.Interfaces;

namespace PokerHandsRanker
{
    public class HandRankerService : IHandRankerService
    {
        private readonly IRankService _rankService;

        public HandRankerService(IRankService rankService)
        {
            _rankService = rankService;
        }

        public int RankHands(List<List<string>> hand)
        {
            var rankHand = new List<IRank>(hand.Count);
            for (var i = 0; i < hand.Count; i++)
            {
                rankHand.Add(RankHand(hand[i]));
            }
            /*var rankHandP1 = RankHand(handP1);
            var rankHandP2 = RankHand(handP2);*/


            bool? pWonbool;
            var pWon = 0;

            for (var i = 1; i < hand.Count; i++)
            {
                pWonbool = rankHand[pWon].IsBetterRank(rankHand[i]) == true;
                if (pWonbool == false)
                {
                    pWon = i;
                }
            }

            

            return pWon+1;
        }

        public IRank RankHand(IList<string> hand)
        {
            return _rankService.GetRankFromHand(hand);
        }
    }
}