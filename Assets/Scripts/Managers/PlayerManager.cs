public class PlayerManager : ManagerBehaviour<PlayerManager>
{
    public int UserID { get; set; }
    public Nutrition Nutrition { get; set; }
    public Speed Speed { get; set; }
    public Endurance Endurance { get; set; }
    public Rest Rest { get; set; }

    protected override void Awake()
    {
        UserID = 0;
    }
}