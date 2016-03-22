package edu.ucf.fiea.gamelab.fitachi;

import com.unity3d.player.UnityPlayerActivity;

import android.os.Bundle;
import android.util.Log;

import java.net.URI;
import java.util.HashMap;

public class Fitachi extends UnityPlayerActivity {

    static String token = null;
	static String user_id = null;
	static String expires_in = null;
	static boolean is_login = false;

    static String LogTag = "Fitachi";

  @Override
  protected void onNewIntent(Intent intent) {
      super.onNewIntent(intent);
      setIntent(intent);
  }

  @Override protected void onStart ()
  {
      Log.i(LogTag, "onStart.");
      parseToken();
      super.onStart();
  }

	void parseToken() {
		android.net.Uri uri = getIntent().getData();
		if (uri != null){
			Log.i(LogTag, "url: " + uri.toString());
			HashMap<String, String> hashMap = parseUri(uri.toString());

			token = hashMap.get("access_token");
			user_id = hashMap.get("user_id");
			expires_in = hashMap.get("expires_in");

			if (user_id != null)
			{
				Log.i(LogTag, "user_id: " + user_id);
			}

			if (token != null)
			{
				Log.i(LogTag, "token: " + token);
			}

			if (expires_in != null)
			{
				Log.i(LogTag, "expires_in: " + expires_in);
			}

			is_login = true;
		}else{
			Log.e(LogTag, "URL is null");
		}
	}

	public String GetToken() {
		return token;
	}

	public String GetUserId() {
		return user_id;
	}

	public String GetExpires() {
		return expires_in;
	}

	public boolean IsLogin() {
		return is_login;
	}

	private HashMap<String, String> parseUri(String url) {
		HashMap<String, String> queries = new HashMap<>();
		url = url.trim();
		String[] nodes = url.split("&");
		for (int i = 1; i < nodes.length; i++) {
			String[] pair = nodes[i].split("=");
			queries.put(pair[0], pair[1]);
		}
		return queries;
	}
}
