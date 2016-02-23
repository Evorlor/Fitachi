using UnityEngine;
using System.Collections;

public class FitbitRestClient : MonoBehaviour {

	/*
	*Application.OpenURL
	*https://github.com/gree/unity-webview
	*https://community.fitbit.com/t5/Web-API/Connecting-FitBit-through-Unity-using-current-Plug-Ins-WebView/td-p/1051844
	* https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=227FWT&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight
	*/

	private static string CLIENT_ID = "227FWT";
	private static string SECRET = "474b1787965a0cc8e648660f40f3f1ce";
	private static string LOG_URL = "https://www.fitbit.com/oauth2/authorize";
	private static string TOKEN_URL = "https://api.fitbit.com/oauth2/token";

	// Use this for initialization
	void Start () {
		//HTTPRestHelper<FitbitRestClient> request = new HTTPRestHelper<FitbitRestClient>("http://www.fiea.ucf.edu/", RestMethod.GET);
		//request.ExecAysnc(test);


		//WWW www = new WWW("http://www.fiea.ucf.edu/");
		//StartCoroutine(WaitForRequest(www));
		Application.OpenURL("http://www.fiea.ucf.edu/");
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
	
	}

	public void Login()
	{

	}
}
