using System.Collections.Generic;

namespace PokerHandsRanker.Interfaces
{
    public interface IHandRankerService
    {
        int RankHands(List<List<string>> hand);
        IRank RankHand(IList<string> hand);
    }
}