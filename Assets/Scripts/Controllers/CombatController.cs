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

    bool pollLock = false;
    bool turnLock = false;

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
        if (turnLock)
        {
            yield break;
        }
        else
        {
            turnLock = true;
        }
        yield return null;
        Debug.Log("DispatchTakeTurn");

        // Send request
        bool success = false;
        if (takeTurn && activeTurn)
        {
            success = ServerManager.Instance.ServerTakeTurn();
            Debug.Log("TURNED: " + success);
            activeTurn = false;
        }

        Debug.Log("DispatchTakeTurn Done");
        turnLock = false;
        yield break;
    }

    private IEnumerator DispatchPool()
    {
        if (pollLock)
        {
            yield break;
        } else
        {
            pollLock = true;
        }
        yield return null;
        Debug.Log("DispatchPool");

        while (!activeTurn)
        {
            activeTurn = ServerManager.Instance.CheckForTurn();
            if (activeTurn)
            {
                turnDisplay.text = PlayerTurnText;
                break;
            }
            yield return new WaitForSeconds(pollTime);
        }

        Debug.Log("DispatchPool Done");
        pollLock = false;
        yield break;
    }
}