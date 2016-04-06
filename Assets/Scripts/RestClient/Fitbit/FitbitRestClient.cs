using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FitbitRestClient : ManagerBehaviour<FitbitRestClient>
{

	public string DEBUG_USERID = "";
	public string DEBUG_TOKEN = "";

	private static string CLIENT_ID = "227FWT";
	private static string EXPIRED_TIME = "2592000";
	private static string LOGIN_API = "https://www.fitbit.com/oauth2/authorize";

	private static string TokenKey = "Token";
	private static string UserIDKey = "UserID";

	private static string ANDROID_URL = LOGIN_API + "?response_type=token&client_id="+ CLIENT_ID+ "&redirect_uri=fitachi%3A%2F%2Fcb&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=" + EXPIRED_TIME;
	private static string WIN_URL = LOGIN_API + "?response_type=token&client_id=" + CLIENT_ID + "&redirect_uri=http%3A%2F%2Fwww.fiea.ucf.edu&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=" + EXPIRED_TIME;

	private string mAccessToke = "";
	private string mUserId = "";

	private bool mIsLogin = false;
	public static Fitbit.User.Profile Profile;
	public static Fitbit.Activity.Activities Activities;
	public static Fitbit.ActivitiesDaily.ActivitiesDaily ActivitiesDaily;

	public static bool IsLogin()
	{
		return Instance.mIsLogin;
	}

	public static string GetUserId()
	{
		return Instance.mUserId;
	}

	public static void SaveData(string token, string userID)
	{
		Debug.Log("Save.. " + token + " " + userID);
		PlayerPrefs.SetString(TokenKey, token);
		PlayerPrefs.SetString(UserIDKey, userID);
		PlayerPrefs.Save();
    }

	public IEnumerator LoadData()
	{
		mAccessToke = PlayerPrefs.GetString(TokenKey);
		mUserId = PlayerPrefs.GetString(UserIDKey);

		if (mAccessToke == "" || mUserId == "")
		{
			Debug.Log("Need to login");
			mIsLogin = false;
		}
		else
		{
			Debug.Log("Got catched id and token" + mUserId + " " + mAccessToke);

			// Validate the token, a very naive way...
			var headers = new Dictionary<string, string>();
			headers.Add("Authorization", "Bearer " + mAccessToke);
			var www = new WWW("https://api.fitbit.com/1/user/" + mUserId + "/profile.json", null, headers);

			yield return www;
			if (www.text.Contains("errors"))
			{
				Debug.Log(www.text);
				Debug.Log("Error token: " + mUserId + " " + mAccessToke);
				mIsLogin = false;
            } else
			{
				Profile = JsonUtility.FromJson<Fitbit.User.Profile>(www.text);
				mIsLogin = true;
				GetAll();
			}
		}
	}

	// Use this for initialization
	IEnumerator Start () {
		// try get access toke from preference
		yield return LoadData();

		if (!mIsLogin) {

			string url = ANDROID_URL;
			try
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				Debug.Log("Login...");
				Application.OpenURL(url);
			}
			catch
			{
				url = WIN_URL;
				if (DEBUG_USERID != string.Empty && DEBUG_TOKEN != string.Empty) {
					mAccessToke = DEBUG_TOKEN;
					mUserId = DEBUG_USERID;

					SaveData(mAccessToke, mUserId);

					mIsLogin = true;
					GetAll();
				} else
				{
					Application.OpenURL(url);
				}
			}
		}
    }

	public static Coroutine GetProfile()
	{
		if (Instance == null) return null;
		return Instance.StartCoroutine(Instance.GetProfileInternal());
	}

	IEnumerator GetProfileInternal()
	{
        var headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + mAccessToke);
		var www = new WWW("https://api.fitbit.com/1/user/" + mUserId + "/profile.json", null, headers);

		while (!www.isDone)
		{
			yield return null;
		}
		Debug.Log("DEBUG: " + www.text);
		Profile = JsonUtility.FromJson<Fitbit.User.Profile>(www.text);
	}

	public static Coroutine GetActiviesLifeTimeState()
	{
		if (Instance == null) return null;
		return Instance.StartCoroutine(Instance.GetActiviesLifeTimeStateInternal());
	}

	IEnumerator GetActiviesLifeTimeStateInternal()
	{
		var headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + mAccessToke);
		var www = new WWW("https://api.fitbit.com/1/user/" + mUserId + "/activities.json", null, headers);

		while (!www.isDone)
		{
			yield return null;
		}
		Debug.Log("DEBUG: " + www.text);
		Activities = JsonUtility.FromJson<Fitbit.Activity.Activities>(www.text);
	}

	public static Coroutine GetActiviesDailyState(System.DateTime date)
	{
		if (Instance == null) return null;
		return Instance.StartCoroutine(Instance.GetActiviesDailyStateInternal(date));
	}

	IEnumerator GetActiviesDailyStateInternal(System.DateTime date)
	{
		var headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + mAccessToke);
		var www = new WWW("https://api.fitbit.com/1/user/" + mUserId + "/activities/date/" + date.ToString("yyyy-MM-dd")+ ".json", null, headers);

		while (!www.isDone)
		{
			yield return null;
		}
		Debug.Log("DEBUG: " + www.text);
		ActivitiesDaily = JsonUtility.FromJson<Fitbit.ActivitiesDaily.ActivitiesDaily>(www.text);
	}

	// Update is called once per frame
	void Update () {

		if (!mIsLogin)
		{
			try
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                if (activity.Call<bool>("IsLogin"))
				{
					mAccessToke = activity.Call<string>("GetToken");
					mUserId = activity.Call<string>("GetUserId");
					// TODO Debug.Log(activity.Call<string>("GetExpires"));

					SaveData(mAccessToke, mUserId);

					mIsLogin = true;
					GetAll();
                }
			}
			catch
			{

			}
		}
	}

	private void GetAll()
	{
		GetProfile();
		GetActiviesLifeTimeState();
		GetActiviesDailyState(System.DateTime.Now);
	}
}
