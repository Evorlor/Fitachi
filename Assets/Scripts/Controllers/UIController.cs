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

    public List<GameObject> Buttons = new List<GameObject>();

    /// <summary>
    /// Loads the specified scene
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseFoodScreen()
    {
        DishButton.gameObject.SetActive(true);
        FoodPyramid.gameObject.SetActive(false);
        FoodMenu.gameObject.SetActive(false);

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
        Debug.Log("Test food cost is: " + foodCost);
    }

    public void DisplayFoodToPurchase(int ButtonAmount)
    {
        DestroyButtonsInList();
        Buttons.Clear();

        for (int i = 0; i < ButtonAmount; i++)
        {
            GameObject Button = Instantiate(FoodPanelButton);
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