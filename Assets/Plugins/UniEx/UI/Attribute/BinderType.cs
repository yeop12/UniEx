using System;
using System.Collections.Generic;
using System.Linq;

namespace UniEx.UI
{
	[AttributeUsage(AttributeTargets.Field)]
	public class BinderType : Attribute
	{
		private readonly List<Type> _types = new();

		public BinderType(Type type, bool includeObservableType = true)
		{
			_types.Add(type);
			if (includeObservableType)
			{
				_types.Add(typeof(IObservable<>).MakeGenericType(type));
			}
		}

		public BinderType(params Type[] types)
		{
			_types.AddRange(types);
		}

		public bool IsTargetType(Type type) => _types.Any(x => x.IsAssignableFrom(type));
	}
}
