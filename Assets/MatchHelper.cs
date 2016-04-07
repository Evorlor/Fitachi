using UnityEngine;

public class MatchHelper
{
    public static Player GetAttackingPlayer(Match match)
    {
        if (match.player0.id == match.turn.id)
        {
            return match.player0;
        }
        else
        {
            return match.player1;
        }
    }

    public static Player GetDefendingPlayer(Match match)
    {
        if (match.player0.id == match.turn.id)
        {
            return match.player1;
        }
        else
        {
            return match.player0;
        }
    }
}
