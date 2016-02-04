using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Rate at which the enemies will spawn")]
    [Range(0, 100)]
    public float spawnRate = 1.0f;

    [Tooltip("Enemies which will be randomly spawned on the spawn line")]
    public Enemy[] enemiesToSpawn;

    [Tooltip("Starting position for the spawn line")]
    public Vector2 startingPosition;

    [Tooltip("Ending position for the spawn line")]
    public Vector2 endingPosition;

    private readonly Color spawnLineColor = Color.red;

    void Update()
    {
        //PlayerManager.Instance.Rest;
    }

    void OnDrawGizmosSelected()
    {
        DrawEnemySpawnOrigin();
    }

    private void DrawEnemySpawnOrigin()
    {
        Gizmos.color = spawnLineColor;
        Gizmos.DrawLine(startingPosition, endingPosition);
        Gizmos.DrawIcon(startingPosition, FileNames.EnemySpawnPositionGizmo);
        Gizmos.DrawIcon(endingPosition, FileNames.EnemySpawnPositionGizmo);
    }
}