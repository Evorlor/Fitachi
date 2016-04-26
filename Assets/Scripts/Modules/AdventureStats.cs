using System.Collections.Generic;
using System;
using UnityEngine;

public static class AdventureStats {
    public static int Gold;

    public static DateTime resetTime;

    private static Queue<int> fourDaysOfSteps = new Queue<int>(1);

    private static int[] AdventurerNutrition = new int[5];

    private static int[] RecomendedNutrition = new int[5] { 3, 6, 3, 4, 7};

	public static int Dairy;

	public static int Protein;

	public static int Grain;

    public static int Vegetable;

    public static int Fruit;

    public static int averageStepsOverFourDays;

    public static int currentStepCount;

    public static int Sweets;

    public static int calories;

    public static void Feed(FoodType food, int foodCaloires, int foodValue) {
        AdventurerNutrition[(int)food] = foodValue;
        calories += foodCaloires;
    }


    public static void UpdateSteps(int newStepsCount) {
        if (fourDaysOfSteps.Count==4) {
            fourDaysOfSteps.Dequeue();
        }
        fourDaysOfSteps.Enqueue(newStepsCount);
        currentStepCount = newStepsCount;
        int averageHolder = 0;
        foreach (int steps in fourDaysOfSteps) {
            averageHolder += steps;
        }
        averageStepsOverFourDays = averageHolder / 4;

    }

    public static void Reset() {
        for (int i = 0; i < AdventurerNutrition.Length; i++) {
            AdventurerNutrition[i] = 0;
        }
        calories = 0;
    }

    public static int GetStat(FoodType type) {
        return AdventurerNutrition[(int)type];
    }

    public static float GetCombatStrength() {
        float combatStrength = 0;
        float totalOverage = 0;
        for (int i = 0; i < AdventurerNutrition.Length; i++) {
            float value = AdventurerNutrition[i] / RecomendedNutrition[i];
            combatStrength += (value < 0) ? 0 : (value > 1) ? 1 : value;
            if (AdventurerNutrition[i] - RecomendedNutrition[i] > 0) {
                totalOverage++;
            }
        }
        totalOverage = totalOverage * .05f;

        combatStrength *= (1-totalOverage);

        return combatStrength;
    }
    public static void checkForNewDate(){
        if (DateTime.Now>resetTime) {
            Reset();
            resetTime.AddDays(1);
        }
    }
    public static void SetNewDate(){
        if (!PlayerPrefs.HasKey("ResetYear")) {
            resetTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 4, 0, 0);
            PlayerPrefs.SetInt("ResetYear", DateTime.Now.Year);
            PlayerPrefs.SetInt("ResetMonth", DateTime.Now.Month);
            PlayerPrefs.SetInt("ResetDay", DateTime.Now.Day);
        }
        if (PlayerPrefs.HasKey("ResetYear")) {
            resetTime = new DateTime(PlayerPrefs.GetInt("ResetYear"), PlayerPrefs.GetInt("ResetMonth"), PlayerPrefs.GetInt("ResetDay"), 4, 0, 0);
        }
    }

}
public enum Sex {
    Male,
    Female
}