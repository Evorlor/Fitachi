using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    [SerializeField]
    int foodCost;

    [SerializeField]
    FoodType type;

    [SerializeField]
    int value;

    [SerializeField]
    int calories;

    public void Eat() {

        AdventureStats.Feed(type, calories, value);   

    }


}

public enum FoodType{

    Dairy,
    Protien,
    Vegetable,
    Fruit,
    Grain,
    Sweets

}
