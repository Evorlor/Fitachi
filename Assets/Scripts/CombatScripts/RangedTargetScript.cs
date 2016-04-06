using UnityEngine;
using System.Collections;

public class RangedTargetScript : MonoBehaviour {

    GameObject[] targetZones;

    float startMovementTime;

    [SerializeField]
    float speed;

    float journeyLength;
    [SerializeField]
    Vector2 startPos, endPos;
    [SerializeField]
    bool isMoving;

    float score;

    // Use this for initialization
    void Start() {
        isMoving = false;
        startPos = transform.position;
        journeyLength = 3;
        score = 0;
    }

    // Update is called once per frame
    void Update() {
        Destroy(gameObject, 7);
        if (isMoving) {
            Move();
        }
        else {
            StartMove();
        }

        TouchCheck();
    }

    void Move() {
        float distCovered = (Time.time - startMovementTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector2.Lerp(startPos, endPos, fracJourney);
        Vector2 pos = transform.position;
        if (pos == endPos) {
            isMoving = false;
        }
    }

    void StartMove() {
        startPos = transform.position;
        endPos = new Vector2(Random.Range(-5.5f, 5.6f), Random.Range(-2.5f, 2.5f));
        startMovementTime = Time.time;
        isMoving = true;
    }

    void TouchCheck() {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.x, ray.y), Vector2.zero, 100);
        if (hit.collider!=null) {
            if (hit.collider.name == "10pts") {
                score += .4f;
            }
            if (hit.collider.name == "5pts") {
                score += .2f;
            }
        }
    }
    void OnDestroy() {
        Debug.Log("Final Score: "+ score);
    }
}
