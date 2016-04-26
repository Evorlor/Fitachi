using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SexSelector : MonoBehaviour
{
    public Image profilePic;
    public Sprite malePic;

<<<<<<< HEAD
    // Use this for initialization
    IEnumerator Start()
    {
        yield return FitbitRestClient.GetProfile();
    }
=======
	// Use this for initialization
	IEnumerator Start () {
        yield return FitbitRestClient.Instance.GetProfile();
	}
>>>>>>> 922b5fb85120fddb15b53be299d4a33fa17a5647

    private bool sexed = false;
    // Update is called once per frame
    void Update()
    {

        if (sexed)
        {
            return;
        }

<<<<<<< HEAD
        var gender = FitbitRestClient.Profile.user.gender;
        if (gender != null)
=======
        var gender = FitbitRestClient.Instance.Profile.user.gender;
        if(gender != null)
>>>>>>> 922b5fb85120fddb15b53be299d4a33fa17a5647
        {
            sexed = true;
            if (gender == "MALE")
            {
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
