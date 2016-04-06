using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventuringPlayer : MonoBehaviour {

    private int AttackDamage;
    private float attackSpeed;
    public float weakenPlayerMultiplier = 10.0f;

    List<Enemy> enemies = new List<Enemy>();

    // Use this for initialization
    IEnumerator Start () {

		yield return FitbitRestClient.GetProfile();
		yield return FitbitRestClient.GetActiviesLifeTimeState();
		yield return FitbitRestClient.GetActiviesDailyState(System.DateTime.Now);
		attackSpeed = float.Parse(FitbitRestClient.Activities.lifetime.total.distance) / 2 / weakenPlayerMultiplier;
		AttackDamage = (int)(float.Parse(FitbitRestClient.Activities.lifetime.total.distance) / weakenPlayerMultiplier);

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
