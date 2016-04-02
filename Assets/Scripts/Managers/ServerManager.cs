using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    public string ServerLink = "http://127.0.0.1:5000";
    private const string FindMatchMethodName = "find_match";
    private const string AttackMethodName = "attack";
    private const string PlayerUpdateMethodName = "get_player";
    private const string GetMatchStatusMethodName = "get_match_status";

    public void FindMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        StartCoroutine(WaitForMatch(player, onMatchFound, pollTime));
    }

    public void Attack(Action<string> onAttack, int matchNumber)
    {
        StartCoroutine(WaitForAttack(onAttack, matchNumber));
    }

    //public void GetPlayerJson(Action<string> onPlayerUpdated)
    //{
    //    StartCoroutine(WaitForPlayerUpdate(onPlayerUpdated));
    //}

    //private IEnumerator WaitForPlayerUpdate(Action<string> onPlayerUpdated)
    //{
    //    string player = JsonUtility.ToJson(PlayerManager.Instance.Player);
    //    string url = CreateUrl(PlayerUpdateMethodName, player);
    //    var www = new WWW(url);
    //    yield return new WaitUntil(() => www.isDone);
    //    var result = GetStringResult(www.bytes);
    //    onPlayerUpdated(result);
    //}

    private IEnumerator WaitForAttack(Action<string> onAttack, int matchNumber)
    {
        var player = PlayerManager.Instance.Player;
        //player.matchNumber = matchNumber;
        string playerJson = JsonUtility.ToJson(player);
        string url = CreateUrl(AttackMethodName, playerJson);
        var www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);
        onAttack(GetStringResult(www.bytes));
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