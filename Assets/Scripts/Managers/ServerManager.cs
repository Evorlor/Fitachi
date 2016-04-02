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

    public void FindMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        StartCoroutine(WaitForMatch(player, onMatchFound, pollTime));
    }

    public void Attack(Match match, Action<Match> onAttack)
    {
        StartCoroutine(WaitForAttack(match, onAttack));
    }

    private IEnumerator WaitForAttack(Match match, Action<Match> onAttack)
    {
        string matchJson = JsonUtility.ToJson(match);
        string url = CreateUrl(AttackMethodName, matchJson);
        var www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);
        var result = GetStringResult(www.bytes);
        match = JsonUtility.FromJson<Match>(result);
        onAttack(match);
    }

    private IEnumerator WaitForMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        string playerJson = JsonUtility.ToJson(player);
        string url = CreateUrl(FindMatchMethodName, playerJson);
        var www = new WWW(url);
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
            url = CreateUrl(GetMatchStatusMethodName, playerJson);
            www = new WWW(url);
        }
    }

    private string CreateUrl(string method, string parameters = null)
    {
        string url = ServerLink + "/" + method;
        if (parameters != null)
        {
            url += "/" + parameters;
        }
        return url;
    }

    private string GetStringResult(byte[] bytes)
    {
        bytes = bytes.Where(x => x != 0x00).ToArray();
        var text = System.Text.Encoding.ASCII.GetString(bytes).Trim();
        return text;
    }
}