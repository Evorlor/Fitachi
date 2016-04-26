using UnityEngine;

public class Serializer
{
    public static string SerializePlayer()
    {
        var playerData = new PlayerData();
        playerData.dairy = AdventureStats.Dairy;
        playerData.protein = AdventureStats.Protein;
        playerData.grain = AdventureStats.Grain;
        playerData.vegetable = AdventureStats.Vegetable;
        playerData.fruit = AdventureStats.Fruit;
        playerData.sweets = AdventureStats.Sweets;
        string playerJson = JsonUtility.ToJson(playerData);
        return playerJson;
    }
}
