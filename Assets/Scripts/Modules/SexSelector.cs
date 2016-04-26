using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SexSelector : MonoBehaviour
{
    public Image profilePic;
    public Sprite malePic;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return FitbitRestClient.GetProfile();
    }

    private bool sexed = false;
    // Update is called once per frame
    void Update()
    {

        if (sexed)
        {
            return;
        }

        var gender = FitbitRestClient.Profile.user.gender;
        if (gender != null)
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
