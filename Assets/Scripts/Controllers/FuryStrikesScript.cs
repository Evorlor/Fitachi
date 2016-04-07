using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class FuryStrikesScript : MonoBehaviour
{
    public float AttackTimeLimit;
    public static int AttackAmount = 0;

    public GameObject AttackButtonPositions;
    public GameObject AttackButton;

    private GameObject[] ButtonList;

    // Use this for initialization
    void Start()
    {
        GameObject AttackPositions = Instantiate(AttackButtonPositions);

        AttackPositions.transform.parent = gameObject.transform;
        AttackPositions.transform.localScale = new Vector3(1, 1, 1);
        ButtonList = new GameObject[AttackButtonPositions.transform.childCount];

        for (int i = 0; i < AttackButtonPositions.transform.childCount; i++)
        {
            Vector3 newPosition = AttackPositions.transform.GetChild(i).transform.position;
            GameObject newAttackButton = (GameObject)Instantiate(AttackButton, newPosition, AttackButton.transform.rotation);

            ButtonList[i] = newAttackButton;
            newAttackButton.transform.SetParent(transform);
            newAttackButton.transform.localScale = new Vector3(1, 1, 1);

            if (i == 0)
            {
                newAttackButton.GetComponent<Image>().enabled = true;
                newAttackButton.GetComponent<Button>().enabled = true;
            }
        }

        StartCoroutine("timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackAmount == 1)
        {
            SetButtons(0, 2);
        }
        if (AttackAmount == 3)
        {
            SetButtons(2, 6);
        }
    }

    private IEnumerator timer()
    {
        float currentTime = 0;
        float timeLimit = 3.0f;

        while (currentTime < timeLimit)
        {
            currentTime += Time.deltaTime;

            yield return null;
        }

        SceneManager.UnloadScene(8);
        //SceneManager.LoadScene("Combat");
    }

    private void SetButtons(int ButtonsToDisable, int ButtonsToEnable)
    {
        for (int i = 0; i < ButtonList.Length; i++)
        {
            if (i <= ButtonsToDisable)
            {
                ButtonList[i].GetComponent<Image>().enabled = false;
                ButtonList[i].GetComponent<Button>().enabled = false;
            }
            if (i > ButtonsToDisable && i <= ButtonsToEnable)
            {
                ButtonList[i].GetComponent<Image>().enabled = true;
                ButtonList[i].GetComponent<Button>().enabled = true;
            }
        }
    }
}
