using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public Text stepsTxt;
    public Text caloriesTxt;

    // Use this for initialization
    void Start()
    {
        stepsTxt.text = "Steps: " + FitbitRestClient.Instance.ActivitiesDaily.summary.steps;
        caloriesTxt.text = "Calories Burnt: " + FitbitRestClient.Instance.ActivitiesDaily.summary.activityCalories;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
