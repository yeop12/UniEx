using UnityEngine;

namespace UniEx
{
	public struct Range
	{
		public float Min;
		public float Max;

		public float RandomValue => Random.Range(Min, Max);

		public Range(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public bool IsIn(float value) => value >= Min && value <= Max;

		public float Clamp(float value) => Mathf.Clamp(value, Min, Max);
	}
}
