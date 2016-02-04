public class PlayerManager : ManagerBehaviour<PlayerManager>
{
    public Nutrition Nutrition { get; set; }
    public Speed Speed { get; set; }
    public Endurance Endurance { get; set; }
    public Rest Rest { get; set; }
}