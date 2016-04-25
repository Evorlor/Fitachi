using UnityEngine;
using System.Collections;

public class BarController : MonoBehaviour {

    [SerializeField]
    Bar[] bars;

    int activeBar;

	// Use this for initialization
	void Start () {
        activeBar = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            bars[activeBar].StopBar();
            activeBar++;
        }
        if (activeBar>=bars.Length) {
            Debug.Log("Final Multiplier: " + getdamageModifier());
        }
	}
    public float getdamageModifier() {
        float totalXMiss = 0;
        for (int i = 0; i<bars.Length;i++) {
            totalXMiss += Mathf.Abs(bars[i].gameObject.transform.position.x);
        }
        if (totalXMiss < 2.1) {
            return 1;
        }
        else if (totalXMiss < 4.5) {
            return .8f;
        }
        else if (totalXMiss < 6.6) {
            return .75f;
        }
        else {
            return .5f;
        }
    }
}
