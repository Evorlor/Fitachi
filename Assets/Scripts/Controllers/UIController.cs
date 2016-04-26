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
    }

    /// <summary>
    /// Loads the specified scene
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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

    public void BuyFood(int foodCost)
    {
        AdventureStats.Gold -= 9;
        AdventureStats.Gold = Mathf.Max(0, AdventureStats.Gold);
        //AdventureStats.Endurance.HeartRate += 3;
        //AdventureStats.Nutrition.Hunger++;
        //AdventureStats.Rest.Sleep++;
        //AdventureStats.Speed.Steps++;
    }

    public void DisplayFoodToPurchase(int FoodCategory)
    {
        DestroyButtonsInList();
        Buttons.Clear();

        switch (FoodCategory)
        {
            case 1:
                SpawnFoodItems(DairyList);
                break;
            case 2:
                SpawnFoodItems(ProteinList);
                break;
            case 3:
                SpawnFoodItems(GrainList);
                break;
            case 4:
                SpawnFoodItems(VegetableList);
                break;
            case 5:
                SpawnFoodItems(FruitList);
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