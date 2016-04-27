using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NutritionSceneScript : MonoBehaviour {
    public GameObject DisplayPanel;

    public List<GameObject> DairyList = new List<GameObject>();
    public List<GameObject> ProteinList = new List<GameObject>();
    public List<GameObject> GrainList = new List<GameObject>();
    public List<GameObject> VegetableList = new List<GameObject>();
    public List<GameObject> FruitList = new List<GameObject>();

    private List<GameObject> Buttons = new List<GameObject>();

    public Image DairySlider;
    public Image ProteinSlider;
    public Image GrainSlider;
    public Image VegetableSlider;
    public Image FruitSlider;

    private static int activeFood = -2;

    void Awake() {
        //DairySlider = GameObject.FindGameObjectWithTag("DairySlider").GetComponent<Image>();
        //ProteinSlider = GameObject.FindGameObjectWithTag("ProteinSlider").GetComponent<Image>();
        //GrainSlider = GameObject.FindGameObjectWithTag("GrainSlider").GetComponent<Image>();
        //VegetableSlider = GameObject.FindGameObjectWithTag("VegetableSlider").GetComponent<Image>();
        //FruitSlider = GameObject.FindGameObjectWithTag("FruitSlider").GetComponent<Image>();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void BuyFood(Food foodItem) {
        if (AdventureStats.Gold<foodItem.foodCost) {
            Debug.Log("It costs more money than you have.");
        }
        AdventureStats.Gold -= foodItem.foodCost;
        AdventureStats.Gold = Mathf.Max(0, AdventureStats.Gold);

        AdventureStats.Feed(foodItem);
        switch (foodItem.type) {
            case FoodType.Dairy:
                //AdventureStats.Dairy += foodItem.foodCost;
                GameObject.FindGameObjectWithTag("DairySlider").GetComponent<Image>().fillAmount += 0.2f;
                break;
            case FoodType.Protien:
                //AdventureStats.Protein += foodItem.foodCost;
                GameObject.FindGameObjectWithTag("ProteinSlider").GetComponent<Image>().fillAmount += 0.2f;
                break;
            case FoodType.Grain:
                //AdventureStats.Grain += foodItem.foodCost;
                GameObject.FindGameObjectWithTag("GrainSlider").GetComponent<Image>().fillAmount += 0.2f;
                break;
            case FoodType.Vegetable:
                //AdventureStats.Vegetable += foodItem.foodCost;
                GameObject.FindGameObjectWithTag("VegetableSlider").GetComponent<Image>().fillAmount += 0.2f;
                break;
            case FoodType.Fruit:
                //AdventureStats.Fruit += foodItem.foodCost;
                GameObject.FindGameObjectWithTag("FruitSlider").GetComponent<Image>().fillAmount += 0.2f;
                break;
            default:
                break;
        }
    }

    // Applies calories to the player
    public void ApplyCalories(int Calories) {
        AdventureStats.calories += Calories;
    }

    public void DisplayFoodToPurchase(int FoodCategory) {
        DestroyButtonsInList();
        Buttons.Clear();

        activeFood = FoodCategory - 1;
        switch (FoodCategory) {
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
            default:
                break;
        }
    }

    private void SpawnFoodItems(List<GameObject> FoodList) {
        for (int i = 0; i < FoodList.Count; i++) {
            GameObject Button = Instantiate(FoodList[i]);
            Button.transform.SetParent(DisplayPanel.transform, false);
            Buttons.Add(Button);
        }
    }

    public void CloseFoodScreen() {
        DestroyButtonsInList();
        Buttons.Clear();
    }

    /// <summary>
    /// Destroys the instantiated buttons in the food menu
    /// </summary>
    void DestroyButtonsInList() {
        foreach (GameObject button in Buttons) {
            Destroy(button);
        }
    }
}
