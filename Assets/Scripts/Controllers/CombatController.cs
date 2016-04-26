using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatController : ManagerBehaviour<CombatController>
{
    public GameObject victoryText;

    [Tooltip("Container for Matches UI")]
    [SerializeField]
    private CanvasRenderer matchesUI;

    [Tooltip("Search for match button")]
    [SerializeField]
    private Button searchForMatch;

    private const float MatchSearchPollTime = 2.0f;
    private const float CurrentStatusPollTime = 1.0f;
    private const string MatchSearchable = "Start Battle!";
    private const string SearchingForMatch = "Searching for match...";

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
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
        var playerData = match.player0.id == FitbitRestClient.Instance.GetUserId() ? match.player0.playerdata : match.player1.playerdata;
        var opponentData = match.player1.id == FitbitRestClient.Instance.GetUserId() ? match.player0.playerdata : match.player1.playerdata;
        AdventureStats.Dairy = playerData.dairy - opponentData.dairy;
        AdventureStats.Fruit = playerData.fruit - opponentData.fruit;
        AdventureStats.Grain = playerData.grain - opponentData.grain;
        AdventureStats.Protein = playerData.protein - opponentData.protein;
        AdventureStats.Sweets = playerData.sweets - opponentData.sweets;
        AdventureStats.Vegetable = playerData.vegetable - opponentData.vegetable;
        int total = AdventureStats.Dairy + AdventureStats.Fruit + AdventureStats.Grain + AdventureStats.Protein + AdventureStats.Sweets + AdventureStats.Vegetable;
        if (total > 0)
        {
            Debug.Log("YOU WIN!");
        }
        else if (total == 0)
        {
            Debug.Log("YOU TIE!");
        }
        else
        {
            Debug.Log("YOU LOSE!");
        }
        AdventureStats.Dairy = Mathf.Max(0, playerData.dairy - opponentData.dairy);
        AdventureStats.Fruit = Mathf.Max(0, playerData.fruit - opponentData.fruit);
        AdventureStats.Grain = Mathf.Max(0, playerData.grain - opponentData.grain);
        AdventureStats.Protein = Mathf.Max(0, playerData.protein - opponentData.protein);
        AdventureStats.Sweets = Mathf.Max(0, playerData.sweets - opponentData.sweets);
        AdventureStats.Vegetable = Mathf.Max(0, playerData.vegetable - opponentData.vegetable);
        // TODO: Determine the winner
        // Play the animation
        //var startMatchButtonText = searchForMatch.GetComponentInChildren<Text>();
        //if (startMatchButtonText)
        //{
        //    startMatchButtonText.text = MatchSearchable;
        //}
        //searchForMatch.interactable = true;
        //matches.Add(match);
    }

    private Player CreatePlayer()
    {
        var player = new Player();
        player.id = FitbitRestClient.Instance.GetUserId();
        player.playerdata = PlayerData.GetPlayerData();
        return player;
    }
}