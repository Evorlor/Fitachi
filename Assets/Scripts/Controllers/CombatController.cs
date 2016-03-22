using System.Linq;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private const float pollTime = 2.0f;

    public void FindMatch()
    {
        CreatePlayer();
        Debug.Log("first: " + PlayerManager.Instance.Player.hitPoints);
        ServerManager.Instance.FindMatch(OnMatchFound, pollTime);
    }

    public void Attack()
    {
        ServerManager.Instance.Attack(OnAttack);
    }

    private void OnAttack(string result)
    {
        var enemy = JsonUtility.FromJson<Player>(result);
        var hitPointMonitor = FindObjectOfType<HitPointMonitor>();
        hitPointMonitor.UpdateHitpointText(PlayerManager.Instance.Player.token, PlayerManager.Instance.Player.hitPoints, enemy.hitPoints);
    }

    private void OnMatchFound()
    {
        Debug.Log("match found");
    }

    private void CreatePlayer()
    {
        var player = new Player();
        player.token = GenerateRandomToken();
        player.hitPoints = PlayerManager.StartingHitPoints;
        player.attackPower = PlayerManager.StartingAttackPower;
        PlayerManager.Instance.Player = player;
    }

    private string GenerateRandomToken()
    {
        return "T" + Random.Range(0, 100000);
    }

    //private const string PlayerTurnText = "It's your turn!";
    //private const string NotPlayerTurnText = "It's not your turn!";

    //[SerializeField]
    //[Tooltip("Keeps track of whose turn it is")]
    //private Text turnDisplay;

    //[SerializeField]
    //[Tooltip("How long of an interval between checks to see if it's the player's turn")]
    //private float pollTime = 2.0f;

    //void Start()
    //{
    //    turnDisplay.text = NotPlayerTurnText;
    //    WaitForTurn();
    //}

    //private void TakeTurn()
    //{
    //    //ServerManager.Instance.TakeTurn(OnTurnComplete);
    //}

    //private void OnTurnComplete(string result)
    //{
    //    bool success = bool.Parse(result);
    //    if (!success)
    //    {
    //        return;
    //    }
    //    turnDisplay.text = NotPlayerTurnText;
    //    WaitForTurn();
    //}

    //private void WaitForTurn()
    //{
    //    //ServerManager.Instance.NotifyOnTurnReady(OnTurnReady, pollTime);
    //}

    //private void OnTurnReady(string result)
    //{
    //    turnDisplay.text = PlayerTurnText;
    //}
}