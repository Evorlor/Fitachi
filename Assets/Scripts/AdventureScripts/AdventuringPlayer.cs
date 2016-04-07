using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventuringPlayer : MonoBehaviour {

    private int AttackDamage;
    private float attackSpeed;
    public float weakenPlayerMultiplier = 10.0f;
    public float attackSpeedModifier = 2.0f;
    
    List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        attackSpeed = int.Parse(FitbitRestClient.Activities.lifetime.total.distance) / attackSpeedModifier / weakenPlayerMultiplier;
        AttackDamage = (int)(int.Parse(FitbitRestClient.Activities.lifetime.total.distance) / weakenPlayerMultiplier);
    }

    // Use this for initialization
    void Start () {
        enemies = new List<Enemy>();
        InvokeRepeating("attackEnemies", 0, attackSpeed);
        Physics.queriesHitTriggers = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D attackedMonster) {
        if (attackedMonster.tag=="Monster") {
            enemies.Add(attackedMonster.GetComponent<Enemy>());
        }

    }
    void OnTriggerExit2D(Collider2D attackedMonster) {
        if (attackedMonster.tag == "Monster") {
            enemies.Remove(attackedMonster.GetComponent<Enemy>());
        }
    }
    private void attackEnemies() {
        foreach (Enemy monster in enemies) {
            monster.TakeDamage(AttackDamage);
        }
    }
}
