using System;
using System.Collections.Generic;

namespace UniEx
{
	public static class CommonExtension
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (action is null) throw new ArgumentNullException(nameof(action));
			foreach (var x in source)
			{
				action.Invoke(x);
			}
		}

		public static string ToMoneyFormat(this int money)
		{
			return $"{money:#,0}";
		}
	}
}
