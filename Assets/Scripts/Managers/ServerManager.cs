using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    public string ServerLink = "http://10.8.3.43:5000";
    private const string FindMatchMethodName = "find_match";
    private const string AttackMethodName = "attack";
    private const string PlayerUpdateMethodName = "get_player";
    private const string GetMatchStatusMethodName = "get_match_status";
    private const string UpdateMatchMethodName = "update_match";

    public void FindMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        StartCoroutine(WaitForMatch(player, onMatchFound, pollTime));
    }

    public void Attack(Match match, Action<Match> onAttack)
    {
        StartCoroutine(WaitForAttack(match, onAttack));
    }

    public void UpdateMatch(Match match, Action<Match> onMatchUpdated)
    {
        StartCoroutine(WaitForMatchUpdate(match, onMatchUpdated));
    }

    private IEnumerator WaitForMatchUpdate(Match match, Action<Match> onMatchUpdated)
    {
        string matchJson = JsonUtility.ToJson(match);
        string method = UpdateMatchMethodName;
        var www = CreatePost(method, matchJson);
        yield return new WaitUntil(() => www.isDone);
        var result = GetStringResult(www.bytes);
        match = JsonUtility.FromJson<Match>(result);
        onMatchUpdated(match);
    }

    private IEnumerator WaitForAttack(Match match, Action<Match> onAttack)
    {
        string matchJson = JsonUtility.ToJson(match);
        string method = AttackMethodName;
        var www = CreatePost(method, matchJson);
		yield return new WaitUntil(() => www.isDone);
        var result = GetStringResult(www.bytes);
        match = JsonUtility.FromJson<Match>(result);
        onAttack(match);
    }

    private IEnumerator WaitForMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        string playerJson = JsonUtility.ToJson(player);
        string method = FindMatchMethodName;
        var www = CreatePost(method, playerJson);
		while (true)
        {
            yield return new WaitUntil(() => www.isDone);
            var result = GetStringResult(www.bytes);
            var match = JsonUtility.FromJson<Match>(result);
            if (match.id > 0)
            {
                onMatchFound(match);
            }
            yield return new WaitForSeconds(pollTime);
			method = GetMatchStatusMethodName;
            www = CreatePost(method, playerJson);
		}
    }

    private string GetStringResult(byte[] bytes)
    {
        bytes = bytes.Where(x => x != 0x00).ToArray();
        var text = System.Text.Encoding.ASCII.GetString(bytes).Trim();
        return text;
    }

	private WWW CreatePost(string method, string postData)
	{
		string url = ServerLink + "/" + method;
		WWWForm form = new WWWForm();
		form.AddField("post_data", postData);
		return new WWW(url, form);
	}
}