using System;
using UnityEngine;

namespace UniEx
{
	[Serializable]
	public struct IntRect
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public int MinX => X;
		public int MaxX => X + Width - 1;
		public int MinY => Y;
		public int MaxY => Y + Height - 1;
		public Vector2Int Position => new(X, Y);
		public Vector2Int Size => new(Width, Height);

		public IntRect(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public IntRect(Vector2Int position, Vector2Int size)
		{
			X = position.x;
			Y = position.y;
			Width = size.x;
			Height = size.y;
		}
	}
}
