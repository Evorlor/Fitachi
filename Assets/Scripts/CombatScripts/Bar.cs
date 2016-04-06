using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

    bool goingUp;
    [SerializeField]
    int maxX, minX;
    [SerializeField]
    float speed;
    float tempX;

    bool isMoving;

	// Use this for initialization
	void Start () {
        minX = -5;
        maxX = 5;
        isMoving = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x>maxX) {
            goingUp = false;

        }
        if (transform.position.x <minX) {
            goingUp = true;
        }
        if (isMoving) {
            MoveBar();
        }
	}

    void MoveBar() {
        if (goingUp) {
            tempX = transform.position.x + Time.deltaTime * speed;
            transform.position = new Vector2(tempX, transform.position.y);
        }
        if (!goingUp) {
            tempX = transform.position.x - Time.deltaTime * speed;
            transform.position = new Vector2(tempX, transform.position.y);
        }
    }

    public void StopBar() {
        isMoving = false;
    }
}
