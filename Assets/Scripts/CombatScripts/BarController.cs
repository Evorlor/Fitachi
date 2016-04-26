using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BarController : MonoBehaviour
{

    [SerializeField]
    Bar[] bars;

    int activeBar;

    private const int gameValue = 250;

    // Use this for initialization
    void Start()
    {
        activeBar = 0;
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        bars[0].gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bars[activeBar].StopBar();
            activeBar++;
            if (activeBar < bars.Length)
            {
                bars[activeBar].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (activeBar >= bars.Length)
        {
            Debug.Log("Final Multiplier: " + getdamageModifier());
            AdventureStats.Gold += (int)(getdamageModifier() * gameValue);
            SceneManager.LoadScene(SceneNames.MainMenu);
        }
    }
    public float getdamageModifier()
    {
        float totalXMiss = 0;
        for (int i = 0; i < bars.Length; i++)
        {
            totalXMiss += Mathf.Abs(bars[i].gameObject.transform.position.x);
        }
        if (totalXMiss < 3.8)
        {
            return 1;
        }
        else if (totalXMiss < 6)
        {
            return .8f;
        }
        else if (totalXMiss < 8)
        {
            return .75f;
        }
        else
        {
            return .5f;
        }
    }
}
