using UnityEngine;
using System.Collections;

namespace Fitbit
{
	namespace ActivitiesDaily
	{
		[System.Serializable]
		public class Distances
		{
			public string activity;
			public string distance;
		}

		[System.Serializable]
		public class Summary
		{
			public string activeScore;
			public string activityCalories;
			public string caloriesBMR;
			public string caloriesOut;
			public Distances[] distances;
			public string fairlyActiveMinutes;
			public string lightlyActiveMinutes;
			public string marginalCalories;
			public string sedentaryMinutes;
			public string steps;
			public string veryActiveMinutes;
		}

		[System.Serializable]
		public class Goals
		{
			public string activeMinutes;
			public string caloriesOut;
			public string distance;
			public string steps;
		}

		[System.Serializable]
		public class Activities
		{
			public string info;
		}

		[System.Serializable]
		public class ActivitiesDaily
		{
			public Activities[] activities;
			public Goals goals;
			public Summary summary;
		}
	}
}
