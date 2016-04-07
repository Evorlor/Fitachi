using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D attackedMonster)
	{
		if (attackedMonster.tag == "Monster")
		{
			Destroy(attackedMonster.gameObject);
			SceneManager.LoadScene("Main");
        }

	}
}
