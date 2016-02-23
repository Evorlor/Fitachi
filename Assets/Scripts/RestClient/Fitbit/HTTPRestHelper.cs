using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;

public delegate void RestHelperExecCallBack<DataType>(uint httpCode, DataType data);

public enum RestMethod
{
	GET, POST, DELETE, PUT
}

public class HTTPRestHelper<DataType>  {

	private string mURL;
	private string mUrlParm = "";
	private string mPostData = "";
	private string mResponseData = "";
	private RestMethod mMethod;
	private RestHelperExecCallBack<DataType> mCallback;
	private HttpWebRequest mRequest;

	public HTTPRestHelper(string url, RestMethod method)
	{
		mURL = url;
		mMethod = method;
    }

	public void ExecAysnc(RestHelperExecCallBack<DataType> callback)
	{
		mCallback = callback;
		mRequest = (HttpWebRequest)HttpWebRequest.Create(mURL + mUrlParm);

		byte[] byteArray = Encoding.UTF8.GetBytes(mPostData);
		// TODO: Async version for sending
		mRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"; // Fake it, pretend we are the browser
        mRequest.ContentType = "application/x-www-form-urlencoded";
		mRequest.ContentLength = byteArray.Length;

		// Write POST data, TODO: make this async
		if (mRequest.Method == "Post") {
			Stream dataStream = mRequest.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();
		}

		StreamReader reader = new StreamReader(mRequest.GetResponse().GetResponseStream(), Encoding.UTF8);
		string responseString = reader.ReadToEnd();
		//mRequest.BeginGetResponse(new System.AsyncCallback(RespCallback), this);
	}

	private void RespCallback(System.IAsyncResult ar)
	{
		HttpWebResponse mResponse = this.mRequest.EndGetResponse(ar) as HttpWebResponse;
        StreamReader reader = new StreamReader(mResponse.GetResponseStream(), Encoding.UTF8);
		string responseString = reader.ReadToEnd();

		DataType data = default(DataType);
        try
		{
			data = JsonUtility.FromJson<DataType>(mResponseData);
			Debug.Log(data);
        } catch(System.Exception e)
		{
			Debug.Log(e.StackTrace);
		}

		mCallback((uint)mResponse.StatusCode, data);
	}
}
