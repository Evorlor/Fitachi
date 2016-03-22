using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitPointMonitor : MonoBehaviour
{
    public Text hitPointsText;

    public void UpdateHitpointText(string token0, int hp0, string token1, int hp1)
    {
        hitPointsText.text = token0 + ": " + hp0 + "  --  " + token1 + ": " + hp1;
    }
}
