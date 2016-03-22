using System.Collections;
using UnityEngine;

public class PlayerManager : ManagerBehaviour<PlayerManager>
{
    public int TokenLength { get; set; }
    public const int StartingHitPoints = 100;
    public const int StartingAttackPower = 10;
    public Player Player { get; set; }

    private const float pollTime = 2.0f;

    void Start()
    {
        StartCoroutine(UpdatePlayer());
    }

    private IEnumerator UpdatePlayer()
    {
        while (true)
        {
            ServerManager.Instance.GetPlayerJson(PlayerUpdated);
            yield return new WaitForSeconds(pollTime);
        }
    }

    private void PlayerUpdated(string results)
    {
        Player = JsonUtility.FromJson<Player>(results);
    }

    //public int UserID { get; set; }
    //public Nutrition Nutrition { get; set; }
    //public Speed Speed { get; set; }
    //public Endurance Endurance { get; set; }
    //public Rest Rest { get; set; }

    //protected override void Awake()
    //{
    //    UserID = 0;
    //}
}