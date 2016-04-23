using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int currHP, maxHP;
    public float movespeed;
    
    public int CoinDropRange;
    public int coinDrops;

    private AdventuringPlayer player;

    Rigidbody2D characterRigidbody;

    void Start()
    {
        currHP = maxHP = (int)(Time.timeSinceLevelLoad / Random.Range(3.0f, 6.5f));
        player = FindObjectOfType<AdventuringPlayer>();
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (currHP <= 0)
        //{
        //    OnKill();
        //    Destroy(gameObject);
        //}
        characterRigidbody.velocity = (Vector3.left * movespeed);
        if (transform.position.x <= xDeath)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("The enemy ate shit and spawned a coin");
        var coinPosition = transform.position;
        coinPosition.y += coinYOffset;
        Instantiate(coin, coinPosition, Quaternion.identity);
        Destroy(gameObject);
        //player.AddDamage(1);
        //Destroy(gameObject);
    }

    //public void TakeDamage(int damage)
    //{
    //    currHP -= damage;
    //}

    //void OnKill()
    //{
    //    for (int i = 0; i < coinDrops; i++)
    //    {
    //        Instantiate(coin, new Vector3(transform.position.x + Random.Range(-CoinDropRange, CoinDropRange), transform.position.y + Random.Range(-CoinDropRange, CoinDropRange), transform.position.z), Quaternion.identity);
    //        AdventureStats.Endurance.HeartRate++;
    //        AdventureStats.Nutrition.Hunger++;
    //        AdventureStats.Rest.Sleep++;
    //        AdventureStats.Speed.Steps++;
    //        AdventureStats.gold += Random.Range(5, 10);
    //    }
    //}

}
