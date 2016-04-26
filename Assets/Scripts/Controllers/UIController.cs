using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public GameObject FoodPanelButton;
    public GameObject DisplayPanel;
    public GameObject DishButton;
    public GameObject FoodPyramid;
    public GameObject FoodMenu;

    public List<GameObject> DairyList = new List<GameObject>();
    public List<GameObject> ProteinList = new List<GameObject>();
    public List<GameObject> GrainList = new List<GameObject>();
    public List<GameObject> VegetableList = new List<GameObject>();
    public List<GameObject> FruitList = new List<GameObject>();

    private List<GameObject> Buttons = new List<GameObject>();

    void Awake()
    {
        Debug.Log("WHYYY");
    }

    /// <summary>
    /// Loads the specified scene
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayRandomMiniGame()
    {
        var sceneNames = new string[3] { SceneNames.BigHit, SceneNames.RangedAttack, SceneNames.FuryStrikes };
        SceneManager.LoadScene(sceneNames[Random.Range(0, sceneNames.Length)]);
    }

    public void CloseFoodScreen()
    {
        //DishButton.gameObject.SetActive(true);
        //FoodPyramid.gameObject.SetActive(false);
        //FoodMenu.gameObject.SetActive(false);

        DestroyButtonsInList();
        Buttons.Clear();
    }

    public void ShowFoodPyramid()
    {
        DishButton.gameObject.SetActive(false);
        FoodPyramid.gameObject.SetActive(true);
        FoodMenu.gameObject.SetActive(true);
    }
    //private string activeFood = "sam";
    private int fuckthis = -2;

    public void BuyFood(int foodCost)
    {
        AdventureStats.Gold -= foodCost;
        AdventureStats.Gold = Mathf.Max(0, AdventureStats.Gold);
        //AdventureStats.Endurance.HeartRate += 3;
        //AdventureStats.Nutrition.Hunger++;
        //AdventureStats.Rest.Sleep++;
        //AdventureStats.Speed.Steps++;
        switch (fuckthis)
        {
            case 0:
                AdventureStats.Dairy += foodCost;
                break;
            case 1:
                AdventureStats.Protein += foodCost;
                break;
            case 2:
                AdventureStats.Grain += foodCost;
                break;
            case 3:
                AdventureStats.Vegetable += foodCost;
                break;
            case 4:
                AdventureStats.Fruit += foodCost;
                break;
            default:
                Debug.Log("GWE" + fuckthis);
                break;
        }
    }

    public void DisplayFoodToPurchase(int FoodCategory)
    {
        DestroyButtonsInList();
        Buttons.Clear();

        fuckthis = FoodCategory - 1;
        switch (FoodCategory)
        {
            case 1:
                Debug.Log("ok");
                AdventureStats.Dairy += 1;
                SpawnFoodItems(DairyList);
                break;
            case 2:
                AdventureStats.Protein += 1;
                SpawnFoodItems(ProteinList);
                break;
            case 3:
                AdventureStats.Grain += 1;
                SpawnFoodItems(GrainList);
                break;
            case 4:
                AdventureStats.Vegetable += 1;
                SpawnFoodItems(VegetableList);
                break;
            case 5:
                AdventureStats.Fruit += 1;
                SpawnFoodItems(FruitList);
                break;
            default:
                Debug.Log("fuck");
                break;
        }
    }

    private void SpawnFoodItems(List<GameObject> FoodList)
    {
        for (int i = 0; i < FoodList.Count; i++)
        {
            GameObject Button = Instantiate(FoodList[i]);
            Button.transform.SetParent(DisplayPanel.transform, false);
            Buttons.Add(Button);
        }
    }

    /// <summary>
    /// Destroys the instantiated buttons in the food menu
    /// </summary>
    void DestroyButtonsInList()
    {
        foreach (GameObject button in Buttons)
        {
            Destroy(button);
        }
    }
}