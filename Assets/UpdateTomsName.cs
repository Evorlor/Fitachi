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
        if (FitbitRestClient.Instance.Profile.user == null)
        {
            return;
        }
        tom.text = FitbitRestClient.Instance.Profile.user.fullName.ToString();
    }
}
