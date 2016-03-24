using UnityEngine;
using System.Collections;

namespace Fitbit {
	namespace Activity
	{
		[System.Serializable]
		public class LifeTime
		{
			public Total total;
			public Tracker tracker;
		}

		[System.Serializable]
		public class Tracker
		{
			public string activeScore;
			public string caloriesOut;
			public string distance;
			public string steps;
		}

		[System.Serializable]
		public class Total
		{
			public string activeScore;
            public string caloriesOut;
			public string distance;
			public string steps;
		}

		[System.Serializable]
		public class Activities
		{
			public LifeTime lifetime;
        }
	}
}
