using UnityEngine;
using System.Collections;

public class FuryStrikeButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    public void OnClickFuryStrike()
    {
        FuryStrikesScript.AttackAmount++;
        gameObject.SetActive(false);
    }
}
