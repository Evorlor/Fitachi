using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    [Tooltip("Container for Matches UI")]
    [SerializeField]
    private CanvasRenderer matchesUI;

    [Tooltip("Search for match button")]
    [SerializeField]
    private Button searchForMatch;

    private const float MatchSearchPollTime = 2.0f;
    private const float CurrentStatusPollTime = 2.0f;
    private const string MatchSearchable = "Start Battle!";
    private const string SearchingForMatch = "Searching for match...";
    private const string UpdateStatusMethodName = "UpdateStatus";

    private List<Match> matches = new List<Match>();

    void Start()
    {
        InvokeRepeating(UpdateStatusMethodName, CurrentStatusPollTime, CurrentStatusPollTime);
    }

    public void FindMatch()
    {
        var startMatchButtonText = searchForMatch.GetComponentInChildren<Text>();
        if (startMatchButtonText)
        {
            startMatchButtonText.text = SearchingForMatch;
        }
        searchForMatch.interactable = false;
        var player = CreatePlayer();
        ServerManager.Instance.FindMatch(player, OnMatchFound, MatchSearchPollTime);
    }

    private void UpdateStatus()
    {
        UpdateMatches();
    }

    private void OnMatchFound(Match match)
    {
        var startMatchButtonText = searchForMatch.GetComponentInChildren<Text>();
        if (startMatchButtonText)
        {
            startMatchButtonText.text = MatchSearchable;
        }
        searchForMatch.interactable = true;
        matches.Add(match);
        UpdateMatches();
    }

    private void UpdateMatches()
    {
        var matchesUIArray = matchesUI.GetComponentsInChildren<UIMatch>();
        for (int i = 0; i < matchesUIArray.Length; i++)
        {
            if (matches.Count > i)
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
        UpdateMatches();
    }

    private Player CreatePlayer()
    {
        var player = new Player();
        player.id = PlayerManager.ID;
        player.hitPoints = PlayerManager.StartingHitPoints;
        player.attackPower = PlayerManager.StartingAttackPower;
        return player;
    }
}