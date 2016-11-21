using System;
using System.CodeDom;
using Nancy.Hosting.Self;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	class App
	{
		const string StagingPort = "8080";

		static readonly string HOST = Environment.GetEnvironmentVariable ("HOST");
		static readonly string PORT = Environment.GetEnvironmentVariable ("PORT");

		static NancyHost Host;

		enum Env { Staging, Deployment }

		static Env CurrentEnv {
			get {
				return HOST == null ? Env.Staging : Env.Deployment;
			}
		}

		static Uri CurrentAddress {
			get {
				switch (CurrentEnv) {
				case Env.Staging:
					return new Uri ("http://0.0.0.0:8080");
				case Env.Deployment:
					return new Uri (HOST.Substring(0, HOST.Length - 1) + ":" + PORT);
				default:
					throw new Exception ("Unexpected environment");
				}
			}
		}

		static void Main (string[] args)
		{
		    //test();
		    //return;
			Host = new NancyHost (CurrentAddress);
			Host.Start ();
			Console.WriteLine ("Nancy is started and listening on {0}...", CurrentAddress);
			while (Console.ReadLine () != "quit");
			Host.Stop ();
		}

	    private static void test()
	    {
	        var gs = JObject.Parse(@"{
""tournament_id"":""550d1d68cd7bd10003000003"",     // Id of the current tournament

    ""game_id"":""550da1cb2d909006e90004b1"",           // Id of the current sit'n'go game. You can use this to link a
                                                    // sequence of game states together for logging purposes, or to
                                                    // make sure that the same strategy is played for an entire game

    ""round"":0,                                      // Index of the current round within a sit'n'go

    ""bet_index"":0,                                  // Index of the betting opportunity within a round

    ""small_blind"": 10,                              // The small blind in the current round. The big blind is twice the
                                                    //     small blind

    ""current_buy_in"": 320,                          // The amount of the largest current bet from any one player

    ""pot"": 400,                                     // The size of the pot (sum of the player bets)

    ""minimum_raise"": 240,                           // Minimum raise amount. To raise you have to return at least:
                                                    //     current_buy_in - players[in_action][bet] + minimum_raise

    ""dealer"": 1,                                    // The index of the player on the dealer button in this round
                                                    //     The first player is (dealer+1)%(players.length)

    ""orbits"": 7,                                    // Number of orbits completed. (The number of times the dealer
                                                    //     button returned to the same player.)

    ""in_action"": 1,                                 // The index of your player, in the players array

    ""players"": [                                    // An array of the players. The order stays the same during the
        {                                           
            ""id"": 0,                               

            ""name"": ""Albert"",                     

            ""status"": ""active"",                    
            ""version"": ""Default random player"", 

            ""stack"": 1010,                        
                                                    

            ""bet"": 320                             
        },
        {
            ""id"": 1,                              
            ""name"": ""Bob"",
            ""status"": ""active"",
            ""version"": ""Default random player"",
            ""stack"": 1590,
            ""bet"": 80,
            ""hole_cards"": [                       
                                                    
                {
                    ""rank"": ""K"",                
                    ""suit"": ""hearts""            
                },
                {
                    ""rank"": ""K"",
                    ""suit"": ""spades""
                }
            ]
        },
        {
            ""id"": 2,
            ""name"": ""Chuck"",
            ""status"": ""out"",
            ""version"": ""Default random player"",
            ""stack"": 0,
            ""bet"": 0
        }
    ],
    ""community_cards"": [                            
        {
            ""rank"": ""4"",
            ""suit"": ""spades""
        },
        {
            ""rank"": ""A"",
            ""suit"": ""hearts""
        },
        {
            ""rank"": ""6"",
            ""suit"": ""clubs""
        }
    ]
}");
	        Console.WriteLine(gs);
	        PokerPlayer.BetRequest(gs);

	    }
	}
}
