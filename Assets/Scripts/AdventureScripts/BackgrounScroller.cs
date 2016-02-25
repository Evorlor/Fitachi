using UnityEngine;
using System.Collections;

public class BackgrounScroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, (tileSizeZ*.60f));
        transform.position = startPosition + Vector3.left * newPosition;

        
    }
}
