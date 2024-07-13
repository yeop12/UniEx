using System;
using System.Collections.Generic;
using System.Linq;

namespace UniEx.UI
{
	[AttributeUsage(AttributeTargets.Field)]
	public class GetterBinderTypeAttribute : BinderTypeAttribute
	{
		private readonly List<Type> _types = new();
		public override bool IsGetter => true;

		public GetterBinderTypeAttribute( Type type, bool includeObservableType = true)
		{
			_types.Add(type);
			if (includeObservableType)
			{
				_types.Add(typeof(IObservable<>).MakeGenericType(type));
			}
		}

		public GetterBinderTypeAttribute( params Type[] types)
		{
			_types.AddRange(types);
		}

		public override bool IsTargetType(Type type ) => _types.Any(x => x.IsAssignableFrom(type));
	}
}
