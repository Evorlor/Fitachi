using System;
using UnityEngine;
using UnityEngine.UI;

namespace adventureUI
{
	public class AdventureUI : MonoBehaviour
	{
		public Text DistanceTraveledValue;
		public Text MonstersDefeatedValue;
		public Text PowerUpsCollectedValue;

		private int distanceTraveled;
		private int monstersDefeated;
		private int powerUpsCollected;

		void Start()
		{

			distanceTraveled = 0;
			monstersDefeated = 0;
			powerUpsCollected = 0;

			DistanceTraveledValue.text = "0";
			MonstersDefeatedValue.text = "0";
			PowerUpsCollectedValue.text = "0";
		}

		void FixedUpdate()
		{
			UpdateDistanceTraveledUI();
		}

		public void UpdateDistanceTraveledUI()
		{
			distanceTraveled = EnemySpawner.stepsTaken;
			DistanceTraveledValue.text = distanceTraveled.ToString();
		}

		public void UpdateMonstersDefeatedUI()
		{
			monstersDefeated += 1;
			MonstersDefeatedValue.text = monstersDefeated.ToString();
		}

		public void UpdatePowerUpsCollectedUI()
		{
			powerUpsCollected += 1;
			PowerUpsCollectedValue.text = powerUpsCollected.ToString();
		}
	} 
}