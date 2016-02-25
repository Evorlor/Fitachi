using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    private const string ServerLink = "http://127.0.0.1:5000/";
    private const string TakeTurnMethod = "take_turn/";
    private const string CheckForTurnMethod = "check_for_turn/";

    public bool CheckForTurn()
    {
        string url = ServerLink + CheckForTurnMethod + PlayerManager.Instance.UserID;

        var www = new WWW(url);
        while (!www.isDone) { }

        var turnString = GetStringConversion(www.bytes);
        return bool.Parse(turnString);
    }

    public bool ServerTakeTurn()
    {
        string url = ServerLink + TakeTurnMethod + PlayerManager.Instance.UserID;

        var www = new WWW(url);
        while (!www.isDone) { }

        var success = GetStringConversion(www.bytes);
        return bool.Parse(success);
    }

    private string GetStringConversion(byte[] bytes)
    {
        bytes = bytes.Where(x => x != 0x00).ToArray();
        var text = System.Text.Encoding.ASCII.GetString(bytes).Trim();
        return text;
    }
}