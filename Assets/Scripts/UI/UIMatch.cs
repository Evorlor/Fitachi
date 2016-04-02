using UnityEngine;
using UnityEngine.UI;

public class UIMatch : MonoBehaviour
{
    private const string MatchIDPrefix = "Match #";
    private const string PlayerHitpointsPrefix = "Player HP: ";
    private const string EnemyHitpointsPrefix = "Enemy HP: ";

    [Tooltip("Text which shows the match ID")]
    [SerializeField]
    private Text matchID;

    [Tooltip("Text which shows the player hitpoints")]
    [SerializeField]
    private Text playerHitpoints;

    [Tooltip("Text which shows the enemy hitpoints")]
    [SerializeField]
    private Text enemyHitpoints;

    [Tooltip("Button used to attack")]
    [SerializeField]
    private Button attack;

    public void UpdateUI(Match match)
    {
        matchID.text = MatchIDPrefix + match.id;
        playerHitpoints.text = PlayerHitpointsPrefix + match.player0.hitPoints;
        enemyHitpoints.text = EnemyHitpointsPrefix + match.player1.hitPoints;
        attack.interactable = match.turn.id == PlayerManager.ID;
    }
}