using UnityEngine;

public class Serializer
{
    public static string SerializePlayer()
    {
        var playerData = new PlayerData();
        playerData.Dairy = AdventureStats.Dairy;
        playerData.Protein = AdventureStats.Protein;
        playerData.Grain = AdventureStats.Grain;
        playerData.Vegetable = AdventureStats.Vegetable;
        playerData.Fruit = AdventureStats.Fruit;
        playerData.Sweets = AdventureStats.Sweets;
        string playerJson = JsonUtility.ToJson(playerData);
        return playerJson;
    }
}
