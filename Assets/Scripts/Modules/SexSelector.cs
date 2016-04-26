using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SexSelector : MonoBehaviour
{
    public Image profilePic;
    public Sprite malePic;
    
	// Use this for initialization
	IEnumerator Start () {
        yield return FitbitRestClient.Instance.GetProfile();
	}

    private bool sexed = false;
    // Update is called once per frame
    void Update()
    {

        if (sexed)
        {
            return;
        }
        
        var gender = FitbitRestClient.Instance.Profile.user.gender;
        if(gender != null && gender != "")
        {
            Debug.Log(gender);
            sexed = true;
            if (gender == "MALE")
            {
                Debug.Log(gender);
                AdventureStats.gender = Sex.Male;
                profilePic.sprite = malePic;
            }
            else
            {
                AdventureStats.gender = Sex.Female;
            }
            AdventureStats.SetRecomendedCalories(20, AdventureStats.gender);
        }
    }
}
