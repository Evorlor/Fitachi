using System.Collections;
using UnityEngine;

public class PlayerManager : ManagerBehaviour<PlayerManager>
{
    public static readonly string ID = "ID" + Random.Range(0, 100000);
    public int TokenLength { get; set; }
    public const int StartingHitPoints = 100;
    public const int StartingAttackPower = 10;
    public Player Player { get; set; }
    public int enemyHitPoints = 100;
}