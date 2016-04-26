using UnityEngine;
using System.Collections;

public class FuryStrikeButton : MonoBehaviour
{
    public GameObject FuryStrikesObject;

    void Awake()
    {
        FuryStrikesObject = GameObject.FindWithTag("ButtonParentReference");
    }

    // Use this for initialization
    void Start()
    {

    }

    public void OnClickFuryStrike()
    {
        FuryStrikesObject.GetComponent<FuryStrikesScript>().AttackAmount++;
        gameObject.SetActive(false);
    }
}
