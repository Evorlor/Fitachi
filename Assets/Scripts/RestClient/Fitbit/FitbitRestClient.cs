using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FitbitRestClient : MonoBehaviour {

	private FitbitRestClient mInstance = null;

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

	public Fitbit.User User;

	public static void SaveData(string token, string userID)
	{
		PlayerPrefs.SetString(TokenKey, token);
		PlayerPrefs.SetString(UserIDKey, userID);
	}

	public void LoadData()
	{
		string mAccessToke = PlayerPrefs.GetString(TokenKey);
		string mUserId = PlayerPrefs.GetString(UserIDKey);

		if (mAccessToke == "" || mUserId == "")
		{
			mIsLogin = false;
		}
		else
		{
			mIsLogin = true;
		}
	}

	// Use this for initialization
	void Start () {

		mInstance = this;

		// try get access toke from preference
		LoadData();

		if (!mIsLogin) {

			string url = ANDROID_URL;
			try
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}
			catch
			{
				url = WIN_URL;
			}

			Application.OpenURL(url);
		}
    }

	Coroutine GetUser()
	{
		return StartCoroutine(GetUserInternal());
	}

	IEnumerator GetUserInternal()
	{
		var headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + mAccessToke);
		var www = new WWW("https://api.fitbit.com/1/user/" + mUserId + "/profile.json", null, headers);

		while (!www.isDone)
		{
			yield return null;
		}
		Debug.Log(www.text);
		User = JsonUtility.FromJson<Fitbit.User>(www.text);
		Debug.Log("name " + User.fullName);
	}

	// Update is called once per frame
	void Update () {

		if (!mIsLogin)
		{
			try
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				Debug.Log("Done");
                if (activity.Call<bool>("IsLogin"))
				{
					mAccessToke = activity.Call<string>("GetToken");
					mUserId = activity.Call<string>("GetUserId");
					// TODO Debug.Log(activity.Call<string>("GetExpires"));

					SaveData(mAccessToke, mUserId);
					mIsLogin = true;
				}
			}
			catch
			{

			}
		} else
		{
			//GetUser();
		}
	}
}
