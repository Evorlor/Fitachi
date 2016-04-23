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
	private const string UpdateMatchesMethodName = "UpdateMatches";

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
        GetExistingMatches();
        InvokeRepeating(UpdateMatchesMethodName, 0, CurrentStatusPollTime);
    }

    public void FindMatch()
    {
        //AdventureStats.Endurance.HeartRate = 1000;

        //AdventureStats.Endurance.HeartRate += 20;
        var startMatchButtonText = searchForMatch.GetComponentInChildren<Text>();
        if (startMatchButtonText)
        {
            startMatchButtonText.text = SearchingForMatch;
        }
        searchForMatch.interactable = false;
        var player = CreatePlayer();
        ServerManager.Instance.FindMatch(player, OnMatchFound, MatchSearchPollTime);
    }

    private void GetExistingMatches()
    {

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
        foreach (var match in matches)
        {
            ServerManager.Instance.UpdateMatch(match, OnMatchUpdated);
        }
        UpdateMatchesUI();
    }

    private void OnMatchUpdated(Match match)
    {
        var clientMatch = matches.Where(m => m.id == match.id).First();
        int matchIndex = matches.IndexOf(clientMatch);
        matches[matchIndex] = match;
        CheckForGameOver(match);
        UpdateMatchesUI();
    }

    private void CheckForGameOver(Match match)
    {
        if (match.player0.hitPoints <= 0 || match.player1.hitPoints <= 0)
        {
			//bool isPlayer0 = FitbitRestClient.GetUserId() == match.player0.id;
			//if (isPlayer0 && match.player1.hitPoints <= 0)
			//{
			//	victoryText.SetActive(true);
			//	Text tempText = victoryText.GetComponent<Text>();
			//	tempText.text = "VICTORY!";
			//	tempText.color = Color.green;
			//}
			//else
			//{
			//	victoryText.SetActive(true);
			//	Text tempText = victoryText.GetComponent<Text>();
			//	tempText.text = "DEFEAT.";
			//	tempText.color = Color.red;
			//}
			matches.Remove(match);
        }
    }

    private void UpdateMatchesUI()
    {
        for (int i = 0; i < matchesUIArray.Length; i++)
        {
            if (!matchesUIArray[i])
            {
                return;
            }
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

		//if (victoryText.activeSelf)
		//{
		//	victoryText.SetActive(false);
		//}
    }

    public void Attack(int matchIndex)
    {
        var match = matches[matchIndex];
        var attackingPlayer = MatchHelper.GetAttackingPlayer(match);
        attackingPlayer.attackPower = 5;
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
        //player.hitPoints = int.Parse(FitbitRestClient.Activities.lifetime.total.steps) + 10;
        //player.attackPower = (int.Parse(FitbitRestClient.Activities.lifetime.total.steps) + 10) / UnityEngine.Random.Range(5, 10);
        player.id = FitbitRestClient.GetUserId();
        //player.hitPoints = AdventureStats.Endurance.HeartRate;
        //player.attackPower = AdventureStats.Speed.Steps;
        return player;
    }
}