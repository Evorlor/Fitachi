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
    private Animator animator;

	private adventureUI.AdventureUI adventureUI;
    private AdventuringPlayer player;

    Rigidbody2D characterRigidbody;

	void Awake()
	{
        adventureUI = FindObjectOfType<adventureUI.AdventureUI>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = FindObjectOfType<AdventuringPlayer>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterRigidbody.velocity = (Vector3.left * movespeed);
    }

    void Update()
    {
        if (dead)
        {
            characterRigidbody.velocity = Vector3.zero;
        }
		if (!dead && transform.position.x <= xDeath)
        {
            Die();
        }
    }

    void OnDestroy()
    {

        var coinPosition = player.transform.position;
        coinPosition.y += coinYOffset;
        Instantiate(coin, coinPosition, Quaternion.identity);
    }

    private void Die()
    {
        if (GetComponent<TreasureTurtle>())
        {
            Debug.Log("Treasure turtle is DEADDDDD!!!");
			adventureUI.UpdatePowerUpsCollectedUI();
		}
		else
        {

            animator.SetTrigger("Die");
        }
		adventureUI.UpdateMonstersDefeatedUI();
        dead = true;
        Destroy(gameObject, 0.8f);
    }
    private bool dead = false;

}
