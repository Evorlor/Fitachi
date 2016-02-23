﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventuringPlayer : MonoBehaviour {

    public int AttackDamage;
    public float attackSpeed;

    List<Enemy> enemies;

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
