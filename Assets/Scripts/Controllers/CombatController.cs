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

    private List<Match> matches = new List<Match>();
    private UIMatch[] matchesUIArray;

    void OnLevelWasLoaded(int level)
    {
        if(level == 2)
        {
            Debug.Log("YIO");
            //matchesUI = GameObject.Find("Matches").GetComponent<CanvasRenderer>();
            matchesUIArray = new UIMatch[5];
            for(int i = 0; i < matchesUIArray.Length; i++)
            {
                matchesUIArray[i] = GameObject.Find("Match #" + (i + 1)).GetComponent<UIMatch>();
            }
            searchForMatch = GameObject.Find("Start Battle").GetComponent<Button>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        matchesUIArray = matchesUI.GetComponentsInChildren<UIMatch>();
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
		Debug.Log(JsonUtility.ToJson(match));
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