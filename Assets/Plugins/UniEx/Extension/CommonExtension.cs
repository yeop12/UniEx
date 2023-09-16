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

		public static string ToMoneyFormat(this uint money)
		{
			return $"{money:#,0}";
		}

		public static string ToMoneyFormat(this long money)
		{
			return $"{money:#,0}";
		}

		public static string ToMoneyFormat(this ulong money)
		{
			return $"{money:#,0}";
		}

		public static IGridReactiveCollection Where<T>(this IGridReactiveCollection reactiveCollection,
			Predicate<T> predicate) => new WhereGridReactiveCollection<T>(reactiveCollection, predicate);
	}
}
