[System.Serializable]
public class PlayerData {
    public int dairy;
    public int protein;
    public int grain;
    public int vegetable;
    public int fruit;
    public int sweets;

	public static PlayerData GetPlayerData()
	{
		var playerData = new PlayerData();
		playerData.dairy = AdventureStats.Dairy;
		playerData.protein = AdventureStats.Protein;
		playerData.grain = AdventureStats.Grain;
		playerData.vegetable = AdventureStats.Vegetable;
		playerData.fruit = AdventureStats.Fruit;
		playerData.sweets = AdventureStats.Sweets;
		return playerData;
	}
}