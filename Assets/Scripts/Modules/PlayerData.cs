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
		playerData.dairy = AdventureStats.GetStat(FoodType.Dairy);
		playerData.protein = AdventureStats.GetStat(FoodType.Protien);
		playerData.grain = AdventureStats.GetStat(FoodType.Grain);
		playerData.vegetable = AdventureStats.GetStat(FoodType.Vegetable);
		playerData.fruit = AdventureStats.GetStat(FoodType.Fruit);
		playerData.sweets = AdventureStats.GetCombatStrength();
		return playerData;
	}
}