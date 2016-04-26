using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DontKillMe : MonoBehaviour {
    public Text goldPurseText;

    void Awake()
    {
    //    DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        goldPurseText.text = AdventureStats.Gold.ToString();
	}
}