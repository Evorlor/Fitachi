using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currHP, maxHP;
    public float movespeed;

    public GameObject coin;
    public int CoinDropRange;
    public int coinDrops;

    Rigidbody2D characterRigidbody;

    void Start() {
        currHP = maxHP;
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (currHP <= 0) {
            OnKill();
            Destroy(gameObject);
        }
        characterRigidbody.velocity=( Vector3.left * movespeed);
    }

    public void TakeDamage(int damage) {
        currHP -= damage;
    }

    void OnKill() {
        for (int i = 0; i<coinDrops; i++) {
            Instantiate(coin, new Vector3(transform.position.x+Random.Range(-CoinDropRange, CoinDropRange),transform.position.y + Random.Range(-CoinDropRange, CoinDropRange), transform.position.z ), Quaternion.identity);
        }
    }

}
