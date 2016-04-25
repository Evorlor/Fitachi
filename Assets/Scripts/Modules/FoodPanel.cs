using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FoodPanel : MonoBehaviour
{
    public Image FoodImage;
    public Text CaloricText;
    public Text CostText;

    public Sprite FoodSprite;
    public int CaloriesAmount;
    public int CostAmount;


    void Awake()
    {
        FoodImage.sprite = FoodSprite;
        CaloricText.text = CaloriesAmount.ToString();
        CostText.text = CostAmount.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
