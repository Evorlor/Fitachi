using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public float LifeTime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D collector) {
        if (collector.tag=="Player") {
            Destroy(gameObject);
        }

    }
    void OnMouseEnter() {
        Destroy(gameObject);
    }

}
