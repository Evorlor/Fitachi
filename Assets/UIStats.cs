using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public Text stepsTxt;

    // Use this for initialization
    void Start()
    {
        stepsTxt.text = "Steps:\n" + FitbitRestClient.Instance.ActivitiesDaily.summary.steps;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
