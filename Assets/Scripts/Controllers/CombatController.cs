using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("How often to poll the server to check if it is the player's turn yet")]
    private float pollTime = 2.0f;

    private bool activeTurn = false;
    private bool takeTurn = false;

	void Start()
    {
        turnDisplay.text = NotPlayerTurnText;
		StartCoroutine(DispatchPool());
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("P0 turn: " + activeTurn);
        }
    }

    public void TakeTurn()
    {
        if (!activeTurn)
        {
            return;
        }
        takeTurn = true;
		turnDisplay.text = NotPlayerTurnText;

        StartCoroutine(DispatchTakeTurn());
        StartCoroutine(DispatchPool());
    }

    private IEnumerator DispatchTakeTurn()
    {
        // Send request
        if (takeTurn && activeTurn)
        {
            yield return ServerManager.Instance.ServerTakeTurn();
        }
    }

    private IEnumerator DispatchPool()
    {
		activeTurn = false;
		while (!activeTurn)
        {
			yield return ServerManager.Instance.CheckForTurn();
			activeTurn = ServerManager.Instance.CheckForTurnResult;
            if (activeTurn)
            {
                turnDisplay.text = PlayerTurnText;
                break;
            }
            yield return new WaitForSeconds(pollTime);
        }
    }
}