using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatUpdate : MonoBehaviour {

	[SerializeField]
	private Text tom;
	[SerializeField]
	private Text littletom;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (FitbitRestClient.Instance.Activities.lifetime == null)
		{
			return;
		}
		tom.text = "Steps: "+ FitbitRestClient.Instance.Activities.lifetime.total.steps;
		littletom.text = "Calories: " + FitbitRestClient.Instance.Activities.lifetime.total.caloriesOut;
	}
}
