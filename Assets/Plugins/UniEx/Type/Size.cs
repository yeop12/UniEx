using System;
using UnityEngine;

namespace UniEx
{
	[Serializable]
	public struct Size
	{
		public static readonly Size Zero = new Size(0, 0);

		public int Width;
		public int Height;

		public Size(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Area => Width * Height;

		public override string ToString()
		{
			return $"Width : {Width}, Height : {Height}";
		}

		public static implicit operator Vector2(Size value) => new Vector2(value.Width, value.Height);
		public static explicit operator Size(Vector2 value) => new Size((int)value.x, (int)value.y);

		public static bool operator ==(Size lhs, Size rhs)
		{
			return lhs.Width == rhs.Width && lhs.Height == rhs.Height;
		}

		public static bool operator !=(Size lhs, Size rhs)
		{
			return lhs.Width != rhs.Width || lhs.Height != rhs.Height;
		}

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			var size = (Size)obj;
			return size == this;
		}

		public override int GetHashCode()
		{
			return Width ^ Height;
		}
	}
}
