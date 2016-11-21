using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "All in on pairs";

		public static int BetRequest(JObject gameState)
		{
		    var players = gameState.SelectToken("players");
		    foreach (var player in players)
		    {
		        var holeCards = player.SelectToken("hole_cards");
		        if (holeCards != null)
		        {
		            var c1 = holeCards.SelectToken("rank")[0].Value<int>();
                    var c2 = holeCards.SelectToken("rank")[1].Value<int>();
		            if (c1 == c2)
		                return 10000;
		        }
            }
		    return 0;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

