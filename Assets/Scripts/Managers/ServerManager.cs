using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    private const string ServerLink = "http://127.0.0.1:5000/";
    private const string TakeTurnMethod = "take_turn/";
    private const string CheckForTurnMethod = "check_for_turn/";

	public bool CheckForTurnResult = false;
	public bool ServerTakeTurnResult = false;

	public Coroutine CheckForTurn()
    {
        string url = ServerLink + CheckForTurnMethod + PlayerManager.Instance.UserID;

		return StartCoroutine(CheckForTurnTask(url));
    }

    public Coroutine ServerTakeTurn()
    {
        string url = ServerLink + TakeTurnMethod + PlayerManager.Instance.UserID;

		return StartCoroutine(ServerTakeTurnTask(url));
	}

    private string GetStringConversion(byte[] bytes)
    {
        bytes = bytes.Where(x => x != 0x00).ToArray();
        var text = System.Text.Encoding.ASCII.GetString(bytes).Trim();
        return text;
    }

	private IEnumerator CheckForTurnTask(string url)
	{
		var www = new WWW(url);
		while (!www.isDone)
		{
			yield return null;
        }

		var result = GetStringConversion(www.bytes);
		CheckForTurnResult = bool.Parse(result);
    }

	private IEnumerator ServerTakeTurnTask(string url)
	{
		var www = new WWW(url);
		while (!www.isDone)
		{
			yield return null;
		}

		var result = GetStringConversion(www.bytes);
		ServerTakeTurnResult = bool.Parse(result);
	}
}