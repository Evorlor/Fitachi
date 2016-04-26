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

    private Player CreatePlayer()
    {
        var player = new Player();
        //player.hitPoints = int.Parse(FitbitRestClient.Instance.Activities.lifetime.total.steps) + 10;
        //player.attackPower = (int.Parse(FitbitRestClient.Instance.Activities.lifetime.total.steps) + 10) / UnityEngine.Random.Range(5, 10);
        player.id = FitbitRestClient.Instance.GetUserId();
        //player.hitPoints = AdventureStats.Endurance.HeartRate;
        //player.attackPower = AdventureStats.Speed.Steps;
        return player;
    }
}