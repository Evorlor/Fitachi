using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ServerManager : ManagerBehaviour<ServerManager>
{
    public string ServerLink = "http://10.8.3.43:5000";
    private const string FindMatchMethodName = "find_match";
    private const string GetMatchStatusMethodName = "get_match_status";

    public void FindMatch(Player player, Action<Match> onMatchFound, float pollTime)
    {
        StartCoroutine(WaitForMatch(player, onMatchFound, pollTime));
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