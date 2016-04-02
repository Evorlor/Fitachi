using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    [Tooltip("Container for Matches UI")]
    [SerializeField]
    private CanvasRenderer matchesUI;

    private const float PollTime = 2.0f;
    private const string SearchingForMatch = "Searching for match...";
    private List<Match> matches = new List<Match>();

    public void FindMatch(Button matchButton)
    {
        var startMatchButtonText = matchButton.GetComponentInChildren<Text>();
        if (startMatchButtonText)
        {
            startMatchButtonText.text = SearchingForMatch;
        }
        matchButton.interactable = false;
        var player = CreatePlayer();
        ServerManager.Instance.FindMatch(player, OnMatchFound, PollTime);
    }

    private void OnMatchFound(Match match)
    {
        matches.Add(match);
        UpdateMatches(match);
    }

    private void UpdateMatches(Match match)
    {
        var matchesUIArray = matchesUI.GetComponentsInChildren<UIMatch>();
        for(int i = 0; i < matchesUIArray.Length; i++)
        {
            if(matches.Count > i)
            {
                matchesUIArray[i].enabled = true;
                matchesUIArray[i].UpdateUI(matches[i]);
            }
            else
            {
                matchesUIArray[i].enabled = false;
            }
        }
    }

    public void Attack(int matchIndex)
    {
        var match = matches[matchIndex];
        ServerManager.Instance.Attack(match, OnAttack);
    }

    private void OnAttack(Match match)
    {
        UpdateMatches(match);
    }

    private Player CreatePlayer()
    {
        var player = new Player();
        player.id = PlayerManager.ID;
        player.hitPoints = PlayerManager.StartingHitPoints;
        player.attackPower = PlayerManager.StartingAttackPower;
        return player;
    }

    //private const string PlayerTurnText = "It's your turn!";
    //private const string NotPlayerTurnText = "It's not your turn!";

    //[SerializeField]
    //[Tooltip("Keeps track of whose turn it is")]
    //private Text turnDisplay;

    //[SerializeField]
    //[Tooltip("How long of an interval between checks to see if it's the player's turn")]
    //private float pollTime = 2.0f;

    //void Start()
    //{
    //    turnDisplay.text = NotPlayerTurnText;
    //    WaitForTurn();
    //}

    //private void TakeTurn()
    //{
    //    //ServerManager.Instance.TakeTurn(OnTurnComplete);
    //}

    //private void OnTurnComplete(string result)
    //{
    //    bool success = bool.Parse(result);
    //    if (!success)
    //    {
    //        return;
    //    }
    //    turnDisplay.text = NotPlayerTurnText;
    //    WaitForTurn();
    //}

    //private void WaitForTurn()
    //{
    //    //ServerManager.Instance.NotifyOnTurnReady(OnTurnReady, pollTime);
    //}

    //private void OnTurnReady(string result)
    //{
    //    turnDisplay.text = PlayerTurnText;
    //}
}