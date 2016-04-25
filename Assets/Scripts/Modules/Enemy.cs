using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("X position where the enemies go kaboom")]
    [SerializeField]
    [Range(-2.5f, 0.0f)]
    private float xDeath = -2.0f;

    [Tooltip("How high above the head coin spawns after enemy death")]
    [SerializeField]
    [Range(0.0f, 2.5f)]
    private float coinYOffset = 0.25f;

    [Tooltip("Coin that will be dropped")]
    [SerializeField]
    private AdventureCoin coin;
    public float movespeed;

    public int CoinDropRange;
    public int coinDrops;

    private AdventuringPlayer player;

    Rigidbody2D characterRigidbody;

    void Start()
    {
        player = FindObjectOfType<AdventuringPlayer>();
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        characterRigidbody.velocity = (Vector3.left * movespeed);
        if (transform.position.x <= xDeath)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GetComponent<TreasureTurtle>())
        {
            Debug.Log("Treasure turtle is DEADDDDD!!!");
        }
        else
        {
            Debug.Log("The enemy ate shit and spawned a coin");
            var coinPosition = transform.position;
            coinPosition.y += coinYOffset;
            Instantiate(coin, coinPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
