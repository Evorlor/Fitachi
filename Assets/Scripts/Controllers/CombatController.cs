using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    private const string PlayerTurnText = "It's your turn!";
    private const string NotPlayerTurnText = "It's not your turn!";

    [SerializeField]
    [Tooltip("Keeps track of whose turn it is")]
    private Text turnDisplay;

    [SerializeField]
    [Tooltip("How long of an interval between checks to see if it's the player's turn")]
    private float pollTime = 2.0f;

    void Start()
    {
        turnDisplay.text = NotPlayerTurnText;
        WaitForTurn();
    }

    private void TakeTurn()
    {
        ServerManager.Instance.TakeTurn(OnTurnComplete);
    }

    private void OnTurnComplete(string result)
    {
        bool success = bool.Parse(result);
        if (!success)
        {
            return;
        }
        turnDisplay.text = NotPlayerTurnText;
        WaitForTurn();
    }

    private void WaitForTurn()
    {
        ServerManager.Instance.NotifyOnTurnReady(OnTurnReady, pollTime);
    }

    private void OnTurnReady(string result)
    {
        turnDisplay.text = PlayerTurnText;
    }
}