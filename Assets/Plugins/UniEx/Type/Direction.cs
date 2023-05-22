using System;
using UnityEngine;

namespace UniEx
{
	public enum Direction
	{
		Up,
		Down,
		Right,
		Left,
	}

	public static class DirectionUtil
	{
		public static Vector2Int ToVector2Int( this Direction direction ) => direction switch {
			Direction.Up => Vector2Int.up,
			Direction.Down => Vector2Int.down,
			Direction.Right => Vector2Int.right,
			Direction.Left => Vector2Int.left,
			_ => throw new NotImplementedException($"{direction}")
		};

		public static Vector2 ToVector2( this Direction direction ) => direction switch {
			Direction.Up => Vector2.up,
			Direction.Down => Vector2.down,
			Direction.Right => Vector2.right,
			Direction.Left => Vector2.left,
			_ => throw new NotImplementedException($"{direction}")
		};

		public static Direction GetOpposite( this Direction direction ) => direction switch {
			Direction.Up => Direction.Down,
			Direction.Down => Direction.Up,
			Direction.Right => Direction.Left,
			Direction.Left => Direction.Right,
			_ => throw new NotImplementedException($"{direction}")
		};
	}
}
