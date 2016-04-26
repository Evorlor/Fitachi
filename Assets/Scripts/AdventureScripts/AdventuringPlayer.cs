using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using adventureUI;

public class AdventuringPlayer : MonoBehaviour
{
	public RuntimeAnimatorController male;
	public RuntimeAnimatorController female;

    [Tooltip("How many hit points the player has")]
    [SerializeField]
    private int hitPoints;
	private string gender;
	private RuntimeAnimatorController animator;

	void Awake()
    {
        hitPoints = int.Parse(FitbitRestClient.Instance.ActivitiesDaily.summary.steps);
        Debug.Log(hitPoints);

		gender = FitbitRestClient.Instance.Profile.user.gender;
		animator = GetComponent<Animator>().runtimeAnimatorController;
	}

	void Start()
	{
		if (gender == "MALE")
		{
			animator = male;
		}
		else
		{
			animator = female;
		}
	}

    public void AddDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneNames.MainMenu);
    }

    //   [SerializeField]
    //   private int AttackDamage;
    //   [SerializeField]
    //   private float attackSpeed;
    //   [SerializeField]
    //   private float hp;
    //   public float weakenPlayerMultiplier = 10.0f;
    //   public float attackspeedmodifier;

    //   List<Enemy> enemies = new List<Enemy>();

    //   // Use this for initialization
    //   void Start () {
    //       hp = 10;
    //	attackSpeed = float.Parse(FitbitRestClient.Instance.Activities.lifetime.total.distance) / attackspeedmodifier;
    //	AttackDamage = (int)(float.Parse(FitbitRestClient.Instance.Activities.lifetime.total.distance) / weakenPlayerMultiplier);

    //	enemies = new List<Enemy>();
    //       InvokeRepeating("attackEnemies", 0, attackSpeed);
    //       Physics.queriesHitTriggers = true;
    //}

    //// Update is called once per frame
    //void Update () {
    //       if (hp<=0) {
    //           SceneManager.LoadScene("Main");
    //       }
    //}

    //   public void injurePlayer(int incommmingDamage) {
    //       hp -= incommmingDamage;
    //   }

    //   void OnTriggerEnter2D(Collider2D attackedMonster) {
    //       if (attackedMonster.tag=="Monster") {
    //           enemies.Add(attackedMonster.GetComponent<Enemy>());
    //       }

    //   }
    //   void OnTriggerExit2D(Collider2D attackedMonster) {
    //       if (attackedMonster.tag == "Monster") {
    //           enemies.Remove(attackedMonster.GetComponent<Enemy>());
    //       }
    //   }
    //   private void attackEnemies() {
    //       foreach (Enemy monster in enemies) {
    //           monster.TakeDamage(AttackDamage);
    //       }
    //   }
    //   public void IgnoreMonster(Enemy ignoreMonster) {
    //       enemies.Remove(ignoreMonster);
    //   }
}
