using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    public string ServerLink = "http://127.0.0.1:5000";
    private const string CreatePlayerMethodName = "create_player";
    private const string FindMatchMethodName = "find_match";
    private const string AttackMethodName = "attack";

    public void FindMatch(Action onMatchFound, float pollTime)
    {
        StartCoroutine(WaitForMatch(onMatchFound, pollTime));
    }

    public void Attack(Action<string> onAttack)
    {
        StartCoroutine(WaitForAttack(onAttack));
    }

    private IEnumerator WaitForAttack(Action<string> onAttack)
    {
        string player = JsonUtility.ToJson(PlayerManager.Instance.Player);
        string url = CreateUrl(AttackMethodName, player);
        var www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);

        //temp code
        Debug.Log("attacking");
        var hitPointMonitor = FindObjectOfType<HitPointMonitor>();
        var enemy = JsonUtility.FromJson<Player>(GetStringResult(www.bytes));
        hitPointMonitor.UpdateHitpointText(PlayerManager.Instance.Player.token, PlayerManager.Instance.Player.hitPoints, enemy.token, enemy.hitPoints);
    }

    private IEnumerator WaitForMatch(Action onMatchFound, float pollTime)
    {
        string player = JsonUtility.ToJson(PlayerManager.Instance.Player);
        string url = CreateUrl(CreatePlayerMethodName, player);
        var www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);
        while (true)
        {
            url = CreateUrl(FindMatchMethodName);
            www = new WWW(url);
            yield return new WaitUntil(() => www.isDone);
            var result = GetStringResult(www.bytes);
            bool matchFound = bool.Parse(result);
            if (matchFound)
            {
                onMatchFound();
                yield break;
            }
            yield return new WaitForSeconds(pollTime);
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