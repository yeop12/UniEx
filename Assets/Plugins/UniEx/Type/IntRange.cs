using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UniEx
{
	[Serializable]
	public struct IntRange
	{
		public int Min;
		public int Max;

		public float RandomValue => Random.Range(Min, Max);

		public IntRange( int min, int max )
		{
			Min = min;
			Max = max;
		}

		public bool IsIn( int value ) => value >= Min && value <= Max;

		public int Clamp( int value ) => Mathf.Clamp(value, Min, Max);
	}
}