using UnityEngine;
using System.Collections;

public class DateSetter : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {
        AdventureStats.SetNewDate();
    }
	
	// Update is called once per frame
	void Update () {
        AdventureStats.checkForNewDate();

    }

    void OnApplicationQuit()
    {
        AdventureStats.SaveResetTime();
    }
}
