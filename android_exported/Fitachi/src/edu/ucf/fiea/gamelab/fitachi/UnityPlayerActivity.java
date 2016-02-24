package edu.ucf.fiea.gamelab.fitachi;

import com.unity3d.player.*;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;

import java.io.File;
import java.net.URI;
import java.util.HashMap;

public class UnityPlayerActivity extends Activity
{
	static String token = null;
	static String user_id = null;
	static String expires_in = null;
	static boolean is_login = false;

	protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code
	static String LogTag = "Fitachi";
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

	// Setup activity layout
	@Override protected void onCreate (Bundle savedInstanceState)
	{
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		super.onCreate(savedInstanceState);

		getWindow().setFormat(PixelFormat.RGBX_8888); // <--- This makes xperia play happy

		mUnityPlayer = new UnityPlayer(this);
		setContentView(mUnityPlayer);
		mUnityPlayer.requestFocus();
	}

	// Quit Unity
	@Override protected void onDestroy ()
	{
		mUnityPlayer.quit();
		super.onDestroy();
	}

	// Pause Unity
	@Override protected void onPause()
	{
		super.onPause();
		mUnityPlayer.pause();
	}

	// Resume Unity
	@Override protected void onResume()
	{
		super.onResume();
		mUnityPlayer.resume();
	}

	// This ensures the layout will be correct.
	@Override public void onConfigurationChanged(Configuration newConfig)
	{
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}

	// Notify Unity of the focus change.
	@Override public void onWindowFocusChanged(boolean hasFocus)
	{
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
	}

	// For some reason the multiple keyevent type is not supported by the ndk.
	// Force event injection by overriding dispatchKeyEvent().
	@Override public boolean dispatchKeyEvent(KeyEvent event)
	{
		if (event.getAction() == KeyEvent.ACTION_MULTIPLE)
			return mUnityPlayer.injectEvent(event);
		return super.dispatchKeyEvent(event);
	}

	// Pass any events not handled by (unfocused) views straight to UnityPlayer
	@Override public boolean onKeyUp(int keyCode, KeyEvent event)     { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onKeyDown(int keyCode, KeyEvent event)   { return mUnityPlayer.injectEvent(event); }
	@Override public boolean onTouchEvent(MotionEvent event)          { return mUnityPlayer.injectEvent(event); }
	/*API12*/ public boolean onGenericMotionEvent(MotionEvent event)  { return mUnityPlayer.injectEvent(event); }
}
