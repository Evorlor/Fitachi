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

        switch (type) {
            case (FoodType.Dairy):
                AdventureStats.Dairy += value;
                break;
            case (FoodType.Protien):
                AdventureStats.Protein += value;
                break;
            case (FoodType.Vegetable):
                AdventureStats.Vegetable += value;
                break;
            case (FoodType.Fruit):
                AdventureStats.Fruit += value;
                break;
            case (FoodType.Grain):
                AdventureStats.Grain += value;
                break;
            case (FoodType.Sweets):
                AdventureStats.Sweets += value;
                break;
        }    

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
