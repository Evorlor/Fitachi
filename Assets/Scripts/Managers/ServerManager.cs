using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    private const string ServerLink = "http://127.0.0.1:5000/";
    private const string TakeTurnMethod = "take_turn/";
    private const string CheckForTurnMethod = "check_for_turn/";

    public void NotifyOnTurnReady(Action<string> onTurnReady, float pollTime)
    {
        StartCoroutine(WaitForTurn(onTurnReady, pollTime));
    }

    public void TakeTurn(Action<string> onTurnComplete)
    {
        StartCoroutine(WaitForTurnCompletion(onTurnComplete));
    }

    private IEnumerator WaitForTurnCompletion(Action<string> onTurnComplete)
    {
        string url = ServerLink + TakeTurnMethod + PlayerManager.Instance.UserID;
        var www = new WWW(url);
        yield return new WaitUntil(() => www.isDone);
        string result = GetStringResult(www.bytes);
        bool turnComplete = bool.Parse(result);
        if (turnComplete)
        {
            onTurnComplete(result);
            yield break;
        }
    }

    private IEnumerator WaitForTurn(Action<string> onTurnReady, float pollTime)
    {
        string url = ServerLink + CheckForTurnMethod + PlayerManager.Instance.UserID;
        while (true)
        {
            var www = new WWW(url);
            yield return new WaitUntil(() => www.isDone);
            var result = GetStringResult(www.bytes);
            bool activeTurn = bool.Parse(result);
            if (activeTurn)
            {
                onTurnReady(result);
                yield break;
            }
            yield return new WaitForSeconds(pollTime);
        }
    }

    private string GetStringResult(byte[] bytes)
    {
        bytes = bytes.Where(x => x != 0x00).ToArray();
        var text = System.Text.Encoding.ASCII.GetString(bytes).Trim();
        return text;
    }
}