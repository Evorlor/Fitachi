using System.Collections.Generic;
using System;
using UnityEngine;

public static class AdventureStats {
    public static int Gold;

    public static float overagePenalty = .05f;

    public static int calorieOveragePenaltyBreak = 50; 

    public static DateTime resetTime;

    private static Queue<int> fourDaysOfSteps = new Queue<int>(1);

    private static int[] AdventurerNutrition = new int[5];

    private static int[] RecomendedNutrition = new int[5] { 3, 6, 3, 4, 7 };

    public static int Dairy;

    public static int Protein;

    public static int Grain;

    public static int Vegetable;

    public static int Fruit;

    public static int averageStepsOverFourDays;

    public static int currentStepCount;

    public static int Sweets;

    public static int calories;

    public static Sex gender;
    public static int DailyRecomendedCalories;

    public static void SetRecomendedCalories(int age, Sex sex) {
        if (sex == Sex.Female) {
            if (age == 12) {
                DailyRecomendedCalories = 1800;
            }
            else if (age > 12 && age < 15) {
                DailyRecomendedCalories = 2000;
            }
            else if (age == 15) {
                DailyRecomendedCalories = 2200;
            }
            else if (age > 16 && age < 19) {
                DailyRecomendedCalories = 2400;
            }
            else if (age > 18 && age < 21) {
                DailyRecomendedCalories = 2600;
            }
            else {
                DailyRecomendedCalories = 2400;
            }
        }
        if (sex == Sex.Male) {
            if (age < 14) {
                DailyRecomendedCalories = 1600;
            }
            else if (age > 13 && age < 19) {
                DailyRecomendedCalories = 1800;
            }

            else {
                DailyRecomendedCalories = 2000;
            }

        }
    }

    public static void Feed(FoodType food, int foodCaloires, int foodValue) {
        AdventurerNutrition[(int)food] = foodValue;
        calories += foodCaloires;
    }


    public static void UpdateSteps(int newStepsCount) {
        if (fourDaysOfSteps.Count == 4) {
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
        float totalServingsOverage = 0;
        for (int i = 0; i < AdventurerNutrition.Length; i++) {
            float value = AdventurerNutrition[i] / RecomendedNutrition[i];
            combatStrength += (value < 0) ? 0 : (value > 1) ? 1 : value;
            if (AdventurerNutrition[i] - RecomendedNutrition[i] > 0) {
                totalServingsOverage++;
            }
        }
        totalServingsOverage = totalServingsOverage * overagePenalty;

        float calorieOverage = ((calories-DailyRecomendedCalories)/ calorieOveragePenaltyBreak) * overagePenalty;

        float totalOveragePenalty = calorieOverage + totalServingsOverage;

        combatStrength *= (1 - totalOveragePenalty);

        return combatStrength;
    }

    //update
    public static void checkForNewDate() {
        if (DateTime.Now > resetTime) {
            Reset();
            resetTime.AddDays(1);
        }
    }

    //call on launch
    public static void SetNewDate() {
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

    //call on close
    public static void SaveResetTime() {
        PlayerPrefs.SetInt("ResetYear", resetTime.Year);
        PlayerPrefs.SetInt("ResetMonth", resetTime.Month);
        PlayerPrefs.SetInt("ResetDay", resetTime.Day);
    }

}
public enum Sex {
    Male,
    Female
}