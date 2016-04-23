using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("How long enemies spawn for")]
    [SerializeField]
    [Range(5.0f, 60.0f)]
    private float spawnDuration = 30.0f;

    [Tooltip("Rate at which the enemies will spawn")]
    public float spawnRate;

    [Tooltip("Enemies which will be randomly spawned on the spawn line")]
    public Enemy[] enemiesToSpawn;

    [Tooltip("Starting position for the spawn line")]
    public Vector2 startingPosition;

    [Tooltip("Ending position for the spawn line")]
    public Vector2 endingPosition;

    private readonly Color spawnLineColor = Color.red;

    void Awake()
    {
        spawnRate = 1.0f / (int.Parse(FitbitRestClient.ActivitiesDaily.summary.steps) + 1.0f);
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8, true);
        InvokeRepeating("SpawnEnemy", 1, spawnRate);
    }

    void Update()
    {
        spawnDuration -= Time.deltaTime;
        if (spawnDuration <= 0)
        {
            SceneManager.LoadScene(SceneNames.MainMenu);
        }
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

    private void SpawnEnemy()
    {
        Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], new Vector3(startingPosition.x, Random.Range(endingPosition.y, startingPosition.y)), Quaternion.identity);
    }
}