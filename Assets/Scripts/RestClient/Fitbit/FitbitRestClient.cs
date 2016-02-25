using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentLogin
{
	private const string TokenKey = "Token";
	private const string UserIDKey = "UserID";

	public static void SaveData(string token, int userID)
	{
		PlayerPrefs.SetString(TokenKey, token);
		PlayerPrefs.SetInt(UserIDKey, userID);
	}

	public static Dictionary<string, object> LoadData()
	{
		var data = new Dictionary<string, object>();
		string token = PlayerPrefs.GetString(TokenKey);
		int userId = PlayerPrefs.GetInt(UserIDKey);
		data.Add(TokenKey, token);
		data.Add(UserIDKey, userId);
		return data;
	}
}


public class FitbitRestClient : MonoBehaviour {

	/*
	http://www.fiea.ucf.edu/#scope=nutrition+weight+location+social+heartrate+settings+sleep+activity+profile&user_id=4885CL&token_type=Bearer&expires_in=604800&access_token=eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE0NTcwMjg4MzYsInNjb3BlcyI6Indwcm8gd2xvYyB3bnV0IHdzbGUgd3NldCB3aHIgd3dlaSB3YWN0IHdzb2MiLCJzdWIiOiI0ODg1Q0wiLCJhdWQiOiIyMjdGV1QiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJpYXQiOjE0NTY0MjQwMzZ9.JJYII-KGWk5Labva3PSCOnOcR-Xf4LwJPyg9ouHSH50
	*/

	private static string CLIENT_ID = "227FWT";
	private static string LOG_URL = "https://www.fitbit.com/oauth2/authorize";
	private static string TOKEN_URL = "https://api.fitbit.com/oauth2/token";

	private static string USER_ID = "4885CL";
	private static string ACCESS_TOKEN = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE0NTcwMjg4MzYsInNjb3BlcyI6Indwcm8gd2xvYyB3bnV0IHdzbGUgd3NldCB3aHIgd3dlaSB3YWN0IHdzb2MiLCJzdWIiOiI0ODg1Q0wiLCJhdWQiOiIyMjdGV1QiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJpYXQiOjE0NTY0MjQwMzZ9.JJYII-KGWk5Labva3PSCOnOcR-Xf4LwJPyg9ouHSH50";

	private static string testurl = "https://www.fitbit.com/oauth2/authorize?response_type=token&client_id="+ CLIENT_ID+ "&redirect_uri=fitachi%3A%2F%2Fcb&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=604800";
	private static string testurl2 = "https://www.fitbit.com/oauth2/authorize?response_type=token&client_id=" + CLIENT_ID + "&redirect_uri=http%3A%2F%2Fwww.fiea.ucf.edu&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=604800";

	// Use this for initialization
	IEnumerator Start () {
		//HTTPRestHelper<FitbitRestClient> request = new HTTPRestHelper<FitbitRestClient>("http://www.fiea.ucf.edu/", RestMethod.GET);
		//request.ExecAysnc(test);


		//WWW www = new WWW("http://www.fiea.ucf.edu/");
		//StartCoroutine(WaitForRequest(www));
		string url = testurl;
        try
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}
		catch
		{
			url = testurl2;
        }

		//Application.OpenURL(url);

		// SEE here
		var headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + ACCESS_TOKEN);
        var www = new WWW("https://api.fitbit.com/1/user/"+ USER_ID + "/profile.json", null, headers);

		yield return www;

		Debug.Log(www.text);
    }

	void test<FitbitRestClient>(uint httpCode, FitbitRestClient data)
	{
		Debug.Log(data);
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
	}

	// Update is called once per frame
	void Update () {

		try {
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

			if (activity.Call<bool>("IsLogin"))
			{
				Debug.Log(activity.Call<string>("GetToken"));
				Debug.Log(activity.Call<string>("GetUserId"));
				Debug.Log(activity.Call<string>("GetExpires"));
			}
		}
		catch
		{

		}
	}

	public void Login()
	{

	}
}
