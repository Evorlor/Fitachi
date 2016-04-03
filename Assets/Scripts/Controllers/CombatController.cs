using System;
using System.Collections.Generic;
using System.Linq;
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
    private const string UpdateMatchesMethodName = "UpdateMatches";
    private const string UpdateMatchesUIMethodName = "UpdateMatchesUI";

    private List<Match> matches = new List<Match>();
    private UIMatch[] matchesUIArray;

    void Awake()
    {
        matchesUIArray = matchesUI.GetComponentsInChildren<UIMatch>();
    }

    void Start()
    {
        InvokeRepeating(UpdateMatchesMethodName, CurrentStatusPollTime, CurrentStatusPollTime);
        InvokeRepeating(UpdateMatchesUIMethodName, CurrentStatusPollTime, CurrentStatusPollTime);
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

    private void OnMatchFound(Match match)
    {
        var startMatchButtonText = searchForMatch.GetComponentInChildren<Text>();
        if (startMatchButtonText)
        {
            startMatchButtonText.text = MatchSearchable;
        }
        searchForMatch.interactable = true;
        matches.Add(match);
        UpdateMatchesUI();
    }

    private void UpdateMatches()
    {
        foreach(var match in matches)
        {
            ServerManager.Instance.UpdateMatch(match, OnMatchUpdated);
        }
    }

    private void OnMatchUpdated(Match match)
    {
        var clientMatch = matches.Where(m => m.id == match.id).First();
        int matchIndex = matches.IndexOf(clientMatch);
        matches[matchIndex] = match;
        UpdateMatchesUI();

		CheckForGameOver(match);
    }

	private void CheckForGameOver(Match match)
	{
		if (match.player0.hitPoints <= 0 || match.player1.hitPoints <= 0)
		{
			matches.Remove(match);
		}
	}

	private void UpdateMatchesUI()
    {
        for (int i = 0; i < matchesUIArray.Length; i++)
        {
            if (matches.Count > i)
            {
                matchesUIArray[i].gameObject.SetActive(true);
                matchesUIArray[i].UpdateUI(matches[i]);
            }
            else
            {
                matchesUIArray[i].gameObject.SetActive(false);
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
        var clientMatch = matches.Where(m => m.id == match.id).First();
        int matchIndex = matches.IndexOf(clientMatch);
        matches[matchIndex] = match;
        UpdateMatchesUI();
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