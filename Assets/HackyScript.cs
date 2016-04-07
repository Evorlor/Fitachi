using UnityEngine;
using System.Collections;

public class HackyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        AdventureStats.Speed.Steps = 22;
    }
}
