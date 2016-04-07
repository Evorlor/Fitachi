using UnityEngine;
using System.Collections;

namespace Fitbit
{
	namespace User
	{
		[System.Serializable]
		public class Profile
		{
			public User user;
		}

		[System.Serializable]
		public class Features
		{
			public string exerciseGoal;
		}

		[System.Serializable]
		public class TopBadges
		{
			public string info;
		}

		[System.Serializable]
		public class User
		{
			public string age;
			public string avatar;
			public string avatar150;
			public string averageDailySteps;
			public string corporate;
			public string country;
			public string dateOfBirth;
			public string displayName;
			public string distanceUnit;
			public string encodedId;
			public Features features;
			public string foodsLocale;
			public string fullName;
			public string gender;
			public string glucoseUnit;
			public string height;
			public string heightUnit;
			public string locale;
			public string memberSince;
			public string offsetFromUTCMillis;
			public string startDayOfWeek;
			public string strideLengthRunning;
			public string strideLengthRunningType;
			public string strideLengthWalking;
			public string strideLengthWalkingType;
			public string timezone;
			public TopBadges[] topBadges;
			public string waterUnit;
			public string waterUnitName;
			public string weight;
			public string weightUnit;
		}
	}
}
