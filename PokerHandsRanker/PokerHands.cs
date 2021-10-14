using System;
using System.Collections.Generic;
using PokerHandsRanker.Interfaces;

namespace PokerHandsRanker
{
    public class PokerHands : IPokerHands
    {
        private readonly IDeckService _deckService;
        private readonly IHandPrinterService _handPrinterService;
        private readonly IHandRankerService _handRankerService;

        public PokerHands(IHandPrinterService handPrinterService, 
            IDeckService deckService, 
            IHandRankerService handRankerService)
        {
            _handPrinterService = handPrinterService;
            _deckService = deckService;
            _handRankerService = handRankerService;
        }

        public bool HandSize(List<List<string>> hand)
        {
            foreach (var handP in hand)
            {
                if (handP.Count != 5)
                    return false;
            }
            return true;
        }
        public void Rank()
        {
            var gameOver = false;
            while (!gameOver)
            {
                var nbPlayer=0;
                string res;
                do
                {
                    Console.WriteLine("Enter the nb of player");
                    
                    try
                    {
                        res = Console.ReadLine();
                        nbPlayer = Int32.Parse(res);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Enter an int");
                    }
                } while (nbPlayer<2 || nbPlayer>8);

                var nbDeck=0;
                res = "0";
                do
                {
                    Console.WriteLine("Enter the nb of decks");
                    
                    try
                    {
                        res = Console.ReadLine();
                        nbDeck = Int32.Parse(res);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Enter an int");
                    }
                } while (nbDeck<1 || nbDeck>4);

                var hand = new List<List<string>>(nbPlayer);
                
                /*var handP1 = new List<string>();
                var handP2 = new List<string>();*/
                
                var deck = _deckService.InitDeck(nbDeck);
                
                    /*_deckService.DrawCard(handP1, deck);
                    _deckService.DrawCard(handP2, deck);*/
                
                for (var i=0;i<nbPlayer;i++)
                {
                    hand.Add(new List<string>());
                    for (int j = 0; j < 5; j++)
                    {
                        _deckService.DrawCard(hand[i], deck);
                    }

                }

                var rankHand = new List<IRank>(nbPlayer);
                for (var i=0;i<nbPlayer;i++)
                {
                    rankHand.Add(_handRankerService.RankHand(hand[i]));
                }
                /*var rankHandP1 = _handRankerService.RankHand(handP1);
                var rankHandP2 = _handRankerService.RankHand(handP2);*/
                
                

                for (var i = 0; i < nbPlayer; i++)
                {
                    _handPrinterService.PrintHand(i+1,hand[i], rankHand[i]);
                }
                /*_handPrinterService.PrintHand(1, handP1, rankHandP1);
                _handPrinterService.PrintHand(2, handP2, rankHandP2);*/

                var winner = _handRankerService.RankHands(hand);

                Console.WriteLine(winner != 0 ? "Player {0} won this round !" : "It's a tie !",winner);
                Console.WriteLine("Play another hand ? Or press 'q' to quit...");
                if (Console.ReadKey().KeyChar.Equals('q'))
                {
                    gameOver = true;
                }
                Console.Clear();
            }
        }
    }
}