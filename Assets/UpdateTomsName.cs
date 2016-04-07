using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTomsName : MonoBehaviour
{
    [SerializeField]
    private Text tom;

    // Use this for initialization
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        tom.text = "Welcome back " + FitbitRestClient.Profile.user.fullName + "!";
    }
}
