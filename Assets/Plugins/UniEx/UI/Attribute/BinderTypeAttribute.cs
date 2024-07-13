using System;

namespace UniEx.UI
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class BinderTypeAttribute : Attribute
	{
		public abstract bool IsGetter { get; }
		public abstract bool IsTargetType(Type type);
	}
}
