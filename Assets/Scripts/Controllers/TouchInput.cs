using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Code from a tutorial on youtube by Sebastian Lague
public class TouchInput : MonoBehaviour
{
    public LayerMask TouchInputMask;

    private List<GameObject> TouchList = new List<GameObject>();
    private GameObject[] TouchOld;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            TouchOld = new GameObject[TouchList.Count];
            TouchList.CopyTo(TouchOld);
            TouchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, TouchInputMask))
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    TouchList.Add(touchedObject);

                    if (touch.phase == TouchPhase.Began)
                    {
                        touchedObject.SendMessage("OnTouchStart", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        touchedObject.SendMessage("OnTouchEnded", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        touchedObject.SendMessage("OnTouchStationary", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            foreach (GameObject touchedGameObject in TouchOld)
            {
                if (!TouchList.Contains(touchedGameObject))
                {
                    touchedGameObject.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
#endif

        if (Input.touchCount > 0)
        {
            TouchOld = new GameObject[TouchList.Count];
            TouchList.CopyTo(TouchOld);
            TouchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, TouchInputMask))
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    TouchList.Add(touchedObject);

                    if (touch.phase == TouchPhase.Began)
                    {
                        touchedObject.SendMessage("OnTouchStart", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        touchedObject.SendMessage("OnTouchEnded", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        touchedObject.SendMessage("OnTouchStationary", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        touchedObject.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            foreach (GameObject touchedGameObject in TouchOld)
            {
                if (!TouchList.Contains(touchedGameObject))
                {
                    touchedGameObject.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        } 
    }
}
